#include "SimulationMgr.h"

SimulationMgr::SimulationMgr(int slotIndex)
{
	m_pPhysxSceneMgr = new PhysicsSceneManager();
	m_iSlotIndex = slotIndex;
	m_bInitPhysx = false;
	m_bLoadedSceneData = false;
    m_pGolf = NULL;
    m_pFlagpole = NULL;
}

SimulationMgr::~SimulationMgr()
{
    delete m_pPhysxSceneMgr;
    m_pPhysxSceneMgr = NULL;
}

bool SimulationMgr::InitPhysx()
{
	if (!m_bInitPhysx) 
	{
		m_bInitPhysx = m_pPhysxSceneMgr->InitPhysics();
	}
	return m_bInitPhysx;
}

bool SimulationMgr::LoadSceneData(const char* filePath)
{
	if (!m_bLoadedSceneData)
	{
		m_bLoadedSceneData = m_pPhysxSceneMgr->ParseFromCollectionFile(filePath);
	}
	return m_bLoadedSceneData;
}

bool SimulationMgr::CreateBall(PxVec3 pos, PxQuat quat, float radius, float mass)
{
	if (m_pPhysxSceneMgr == NULL) { return false; }

	if (m_pGolf == NULL)
	{
		m_pGolf = new Golf(m_pPhysxSceneMgr, pos, quat, radius, mass);
	}
	else
	{
		printf("reset ball data \n");
		m_pGolf->m_pRigidbody->SetKinematic(true);
		m_pGolf->ResetPosAndQuat(pos, quat);
	}

	return true;
}

void SimulationMgr::StrikeBall(PxVec3 force,PxForceMode::Enum forceMode)
{
    m_pGolf->StrikeBall(force, forceMode);
}

Rigidbody* SimulationMgr::GetRigidbody()
{
    return m_pGolf->m_pRigidbody;
}

PxVec3 SimulationMgr::GetBallGlobalPos()
{
	return m_pGolf->m_pRigidbody->getGlobalPos().p;
}

PxQuat SimulationMgr::GetBallGlobalQuat()
{
	return m_pGolf->m_pRigidbody->getGlobalPos().q;
}

void SimulationMgr::Simulation()
{
	m_pPhysxSceneMgr->StepPhysics(true);
}

void SimulationMgr::CleanPhysics()
{
    if (m_pGolf != NULL)
    {
        delete m_pGolf;
        m_pGolf = NULL;
    }
    if(m_bInitPhysx)
    {
        m_bInitPhysx = false;
        m_pPhysxSceneMgr->CleanPhysics();
    }
}
void SimulationMgr::InitCallback(Action pFixedUpdate,ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit,ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit)
{
	m_pGolf->InitUpdateCallback(pFixedUpdate);
    m_pGolf->InitCollisionCallback(pCollisionEnter, pCollisionStay, pCollisionExit);
    m_pGolf->InitTriggerCallback(pTriggerEnter, pTriggerStay, pTriggerExit);
}

bool SimulationMgr::RaycastHit( PxVec3 origin,  PxVec3 unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
	return m_pPhysxSceneMgr->RaycastHit(origin, unitDir, distance, hitInfo, layerMask);
}

bool SimulationMgr::SphereCast( PxReal radius, PxVec3 origin, const PxVec3 unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
    return m_pPhysxSceneMgr->SphereCast(radius, origin, unitDir, distance, hitInfo, layerMask);
}

void SimulationMgr::SetFlagpoleActive(bool active)
{
    if(m_pFlagpole == NULL)
    {
        m_pFlagpole = m_pPhysxSceneMgr->FindStaticActorByTag("qi_gan%");
        if(m_pFlagpole == NULL){
            printf("no find qi_gan \n");
            return;
        }
    }
    
    m_pFlagpole->setActorFlag(PxActorFlag::eDISABLE_SIMULATION, active);
}
