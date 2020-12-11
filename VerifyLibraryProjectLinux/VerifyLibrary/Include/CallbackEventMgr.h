//
//  CallbackEventMgr.h
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#ifndef CallbackEventMgr_h
#define CallbackEventMgr_h
#include <map>
//#include <queue>
#include <vector>
#include "PxRigidActor.h"
#include "Collision.h"
#include "PxSimulationEventCallback.h"

using namespace physx;

class Behaviour;

class CallbackEventMgr
{
public:
    enum EuContactStep{
        eu_enter = 0,
        eu_stay = 1,
        eu_exit = 2,   //失去
        eu_null
    };

	struct StCollisionEventNode 
	{
		StCollisionEventNode()
		{
			m_euContaceStep = EuContactStep::eu_null;
			m_pCollisionParams = NULL;
            m_bReady = false;
		}
		~StCollisionEventNode()
		{
			if (m_pCollisionParams != NULL)
			{
				delete m_pCollisionParams;
				m_pCollisionParams = NULL;
			}
		}
		EuContactStep m_euContaceStep;
		Collision*    m_pCollisionParams;
        bool          m_bReady; //防止同一数据多次模拟
	};

	struct StTriggerEventNode 
	{
		StTriggerEventNode()
		{
			m_euTriggerStep = EuContactStep::eu_null;
			m_pColliderParams = NULL;
		};
		~StTriggerEventNode() 
		{ 
			delete m_pColliderParams;
			m_pColliderParams = NULL;
		};

		EuContactStep m_euTriggerStep;
		Collider* m_pColliderParams;
	};
    
    struct StBehaviourCallback
	{
		std::vector<StCollisionEventNode*> m_listCollisionEvent;
		std::vector<StTriggerEventNode*> m_listTriggerEvent;
        Behaviour* m_pBehaviour = NULL;
    };

private:
    std::map<unsigned int,StBehaviourCallback*> m_mapBehaviour;  
public:
    CallbackEventMgr();
    ~CallbackEventMgr();
    
    void Attach(unsigned int indentify, Behaviour* pBehaviour);

    void Detach(unsigned int indentify);
    
    void ModifyContact(const PxRigidActor* px, PxContactSet& contactSet, const PxRigidActor* pxHit, PxTransform& transformHit);
    
	Collision* ExtractContactData(Collision* p_col, const PxContactPair& pair);
    Collision* CreateCollision(const PxContactPair& pair, PxRigidActor* pxHit);

    void NotifyCollision(PxRigidActor* px, const PxContactPair& pair, PxRigidActor* pxHit, EuContactStep euStep);
    
	Collider* CreateCollider(PxRigidActor* px);

    void NotifyTriggerEnter(PxRigidActor* px, PxRigidActor* pxOther);
    
    void NotifyTriggerExit(PxRigidActor* px, PxRigidActor* pxOther);
    
    void DoCollisionEvent();
    
    void DoTriggerEvent();
    
    void DoEvent();

	void OnFixedUpdate(float dt);
private:
    int GetIndentify(void* userData);
};

#endif /* CallbackEventMgr_h */
