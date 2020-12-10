#include "VeriAPI.h"

extern "C"
{
    VERIAPI_API Vector3 CreateVector3(PxVec3& pv)
    {
        Vector3 v =Vector3();
        v.x = pv.x;
        v.y = pv.y;
        v.z = pv.z;
        return v;
    }

//    VERIAPI_API MyRayCastHit CreateMyCastHit(RayCastHit& hitInfo)
//    {
//        MyRayCastHit castHit = MyRayCastHit();
//        castHit.actor = hitInfo.actor;
//        castHit.distance = hitInfo.distance;
//        castHit.normal = CreateVector3(hitInfo.normal);
//        castHit.point = CreateVector3(hitInfo.point);
//        castHit.shape = hitInfo.shape;
//        return castHit;
//    }
    
    VERIAPI_API VerifyMgr* CreateVerifyMgr()
    {
        return new VerifyMgr();
    }

    VERIAPI_API int GetFreeSlotIndex(VerifyMgr* pVerifyMgr, int sceneId)
    {
        if(pVerifyMgr != NULL){
            return pVerifyMgr->GetFreeSlotIndex(sceneId);
        }
        printf("GetFreeSlotIndex pVerifyMgr is null \n");
        return -2;
    }

    VERIAPI_API void BackSlotIndex(VerifyMgr* pVerifyMgr, int sceneId, int slotIndex)
    {
        if(pVerifyMgr != NULL){
            pVerifyMgr->BackSlotIndex(sceneId, slotIndex);
        }
        else{
            printf("BackSlotIndex pVerifyMgr is null \n");
        }
    }

    VERIAPI_API SimulationMgr* GetSimulationMgr(VerifyMgr* pVerifyMgr, int slotIndex)
    {
        if(pVerifyMgr != NULL){
           return pVerifyMgr->GetSimulationMgr(slotIndex);
        }
        printf("GetSimulationMgr pVerifyMgr is null \n");
        return NULL;
    }

    VERIAPI_API bool InitPhysics(SimulationMgr*  pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->InitPhysx();
        }
        printf("InitPhysics null \n");
        return false;
    }

    VERIAPI_API bool LoadXMLDataFile(SimulationMgr*  pSimulationMgr, char* filePath)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->LoadSceneData(filePath);
        }
        printf("LoadXMLDataFile null \n");
        return false;
    }

    VERIAPI_API bool CreateBall(SimulationMgr*  pSimulationMgr, float radius, float mass, Vector3 pos, Quaternion quat)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->CreateBall(PxVec3(pos.x, pos.y, pos.z), PxQuat(quat.x, quat.y, quat.z, quat.w), radius, mass);
        }
        printf("CreateBall null \n");
        return false;
    }

    VERIAPI_API void StepPhysics(SimulationMgr* pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->Simulation();
        }
        else{
            printf("StepPhysics null \n");
        }
    }

    VERIAPI_API void SetFlagpoleActive(SimulationMgr*  pSimulationMgr, bool active)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->SetFlagpoleActive(active);
        }
        else{
            printf("SetFlagpoleActive null \n");
        }
    }

    VERIAPI_API void Release(VerifyMgr* pVerifyMgr)
    {
        if(pVerifyMgr != NULL){
            delete pVerifyMgr;
            pVerifyMgr = NULL;
        }
        else{
            printf("Release pVerifyMgr is null \n");
        }
    }

    VERIAPI_API void InitCallback(SimulationMgr*  pSimulationMgr, Action pFixedUpdate,ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit,ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->InitCallback(pFixedUpdate,pCollisionEnter, pCollisionStay, pCollisionExit,pTriggerEnter, pTriggerStay, pTriggerExit);
        }
        else{
            printf("InitCallback null \n");
        }
    }

    VERIAPI_API RayCastHitExtend* RaycastHit(SimulationMgr*  pSimulationMgr, Vector3 pOrigin, Vector3 pDir, PxReal distance, int layerMask)
    {
        RayCastHitExtend* pHitExtend = new RayCastHitExtend();
        if(pSimulationMgr != NULL){
            pHitExtend->bHit = pSimulationMgr->RaycastHit(PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, pHitExtend->hitInfo, layerMask);
        }
        else{
            printf("RaycastHit null \n");
            pHitExtend->bHit = false;
        }
        return pHitExtend;
//        RayCastHitExtend hitExtend =RayCastHitExtend();
//        if(pSimulationMgr != NULL){
//            RayCastHit hitInfo = RayCastHit();
//
//            if(pSimulationMgr->RaycastHit(PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, hitInfo, layerMask))
//            {
//                hitExtend.bHit = true;
//                hitExtend.hitInfo = CreateMyCastHit(hitInfo);
//            }
//            else
//            {
//                hitExtend.bHit = false;
//            }
//
//        }
//        else{
//            printf("RaycastHit null \n");
//            hitExtend.bHit = false;
//        }
//        return hitExtend;
    }

    VERIAPI_API RayCastHitExtend* SphereCast(SimulationMgr*  pSimulationMgr, PxReal radius, Vector3 pOrigin, Vector3 pDir, PxReal distance, int layerMask)
    {
        RayCastHitExtend* pHitExtend = new RayCastHitExtend();
        if(pSimulationMgr != NULL){
            pHitExtend->bHit = pSimulationMgr->SphereCast(radius, PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, pHitExtend->hitInfo, layerMask);
        }
        else{
            printf("SphereCast null \n");
            pHitExtend->bHit = false;
        }
        return pHitExtend;
        
//        RayCastHitExtend hitExtend =RayCastHitExtend();
//       if(pSimulationMgr != NULL){
//           RayCastHit hitInfo = RayCastHit();
//           if(pSimulationMgr->SphereCast(radius, PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, hitInfo, layerMask))
//           {
//               hitExtend.bHit = true;
//               hitExtend.hitInfo = CreateMyCastHit(hitInfo);
//           }
//           else
//           {
//               hitExtend.bHit = false;
//           }
//       }
//       else{
//           printf("SphereCast null \n");
//           hitExtend.bHit = false;
//       }
//       return hitExtend;
    }

    VERIAPI_API Rigidbody* GetRigidbody(SimulationMgr*  pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->GetRigidbody();
        }
        printf("GetRigidbody:: null \n");
        return NULL;
    }

    VERIAPI_API Vector3 GetAngularVelocity(Rigidbody* pRigid)
    {
        Vector3 v;
        if(pRigid != NULL){
            PxVec3 pv = pRigid->GetAngularVelocity();
            v.x = pv.x;
            v.y = pv.y;
            v.z = pv.z;
        }
        else{
            printf("GetAngularVelocity:: null \n");
            v.x = v.y = v.z = 0;
        }
        return v;
    }

    VERIAPI_API void SetAngularVelocity(Rigidbody* pRigid, Vector3 pV)
    {
        if(pRigid != NULL){
            pRigid->SetAngularVelocity(PxVec3(pV.x, pV.y, pV.z));
        }
    }

    VERIAPI_API Vector3 GetLinearVelocity(Rigidbody* pRigid)
    {
        Vector3 v;
        if(pRigid != NULL){
            PxVec3 pv = pRigid->GetLinearVelocity();
            v.x = pv.x;
            v.y = pv.y;
            v.z = pv.z;
        }
        else{
            printf("GetLinearVelocity:: null \n");
            v.x = v.y = v.z = 0;
        }
        return v;
    }

    VERIAPI_API void SetLinearVelocity(Rigidbody* pRigid, Vector3 pV)
    {
        if(pRigid != NULL){
            pRigid->SetLinearVelocity(PxVec3(pV.x, pV.y, pV.z));
        }
    }

    VERIAPI_API bool IsSleeping(Rigidbody* pRigid)
    {
        if(pRigid != NULL)
        {
            return pRigid->IsSleeping();
        }
        printf("IsSleeping:: null \n");
        return false;
    }

    VERIAPI_API void WakeUp(Rigidbody* pRigid)
    {
        if(pRigid != NULL){ pRigid->WakeUp();}
    }

    VERIAPI_API void SetKinematic(Rigidbody* pRigid, bool isKinematic)
    {
        if(pRigid != NULL){
            pRigid->SetKinematic(isKinematic);
        }
        else{
            printf("SetKinematic:: null \n");
        }
    }

    VERIAPI_API bool GetKinematic(Rigidbody* pRigid)
    {
        if(pRigid != NULL){
            return pRigid->IsKinematic();
        }
        printf("GetKinematic:: null \n");
        return false;
    }

    VERIAPI_API float GetMass(Rigidbody* pRigid)
    {
        if(pRigid != NULL){
            return pRigid->GetMass();
        }
        printf("GetMass:: null \n");
        return -1;
    }

    VERIAPI_API void SetMass(Rigidbody* pRigid,float mass)
    {
        if(pRigid != NULL){
            pRigid->SetMass(mass);
        }
    }

    VERIAPI_API void SetGlobalPos(Rigidbody* pRigid, Transform pTrans)
    {
        if(pRigid != NULL){
            PxVec3 p = PxVec3(pTrans.p.x, pTrans.p.y, pTrans.p.z);
            PxQuat q = PxQuat(pTrans.q.x, pTrans.q.y, pTrans.q.z, pTrans.q.w);
            pRigid->SetGlobalPos(PxTransform(p,q));
        }
    }

    VERIAPI_API Transform GetGlobalPos(Rigidbody* pRigid)
    {
        Transform trs = Transform();
        if(pRigid != NULL){
            PxTransform pxTrs = pRigid->getGlobalPos();
            Vector3 p = Vector3();
            p.x = pxTrs.p.x;
            p.y = pxTrs.p.y;
            p.z = pxTrs.p.z;
            trs.p = p;
            
            Quaternion q = Quaternion();
            q.x = pxTrs.q.x;
            q.y = pxTrs.q.y;
            q.z = pxTrs.q.z;
            q.w = pxTrs.q.w;
            trs.q = q;
        }
        else{
            printf("GetGlobalPos:: null \n");
        }
        return trs;
    }

    VERIAPI_API const char* GetActorName(PxActor* actor)
    {
        if(actor != NULL){
            return actor->getName();
        }
        printf("GetActorName:: null \n");
        return "";
    }

    VERIAPI_API bool IsNullRigidbody(Rigidbody* pRigid)
    {
        return pRigid == NULL;
    }

    VERIAPI_API void PrintfShapeMaterial(PxShape* pShape)
    {
        if(pShape != NULL && pShape->getNbMaterials() > 0){
            PxMaterial* arrMaterial[1];
            pShape->getMaterials(arrMaterial, 1);
            printf("material restitution %f, staticFriction %f, dynamicFriction %f \n",
            arrMaterial[0]->getRestitution(),
            arrMaterial[0]->getStaticFriction(),
                   arrMaterial[0]->getDynamicFriction());
        }
        else{
            printf("pShape null \n");
        }
    }

    VERIAPI_API bool IsTrigger(PxShape* pShape)
    {
        if(pShape != NULL)
        {
            return pShape->getFlags().isSet(PxShapeFlag::eTRIGGER_SHAPE);
        }
        printf("IsTrigger null \n");
        return false;
    }
}
