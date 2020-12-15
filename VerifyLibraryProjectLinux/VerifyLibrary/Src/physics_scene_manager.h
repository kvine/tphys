#ifndef PhysicsSceneManager_h
#define PhysicsSceneManager_h

#include <ctype.h>
#include <iostream>
#include <fstream>
#include "PxPhysicsAPI.h"
#include "ContactReportCallback.h"
#include "ContactModifyCallback.h"
#include "CallbackEventMgr.h"
#include "PhyRaycast.h"

using namespace physx;

#define CONTACT_OFFSET			    0.01f
#define SLEEP_THRESHOLD             0.005
#define BOUNCE_THRESHOLD            2
#define DEFAULT_SOLVER_ITERATIONS   6
#define DEFAULT_SOLVER_VELOCITY_ITERATIONS 1
#define STEP_T				    0.02f
////	#define CONTACT_OFFSET			0.1f
////	#define STEP_OFFSET				0.01f
////	#define STEP_OFFSET				0.1f
////	#define STEP_OFFSET				0.2f
//
////	#define SLOPE_LIMIT				0.8f
//#define SLOPE_LIMIT				    0.0f
////	#define INVISIBLE_WALLS_HEIGHT	6.0f
//#define INVISIBLE_WALLS_HEIGHT	    0.0f
////	#define MAX_JUMP_HEIGHT			4.0f
//#define MAX_JUMP_HEIGHT			    0.0f

//static const float kScaleFactor = 1.0f;
//static const float kStandingSize = 20.00f * kScaleFactor;
//static const float kCrouchingSize = 5.0f   * kScaleFactor;
//static const float kControllerRadius = 3.0f   * kScaleFactor;

class PhysicsSceneManager
{
public:
	PhysicsSceneManager() 
	{
        m_sceneDesc = NULL;
		g_objectCounter = 0;
		m_pPhysxCallbackMgr = new CallbackEventMgr();
		m_pContactReportCallback = new ContactReportCallback(m_pPhysxCallbackMgr);
		m_pContactModifyCallback = new ContactModifyCallback(m_pPhysxCallbackMgr);
		m_pCCDContactModifyCallback = new CCDContactModifyCallback(m_pPhysxCallbackMgr);	
	}
	~PhysicsSceneManager() 
	{
		delete m_pContactReportCallback;
		m_pContactReportCallback = NULL;

		delete m_pContactModifyCallback;
		m_pContactModifyCallback = NULL;

		delete m_pCCDContactModifyCallback;
		m_pCCDContactModifyCallback = NULL;

		delete m_pPhysxCallbackMgr;
		m_pPhysxCallbackMgr = NULL;

		delete m_pPhyRaycast;
		m_pPhyRaycast = NULL;
	}
	int GetUniqueIndentity();
	bool InitPhysics();
	void CleanPhysics();
	static void CleanPhysxFoundation();
	void SetupPvdDebug(bool bSetUp);
	void StepPhysics(bool interactive);

	bool ParseFromCollectionFile(const char* filePath);

	PxRigidDynamic* createDynamic(PxMaterial* material, const PxTransform& t, const PxGeometry& geometry, Behaviour* pBehaviour = NULL);
	
	static PxFilterFlags contactReportFilterShader(PxFilterObjectAttributes attributes0, PxFilterData filterData0,
		PxFilterObjectAttributes attributes1, PxFilterData filterData1,
		PxPairFlags& pairFlags, const void* constantBlock, PxU32 constantBlockSize);

	void AttachCollisionEvent(unsigned int indentify, Behaviour* pBehaviour);

	void DetachCollisionEvent(unsigned int indentify);
    
    void EnableCCD(bool active);
    void DisableCCDResweep(bool active);

	bool RaycastHit(const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask = -1);
    
    bool SphereCast(const PxReal radius, const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask = -1);
    
	PxMaterial* CreateMaterial(PxReal staticFriction, PxReal dynamicFriction, PxReal restitution, PxCombineMode::Enum frictionMode, PxCombineMode::Enum restitutionMode);
    
    PxRigidActor* FindStaticActorByTag(const char* tag);
    
    static bool CreateFoundation();
private:
	static PxFoundation*           g_foundation;
    static PxDefaultAllocator*     g_allocator;
	static PxDefaultErrorCallback* g_error_callback;
    static PxStringTable*          g_stringTable;
    PxSceneDesc*                 m_sceneDesc;
	PxPhysics*                   m_physics;
	PxPvd*                       m_pvdCon;
	PxCooking*                   m_cooking;
	PxScene*                     m_scene;
    
	PxDefaultCpuDispatcher*      m_dispatcher;
	PxSerializationRegistry*	 m_registery;

	ContactReportCallback*       m_pContactReportCallback;	
	ContactModifyCallback*       m_pContactModifyCallback;
	CCDContactModifyCallback*    m_pCCDContactModifyCallback;
	CallbackEventMgr*            m_pPhysxCallbackMgr;
	PhyRayCast*                  m_pPhyRaycast;

	unsigned int g_objectCounter;
};
#endif //PhysicsSceneManager_h
