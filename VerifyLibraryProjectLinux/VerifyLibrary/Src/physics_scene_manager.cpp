#include "physics_scene_manager.h"

PxFoundation* PhysicsSceneManager::m_foundation = NULL;

int PhysicsSceneManager::GetUniqueIndentity()
{
	unsigned int d = ++g_objectCounter;
	return d;
}

void PhysicsSceneManager::AttachCollisionEvent(unsigned int indentify, Behaviour* pBehaviour) {
	m_pPhysxCallbackMgr->Attach(indentify, pBehaviour);
}

void PhysicsSceneManager::DetachCollisionEvent(unsigned int indentify) {
	m_pPhysxCallbackMgr->Detach(indentify);
}

bool PhysicsSceneManager::InitPhysics()
{
	if (PhysicsSceneManager::m_foundation == NULL)
	{
		m_foundation = PxCreateFoundation(PX_FOUNDATION_VERSION, m_allocator, m_error_callback);
		if (PhysicsSceneManager::m_foundation == NULL)
		{
			std::cerr << "PxCreateFoundation failed!" << std::endl;
			return false;
		}
	}

	SetupPvdDebug(false);
	bool recordMemoryAllocations = true;
	m_physics = PxCreatePhysics(PX_PHYSICS_VERSION, *PhysicsSceneManager::m_foundation, PxTolerancesScale(), recordMemoryAllocations, m_pvdCon);
	if (m_physics == NULL)
	{
		std::cerr << "PxCreatePhysics failed!" << std::endl;
		return false;
	}

	m_cooking = PxCreateCooking(PX_PHYSICS_VERSION, *PhysicsSceneManager::m_foundation, PxCookingParams(PxTolerancesScale()));
	if (m_cooking == NULL)
	{
		std::cerr << "PxCreateCooking failed!" << std::endl;
		return false;
	}
	
	//scene descriptor
	m_sceneDesc = new PxSceneDesc(m_physics->getTolerancesScale());
	m_sceneDesc->gravity = PxVec3(0.0f, -9.81f, 0.0f);
	m_dispatcher = PxDefaultCpuDispatcherCreate(2);

	m_sceneDesc->cpuDispatcher = m_dispatcher;

	m_sceneDesc->filterShader = contactReportFilterShader;
	//possible notification callbackk
	m_sceneDesc->simulationEventCallback = m_pContactReportCallback;
	//    m_sceneDesc->contactModifyCallback = m_pContactModifyCallback;
    //    m_sceneDesc->ccdContactModifyCallback = m_pCCDContactModifyCallback;
		
	m_sceneDesc->flags |= PxSceneFlag::eDEPRECATED_TRIGGER_TRIGGER_REPORTS;
	//    sceneDesc->flags |= PxSceneFlag::eENABLE_PCM;
	m_sceneDesc->broadPhaseType = PxBroadPhaseType::eSAP;
	//eENABLE_ENHANCED_DETERMINISM false

	m_sceneDesc->bounceThresholdVelocity = BOUNCE_THRESHOLD;
	m_sceneDesc->frictionType = PxFrictionType::ePATCH;
	//sceneDesc->frictionOffsetThreshold
    
	//create scene and default material
	m_scene = m_physics->createScene(*m_sceneDesc);

	PxPvdSceneClient* pvdClient = m_scene->getScenePvdClient();
	if (pvdClient)
	{
		pvdClient->setScenePvdFlag(PxPvdSceneFlag::eTRANSMIT_CONSTRAINTS, true);
		pvdClient->setScenePvdFlag(PxPvdSceneFlag::eTRANSMIT_CONTACTS, true);
		pvdClient->setScenePvdFlag(PxPvdSceneFlag::eTRANSMIT_SCENEQUERIES, true);
	}

	m_pPhyRaycast = new PhyRayCast(m_scene);
	m_registery = PxSerialization::createSerializationRegistry(*m_physics);

	return true;
}

