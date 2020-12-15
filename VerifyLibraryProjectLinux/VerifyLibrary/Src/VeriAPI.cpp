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
    
    VERIAPI_API VerifyMgr* CreateVerifyMgr(unsigned int iSceneConcurrencyCnt)
    {
        printf("CreateVerifyMgr iSceneConcurrencyCnt %ud \n", iSceneConcurrencyCnt);
        return new VerifyMgr(iSceneConcurrencyCnt);
    }

    VERIAPI_API int GetFreeSlotIndex(VerifyMgr* pVerifyMgr, int sceneId)
    {
        if(pVerifyMgr != NULL){
            return pVerifyMgr->GetFreeSlotIndex(sceneId);
        }
        fprintf(stderr,"GetFreeSlotIndex pVerifyMgr is null \n");
        return -2;
    }

    VERIAPI_API void BackSlotIndex(VerifyMgr* pVerifyMgr, int sceneId, int slotIndex)
    {
        if(pVerifyMgr != NULL){
            pVerifyMgr->BackSlotIndex(sceneId, slotIndex);
        }
        else{
            fprintf(stderr,"BackSlotIndex pVerifyMgr is null \n");
        }
    }

    VERIAPI_API SimulationMgr* GetSimulationMgr(VerifyMgr* pVerifyMgr, int slotIndex)
    {
        if(pVerifyMgr != NULL){
           return pVerifyMgr->GetSimulationMgr(slotIndex);
        }
        fprintf(stderr,"GetSimulationMgr pVerifyMgr is null \n");
        return NULL;
    }

    VERIAPI_API bool InitPhysics(SimulationMgr*  pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->InitPhysx();
        }
        fprintf(stderr,"InitPhysics null \n");
        return false;
    }

    VERIAPI_API bool LoadXMLDataFile(SimulationMgr*  pSimulationMgr, char* filePath)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->LoadSceneData(filePath);
        }
        fprintf(stderr,"LoadXMLDataFile null \n");
        return false;
    }

    VERIAPI_API bool CreateBall(SimulationMgr*  pSimulationMgr, float radius, float mass, Vector3 pos, Quaternion quat)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->CreateBall(PxVec3(pos.x, pos.y, pos.z), PxQuat(quat.x, quat.y, quat.z, quat.w), radius, mass);
        }
        fprintf(stderr,"CreateBall null \n");
        return false;
    }

    VERIAPI_API void StepPhysics(SimulationMgr* pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->Simulation();
        }
        else{
            fprintf(stderr,"StepPhysics null \n");
        }
    }

    VERIAPI_API void SetFlagpoleActive(SimulationMgr*  pSimulationMgr, bool active)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->SetFlagpoleActive(active);
        }
        else{
            fprintf(stderr,"SetFlagpoleActive null \n");
        }
    }

    VERIAPI_API void Release(VerifyMgr* pVerifyMgr)
    {
        if(pVerifyMgr != NULL){
            delete pVerifyMgr;
            pVerifyMgr = NULL;
        }
        else{
            fprintf(stderr,"Release pVerifyMgr is null \n");
        }
    }

    VERIAPI_API void InitCallback(SimulationMgr*  pSimulationMgr, Action pFixedUpdate,ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit,ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->InitCallback(pFixedUpdate,pCollisionEnter, pCollisionStay, pCollisionExit,pTriggerEnter, pTriggerStay, pTriggerExit);
        }
        else{
            fprintf(stderr,"InitCallback null \n");
        }
    }

    VERIAPI_API void CleanCallback(SimulationMgr*  pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            pSimulationMgr->CleanCallback();
        }
        else{
            fprintf(stderr,"CleanCallback null \n");
        }
    }

    VERIAPI_API RayCastHit* GetRaycasthit(SimulationMgr* pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->GetRayCastHit();
        }
        else{
            fprintf(stderr,"GetRaycasthit null \n");
            return NULL;
        }
    }

    VERIAPI_API bool RaycastHit(SimulationMgr*  pSimulationMgr, Vector3 pOrigin, Vector3 pDir, PxReal distance, int layerMask, RayCastHit* pHitInfo)
    {
        if(pSimulationMgr != NULL && pHitInfo != NULL){
            return pSimulationMgr->RaycastHit(PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, *pHitInfo, layerMask);
        }
        else{
            fprintf(stderr,"RaycastHit null \n");
            return false;
        }
    }

    VERIAPI_API bool SphereCast(SimulationMgr*  pSimulationMgr, PxReal radius, Vector3 pOrigin, Vector3 pDir, PxReal distance, int layerMask, RayCastHit* pHitInfo)
    {
        if(pSimulationMgr != NULL && pHitInfo != NULL){
            return pSimulationMgr->SphereCast(radius, PxVec3(pOrigin.x, pOrigin.y, pOrigin.z), PxVec3(pDir.x, pDir.y, pDir.z), distance, *pHitInfo, layerMask);
        }
        else{
            fprintf(stderr,"SphereCast null \n");
            return false;
        }
    }

    VERIAPI_API Rigidbody* GetRigidbody(SimulationMgr*  pSimulationMgr)
    {
        if(pSimulationMgr != NULL){
            return pSimulationMgr->GetRigidbody();
        }
        fprintf(stderr,"GetRigidbody:: null \n");
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
            fprintf(stderr,"GetAngularVelocity:: null \n");
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
            fprintf(stderr,"GetLinearVelocity:: null \n");
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
        fprintf(stderr,"IsSleeping:: null \n");
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
            fprintf(stderr,"SetKinematic:: null \n");
        }
    }

    VERIAPI_API bool GetKinematic(Rigidbody* pRigid)
    {
        if(pRigid != NULL){
            return pRigid->IsKinematic();
        }
        fprintf(stderr,"GetKinematic:: null \n");
        return false;
    }

    VERIAPI_API float GetMass(Rigidbody* pRigid)
    {
        if(pRigid != NULL){
            return pRigid->GetMass();
        }
        fprintf(stderr,"GetMass:: null \n");
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
           fprintf(stderr,"GetGlobalPos:: null \n");
        }
        return trs;
    }

    VERIAPI_API const char* GetActorName(PxActor* actor)
    {
        if(actor != NULL){
            return actor->getName();
        }
        fprintf(stderr,"GetActorName:: null \n");
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
            fprintf(stderr,"pShape null \n");
        }
    }

    VERIAPI_API bool IsTrigger(PxShape* pShape)
    {
        if(pShape != NULL)
        {
            return pShape->getFlags().isSet(PxShapeFlag::eTRIGGER_SHAPE);
        }
        fprintf(stderr,"IsTrigger null \n");
        return false;
    }
}
