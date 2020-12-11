//
//  golf.h
//  PxVisualization
//
//  Created by doodle on 2020/10/13.
//  Copyright © 2020 com.TN. All rights reserved.
//

#ifndef golf_h
#define golf_h
#include "Behaviour.h"

namespace Golf3D 
{
	class Golf : public Behaviour
	{
	public:
		Golf(PhysicsSceneManager* sceneMgr, PxVec3 pos, PxQuat quat, float ballRadius, float mass);
		~Golf();
		
		void StrikeBall(const PxVec3& force, PxForceMode::Enum forceMode = PxForceMode::eFORCE);
        void ResetPosAndQuat(const PxVec3& pos, const PxQuat& quat);
		const char* getName();
		virtual void OnFixedUpdate(float dt);
		virtual void OnCollisionEnter(Collision* p_collision);
		virtual void OnCollisionStay(Collision* p_collision);
		virtual void OnCollisionExit(Collision* p_collision);

		virtual void OnTriggerEnter(Collider* p_collider);
		virtual void OnTriggerStay(Collider* p_collider);
		virtual void OnTriggerExit(Collider* p_collider);


		void InitUpdateCallback(Action pFixedUpdate);
		void InitCollisionCallback(ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit);
        void InitTriggerCallback(ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit);
    private:
        void CreateGolfBall(const PxVec3& pos, const PxQuat& quat, float ballRadius, float mass);
	private:
		Action m_pFunFixedUpdate;
		ActionCollision m_pFunCollisionEnter;
		ActionCollision m_pFunCollisionStay;
		ActionCollision m_pFunCollisionExit;
        ActionTrigger   m_pFunTriggerEnter;
        ActionTrigger   m_pFunTriggerStay;
        ActionTrigger   m_pFunTriggerExit;
	};
}

#endif /* golf_h */