void PhysicsSceneManager::SetupPvdDebug(bool bSetUp)
{
	if (bSetUp)
	{
		const char*     pvd_host_ip = "127.0.0.1";  // IP of the PC which is running PVD
		int             port = 5425;         // TCP port to connect to, where PVD is listening
		unsigned int    timeout = 100;          // timeout in milliseconds to wait for PVD to respond,
		m_pvdCon = PxCreatePvd(*PhysicsSceneManager::m_foundation);
		PxPvdTransport* transport = PxDefaultPvdSocketTransportCreate(pvd_host_ip, port, timeout);
		m_pvdCon->connect(*transport, PxPvdInstrumentationFlag::eALL);
	}
	else 
	{
		m_pvdCon = NULL;
	}
}

bool PhysicsSceneManager::ParseFromCollectionFile(const char* filePath)
{
	PxCollection* shared = NULL;
	PxCollection* collection = NULL;
	
	PxDefaultFileInputData inputStream(filePath);
	if (inputStream.isValid() == false)
	{
		printf("invalid file path %s \n", filePath);
		return false;
	}
    
    m_stringTable = &PxStringTableExt::createStringTable(m_foundation->getAllocatorCallback());
    
	collection = PxSerialization::createCollectionFromXml(inputStream, *PhysicsSceneManager::m_cooking, *m_registery, shared, m_stringTable);

	if (collection == NULL)
	{
		printf("createCollectionFromXml failed \n");
		return false;
	}
	m_scene->addCollection(*collection);


	//todo: test trigger
	/*PxMaterial* material = CreateMaterial(1, 1, 0.41f, PxCombineMode::eMAX, PxCombineMode::eMAX);
    PxShape* shape = m_physics->createShape(PxBoxGeometry(1, 1, 2), *material, false, PxShapeFlag::eVISUALIZATION | PxShapeFlag::eSCENE_QUERY_SHAPE | PxShapeFlag::eTRIGGER_SHAPE);
    
	PxRigidStatic* pRigid = m_physics->createRigidStatic(PxTransform(1.1f, 4, 0));
    pRigid->attachShape(*shape);
	pRigid->setName("st1");
    m_scene->addActor(*pRigid);

	PxRigidStatic* pRigid2 = m_physics->createRigidStatic(PxTransform(-1.1f, 4, 0));
	pRigid2->attachShape(*shape);
	pRigid2->setName("st2");
	m_scene->addActor(*pRigid2);

    shape->release();*/

	return true;
}

void PhysicsSceneManager::StepPhysics(bool interactive)
{
    m_pPhysxCallbackMgr->OnFixedUpdate(STEP_T);
    m_pPhysxCallbackMgr->DoEvent();
    
	m_scene->simulate(STEP_T); //get the world state after STEP_T second

	m_scene->fetchResults(interactive); //these two line must be used in pair
}

void PhysicsSceneManager::CleanPhysics()
{
    if(m_sceneDesc != NULL)
    {
        delete m_sceneDesc;
    }
    
	m_scene->release();
    
    if(m_stringTable != NULL)
    {
        m_stringTable->release();
    }
    
	m_dispatcher->release();

	m_registery->release();

	m_physics->release();
	PxPvdTransport* transport = NULL;
	if (m_pvdCon != NULL) 
	{
		transport = m_pvdCon->getTransport();
		m_pvdCon->release();
	}

	if (transport != NULL) { transport->release(); }

	m_cooking->release();
}

void PhysicsSceneManager::CleanPhysxFoundation()
{
    if(PhysicsSceneManager::m_foundation != NULL)
	{
        PhysicsSceneManager::m_foundation->release();
    }
}

PxRigidDynamic* PhysicsSceneManager::createDynamic(PxMaterial* material, const PxTransform& t, const PxGeometry& geometry, Behaviour* pBehaviour)
{
	PxShape* shape = m_physics->createShape(geometry, *material, true);
	shape->setContactOffset(CONTACT_OFFSET);
	PxRigidDynamic* meshActor = m_physics->createRigidDynamic(t);
	meshActor->attachShape(*shape);

	shape->release();
	meshActor->setSleepThreshold(SLEEP_THRESHOLD);
	meshActor->setSolverIterationCounts(DEFAULT_SOLVER_ITERATIONS, DEFAULT_SOLVER_VELOCITY_ITERATIONS);
	meshActor->setLinearVelocity(PxVec3(0, 0, 0));
    
	m_scene->addActor(*meshActor);

	meshActor->userData = (void*)(pBehaviour);
	return meshActor;
}

