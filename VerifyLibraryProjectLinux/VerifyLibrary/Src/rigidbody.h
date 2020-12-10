//
//  rigidbody.h
//  PxVisualization
//
//  Created by doodle on 2020/10/13.
//  Copyright © 2020 com.TN. All rights reserved.
//

#ifndef rigidbody_h
#define rigidbody_h
#include "physics_scene_manager.h"

class Rigidbody
{
    public:
        enum EuCollsionDetection{
            eu_discrete = 0,
            eu_continuous,
            eu_continuous_dynamic,
            eu_continuous_speculative
        };
    private:
        PxRigidDynamic* m_pRigidDynamic;
        volatile bool m_bUseGravity;
        EuCollsionDetection m_euCollisionDetection = EuCollsionDetection::eu_discrete;
        
    public:
        Rigidbody(PxRigidDynamic* pRigidDynamic);
        ~Rigidbody();
        PxTransform getGlobalPos(){
            return m_pRigidDynamic->getGlobalPose();
        }
		inline PxReal GetMass() {
			return m_pRigidDynamic->getMass(); 
		}
		void SetName(const char* name);
        void SetMass(PxReal mass);
        void SetKinematic(bool bKinematic);
        bool IsKinematic();
        void SetGravity(bool useGravity);
        void SetCollisionDetection(EuCollsionDetection flag, PhysicsSceneManager* pPhysxSceneMgr);
        bool IsSleeping();
        void WakeUp();
        void Sleep();
        inline void SetLinearVelocity(PxVec3 vt){m_pRigidDynamic->setLinearVelocity(vt);}
        inline PxVec3 GetLinearVelocity(){return m_pRigidDynamic->getLinearVelocity();}
    
		inline PxVec3 GetAngularVelocity() { return m_pRigidDynamic->getAngularVelocity(); }
        inline void SetAngularVelocity(PxVec3 vt){m_pRigidDynamic->setAngularVelocity(vt);}
    
        void AddForce(const PxVec3& force,PxForceMode::Enum mode = PxForceMode::eFORCE);
        void SetGlobalPos(const PxTransform& transform);
    
        PxVec3 GetInertiaTensor(){return m_pRigidDynamic->getMassSpaceInertiaTensor();}
    
        void SetLayer(PxDominanceGroup layer);
        PxDominanceGroup Layer();
    private:        
        void SetRigidFlag(bool active);
    
};
#endif /* rigidbody_h */
