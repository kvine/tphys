#ifndef simulationMgr_h
#define simulationMgr_h

#include "physics_scene_manager.h"
#include "golf.h"
using namespace Golf3D;
class SimulationMgr
{
public:
	SimulationMgr(int slotIndex);
	~SimulationMgr();
	bool InitPhysx();
	bool LoadSceneData(const char* filePath);
	bool CreateBall(PxVec3 pos, PxQuat quat, float radius, float mass);
    void StrikeBall(PxVec3 force,PxForceMode::Enum forceMode);
    Rigidbody* GetRigidbody();

	PxVec3 GetBallGlobalPos();
	PxQuat GetBallGlobalQuat();
	void Simulation();
	void CleanPhysics();
	bool RaycastHit(const PxVec3 origin, PxVec3 unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask);
    bool SphereCast(const PxReal radius, PxVec3 origin,  PxVec3 unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask);
    
	void InitCallback(Action pFixedUpdate,ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit,ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit);
    
    void SetFlagpoleActive(bool active);
private:
	PhysicsSceneManager* m_pPhysxSceneMgr;
	Golf* m_pGolf;
    PxRigidActor* m_pFlagpole; //旗杆
	int m_iSlotIndex;
	bool m_bInitPhysx;
	bool m_bLoadedSceneData;

};

#endif