PxFilterFlags PhysicsSceneManager::contactReportFilterShader(PxFilterObjectAttributes attributes0, PxFilterData filterData0,
	PxFilterObjectAttributes attributes1, PxFilterData filterData1,
	PxPairFlags& pairFlags, const void* constantBlock, PxU32 constantBlockSize)
{
	PX_UNUSED(attributes0);
	PX_UNUSED(attributes1);
	PX_UNUSED(filterData0);
	PX_UNUSED(filterData1);
	PX_UNUSED(constantBlockSize);
	PX_UNUSED(constantBlock);

	// all initial and persisting reports for everything, with per-point data
	pairFlags = PxPairFlag::eSOLVE_CONTACT
		//| PxPairFlag::eMODIFY_CONTACTS
		| PxPairFlag::eDETECT_DISCRETE_CONTACT
		//| PxPairFlag::eDETECT_CCD_CONTACT
		| PxPairFlag::eNOTIFY_TOUCH_FOUND
		| PxPairFlag::eNOTIFY_TOUCH_PERSISTS
		| PxPairFlag::eNOTIFY_TOUCH_LOST
		| PxPairFlag::eNOTIFY_CONTACT_POINTS
		;

	return PxFilterFlag::eDEFAULT;
}

bool PhysicsSceneManager::RaycastHit(const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
	if (m_pPhyRaycast == NULL)
	{
		return false;
	}
	return m_pPhyRaycast->RaycastHit(origin, unitDir, distance, hitInfo, layerMask);
}

bool PhysicsSceneManager::SphereCast(const PxReal radius, const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
    if (m_pPhyRaycast == NULL)
    {
        return false;
    }
    return m_pPhyRaycast->SphereCast(radius, origin, unitDir, distance, hitInfo, layerMask);
}

PxMaterial* PhysicsSceneManager::CreateMaterial(PxReal staticFriction, PxReal dynamicFriction, PxReal restitution, PxCombineMode::Enum frictionMode, PxCombineMode::Enum restitutionMode)
{
	PxMaterial* material = m_physics->createMaterial(staticFriction, dynamicFriction, restitution);
	material->setFrictionCombineMode(frictionMode);
	material->setRestitutionCombineMode(restitutionMode);
	return material;
}

void PhysicsSceneManager::EnableCCD(bool active)
{
    if(active)
    {
        m_sceneDesc->flags |= PxSceneFlag::eENABLE_CCD;
    }
}

void PhysicsSceneManager::DisableCCDResweep(bool active)
{
    EnableCCD(active);
    if(active)
    {
        m_sceneDesc->flags |= PxSceneFlag::eDISABLE_CCD_RESWEEP;
    }
}

PxRigidActor* PhysicsSceneManager::FindStaticActorByTag(const char* tag)
{
    PxU32 nbActors = m_scene->getNbActors(PxActorTypeFlag::eRIGID_STATIC);
    printf("static actor nb: %d\n", nbActors);
    if (nbActors != 0)
    {
        std::vector<PxRigidActor*> actors(nbActors);
        m_scene->getActors(PxActorTypeFlag::eRIGID_STATIC, (PxActor**)&actors[0], nbActors);
        
        size_t len = strlen(tag);
        std::vector<PxRigidActor*>::iterator iter;
        for(iter = actors.begin(); iter != actors.end(); ++iter)
        {
            PxRigidActor* tmp = *iter;
            const char* tag_name = tmp->getName();
            
            if(strncmp(tag_name, tag, len) == 0)
            {
//                printf("find actor name is %s \n", tag_name);
                return tmp;
            }
        }
    }
    return NULL;
}
