#ifdef _WIN32
    #include "./../../PxVisualizationProjectNew/PxVisualizationProject/PxVisualizationProject/SimulateMgr/VerifyMgr.h"

	#ifdef VERIAPI_EXPORTS
		#define VERIAPI_API __declspec(dllexport)
	#else
		#define VERIAPI_API __declspec(dllimport)
	#endif
#else
    #ifdef __linux__
        #include "VerifyMgr.h"
        #define VERIAPI_API __attribute__ ((visibility("default")))
    #else
        #include "./../../PxVisualizationProjectNew/PxVisualizationProject/PxVisualizationProject/SimulateMgr/VerifyMgr.h"

        #define VERIAPI_API
    #endif
#endif



extern "C"{
    struct Vector3{
        float x, y, z;
    };

    struct Quaternion{
        float x, y, z, w;
    };

    struct Transform
    {
        Quaternion q;
        Vector3 p;
    };

    struct RayCastHitExtend{
        RayCastHit hitInfo;
        bool bHit;
    };

    VERIAPI_API Vector3 CreateVector3(PxVec3& pv);

    VERIAPI_API VerifyMgr* CreateVerifyMgr(unsigned int iSceneConcurrencyCnt);

    VERIAPI_API int GetFreeSlotIndex(VerifyMgr* pVerifyMgr, int sceneId);

    VERIAPI_API void BackSlotIndex(VerifyMgr* pVerifyMgr, int sceneId, int slotIndex);

    VERIAPI_API SimulationMgr* GetSimulationMgr(VerifyMgr* pVerifyMgr, int slotIndex);

    VERIAPI_API bool InitPhysics(SimulationMgr*  pSimulationMgr);

	VERIAPI_API bool LoadXMLDataFile(SimulationMgr*  pSimulationMgr, char* filePath);

	VERIAPI_API bool CreateBall(SimulationMgr*  pSimulationMgr, float radius, float mass, Vector3 pos, Quaternion quat);

	VERIAPI_API void StepPhysics(SimulationMgr*  pSimulationMgr);

    VERIAPI_API void SetFlagpoleActive(SimulationMgr*  pSimulationMgr, bool active);

    VERIAPI_API void Release(VerifyMgr* pVerifyMgr);

    VERIAPI_API void InitCallback(SimulationMgr*  pSimulationMgr, Action pFixedUpdate,ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit,ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit);

    VERIAPI_API void CleanCallback(SimulationMgr*  pSimulationMgr);

    VERIAPI_API RayCastHit* GetRaycasthit(SimulationMgr* pSimulationMgr);
    
    VERIAPI_API bool RaycastHit(SimulationMgr* pSimulationMgr, Vector3 pOrigin, Vector3 pDir, PxReal distance, int layerMask, RayCastHit* pHitInfo);

    VERIAPI_API bool SphereCast(SimulationMgr*  pSimulationMgr, PxReal radius, Vector3 pOrigin, Vector3 pDir,  PxReal distance, int layerMask, RayCastHit* pHitInfo);

    VERIAPI_API Rigidbody* GetRigidbody(SimulationMgr*  pSimulationMgr);

    VERIAPI_API Vector3 GetAngularVelocity(Rigidbody* pRigid);
    VERIAPI_API void SetAngularVelocity(Rigidbody* pRigid, Vector3 pV);

    VERIAPI_API Vector3 GetLinearVelocity(Rigidbody* pRigid);
    VERIAPI_API void SetLinearVelocity(Rigidbody* pRigid, Vector3 pV);

    VERIAPI_API bool IsSleeping(Rigidbody* pRigid);
    VERIAPI_API void WakeUp(Rigidbody* pRigid);

    VERIAPI_API void SetKinematic(Rigidbody* pRigid, bool isKinematic);
    VERIAPI_API bool GetKinematic(Rigidbody* pRigid);

    VERIAPI_API float GetMass(Rigidbody* pRigid);
    VERIAPI_API void SetMass(Rigidbody* pRigid,float mass);


    VERIAPI_API void SetGlobalPos(Rigidbody* pRigid, Transform pTrans);
    VERIAPI_API Transform GetGlobalPos(Rigidbody* pRigid);

    VERIAPI_API const char* GetActorName(PxActor* actor);

    VERIAPI_API bool IsNullRigidbody(Rigidbody* pRigid);

    VERIAPI_API void PrintfShapeMaterial(PxShape* pShape);

    VERIAPI_API bool IsTrigger(PxShape* pShape);
}
