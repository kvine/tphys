//
//  golf.cpp
//  PxVisualization
//
//  Created by doodle on 2020/10/13.
//  Copyright © 2020 com.TN. All rights reserved.
//

#include "golf.h"

namespace Golf3D
{
	Golf::Golf(PhysicsSceneManager* sceneMgr, PxVec3 pos, PxQuat quat, float ballRadius, float mass)
		:Behaviour(sceneMgr)
	{		
		m_pFunFixedUpdate = NULL;
		m_pFunCollisionEnter = NULL;
		m_pFunCollisionStay = NULL;
		m_pFunCollisionExit = NULL;
        m_pFunTriggerEnter = NULL;
        m_pFunTriggerStay = NULL;
        m_pFunTriggerExit = NULL;
        m_pRigidbody = NULL;
		m_strName = "golfball";
		CreateGolfBall(pos, quat, ballRadius, mass);
	}

	Golf::~Golf()
	{
		
	}

	void Golf::InitUpdateCallback(Action pFixedUpdate)
	{
		m_pFunFixedUpdate = pFixedUpdate;
	}

	void Golf::InitCollisionCallback(ActionCollision pCollisionEnter, ActionCollision pCollisionStay, ActionCollision pCollisionExit)
	{
		m_pFunCollisionEnter = pCollisionEnter;
		m_pFunCollisionStay = pCollisionStay;
		m_pFunCollisionExit = pCollisionExit;
	}

    void Golf::InitTriggerCallback(ActionTrigger pTriggerEnter, ActionTrigger pTriggerStay, ActionTrigger pTriggerExit)
    {
        m_pFunTriggerEnter = pTriggerEnter;
        m_pFunTriggerStay = pTriggerStay;
        m_pFunTriggerExit = pTriggerExit;
    }

	const char* Golf::getName() {
		return m_strName;
	}

	void Golf::CreateGolfBall(const PxVec3& pos, const PxQuat& quat, float ballRadius, float mass)
	{
		if (m_pRigidbody != NULL) {
			return;
		}
        
		m_pMaterial = m_pScene_mgr->CreateMaterial(0, 0, 0, PxCombineMode::eMAX, PxCombineMode::eMAX);
		PxRigidDynamic* pRigidDynamic = m_pScene_mgr->createDynamic(m_pMaterial, PxTransform(pos, quat), PxSphereGeometry(ballRadius), this);

		m_pRigidbody = new Rigidbody(pRigidDynamic);
        
        m_pRigidbody->SetCollisionDetection(Rigidbody::EuCollsionDetection::eu_discrete, m_pScene_mgr);
        
		m_pRigidbody->SetName(m_strName);
		m_pRigidbody->SetMass(mass);
		m_pRigidbody->SetGravity(true);
		m_pRigidbody->SetKinematic(true);
        m_pRigidbody->SetLayer(13);
        
	}


	void Golf::StrikeBall(const PxVec3& force, PxForceMode::Enum forceMode)
	{
		if (m_pRigidbody == NULL)
		{
			std::cout << "rigidbody is null" << std::endl;
			return;
		}
		m_pRigidbody->SetKinematic(false);
		m_pRigidbody->SetCollisionDetection(Rigidbody::EuCollsionDetection::eu_continuous_speculative, m_pScene_mgr);
		m_pRigidbody->AddForce(force, forceMode);
	}

    void Golf::ResetPosAndQuat(const PxVec3& pos, const PxQuat& quat)
    {
        if (m_pRigidbody == NULL)
        {
            std::cout << "rigidbody is null" << std::endl;
            return;
        }
        m_pRigidbody->SetGlobalPos(PxTransform(pos, quat));
    }

	void Golf::OnFixedUpdate(float dt)
	{
		if (m_pFunFixedUpdate != NULL) { (*m_pFunFixedUpdate)(dt); }
		//printf("fixed update \n");
	}

	void Golf::OnCollisionEnter(Collision* p_collision)
	{
        PxVec3 pos = m_pRigidbody->getGlobalPos().p;
        printf("golf collision enter, hit gameobject layer %d , name: %s  pos(%f, %f, %f)\n", p_collision->dominanceGroup, p_collision->name, pos.x, pos.y, pos.z);
		if (m_pFunCollisionEnter != NULL) { (*m_pFunCollisionEnter)(p_collision); }
		else
		{
//            PxVec3 pos = m_pRigidbody->getGlobalPos().p;
//			printf("golf collision enter, hit gameobject layer %d , name: %s  pos(%f, %f, %f)\n", p_collision->dominanceGroup, p_collision->name, pos.x, pos.y, pos.z);
//			if (!m_pRigidbody->IsKinematic())
//			{
//				PxVec3 v = m_pRigidbody->GetLinearVelocity();
//				v *= 0.9f;
//				m_pRigidbody->SetLinearVelocity(v);
//			}

//
			/*std::cout << "golf collision enter" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
			printf("hit gameobject layer %d , hit transform pos is(%f, %f, %f) name: %s \n", p_collision->dominanceGroup,
				p_collision->pTransform->p.x, p_collision->pTransform->p.y, p_collision->pTransform->p.z, p_collision->name);*/
		}
	}

	void Golf::OnCollisionStay(Collision* p_collision)
	{
		if (m_pFunCollisionStay != NULL) { (*m_pFunCollisionStay)(p_collision); }
		else
		{
			/*PxVec3 pos = m_pRigidbody->getGlobalPos().p;
            printf("golf collision stay, hit gameobject layer %d , name: %s  pos(%f, %f, %f)\n", p_collision->dominanceGroup, p_collision->name, pos.x, pos.y, pos.z);*/
			//std::cout << "golf collision stay" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
//            PxVec3 v = m_pRigidbody->GetInertiaTensor();
//            printf("golf collision stay InertiaTensor %f, %f, %f \n", v.x, v.y, v.z);
//			if (!m_pRigidbody->IsKinematic())
//			{
//				PxVec3 v = m_pRigidbody->GetLinearVelocity();
//				v *= 0.9f;
//				m_pRigidbody->SetLinearVelocity(v);
//
//                PxVec3 v2 = m_pRigidbody->GetAngularVelocity();
//                v2 *= 0.9f;
//                m_pRigidbody->SetAngularVelocity(v2);
//			}
		}
	}

	void Golf::OnCollisionExit(Collision* p_collision)
	{
        PxVec3 pos = m_pRigidbody->getGlobalPos().p;
        printf("golf collision exit, hit gameobject layer %d , name: %s  pos(%f, %f, %f)\n", p_collision->dominanceGroup, p_collision->name, pos.x, pos.y, pos.z);
		if (m_pFunCollisionExit != NULL) { (*m_pFunCollisionExit)(p_collision); }
		else
		{
//			PxVec3 pos = m_pRigidbody->getGlobalPos().p;
//            printf("golf collision exit, hit gameobject layer %d , name: %s  pos(%f, %f, %f)\n", p_collision->dominanceGroup, p_collision->name, pos.x, pos.y, pos.z);
			//PxVec3 pos = m_pRigidbody->getGlobalPos().p;
			//std::cout << "golf collision exit" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
		}
	}

	void Golf::OnTriggerEnter(Collider* p_collider)
	{
        if (m_pFunTriggerEnter != NULL) { (*m_pFunTriggerEnter)(p_collider); }
        else
        {
            //PxVec3 pos = m_pRigidbody->getGlobalPos().p;
            printf("golf trigger enter, hit gameobject layer %d , name: %s \n", p_collider->dominanceGroup, p_collider->name);
            //std::cout << "golf trigger enter" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
        }
	}

	void Golf::OnTriggerStay(Collider* p_collider)
	{
        if (m_pFunTriggerStay != NULL) { (*m_pFunTriggerStay)(p_collider); }
        else
        {
            //PxVec3 pos = m_pRigidbody->getGlobalPos().p;
            printf("golf trigger stay, hit gameobject layer %d , name: %s \n", p_collider->dominanceGroup, p_collider->name);
            //std::cout << "golf trigger stay" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
        }
	}

	void Golf::OnTriggerExit(Collider* p_collider)
	{
        if (m_pFunTriggerExit != NULL) { (*m_pFunTriggerExit)(p_collider); }
        else
        {
            printf("golf trigger exit, hit gameobject layer %d , name: %s \n", p_collider->dominanceGroup, p_collider->name);
            //PxVec3 pos = m_pRigidbody->getGlobalPos().p;
            //std::cout << "golf trigger exit" << " ball x " << pos.x << " y " << pos.y << " z " << pos.z << std::endl;
        }
	}
}
