//
//  CallbackEventMgr.cpp
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#include "CallbackEventMgr.h"
#include "Behaviour.h"

CallbackEventMgr::CallbackEventMgr(){}
CallbackEventMgr::~CallbackEventMgr(){
    std::map<unsigned int, StBehaviourCallback*>::iterator iter;
    for(iter = m_mapBehaviour.begin(); iter != m_mapBehaviour.end(); iter++)
    {
        if(iter->second != NULL)
        {
           delete iter->second;
           iter->second = NULL;
        }
    }
    m_mapBehaviour.clear();
};

void CallbackEventMgr::Attach(unsigned int indentify, Behaviour* pBehaviour)
{
	if (pBehaviour == NULL) { return; }

	std::map<unsigned int, StBehaviourCallback*>::iterator iter;
	iter = m_mapBehaviour.find(indentify);
	if (iter == m_mapBehaviour.end())
	{
		StBehaviourCallback* pStcol = new StBehaviourCallback();
		pStcol->m_pBehaviour = pBehaviour;

		m_mapBehaviour.insert(std::pair<unsigned int, StBehaviourCallback*>(indentify, pStcol));
		printf("Contact Attach indentify %d  success, size %lu \n", indentify, m_mapBehaviour.size());
	}
	else
	{
		printf("Contact Attach failed %d \n", indentify);
	}
}

void CallbackEventMgr::Detach(unsigned int indentify)
{
    std::map<unsigned int, StBehaviourCallback*>::iterator iter;
    iter = m_mapBehaviour.find(indentify);
    if(iter != m_mapBehaviour.end())
    {
       delete iter->second;
       iter->second = NULL;
    }
    m_mapBehaviour.erase(indentify);
}

void CallbackEventMgr::ModifyContact(const PxRigidActor* px, PxContactSet& contactSet, const PxRigidActor* pxHit, PxTransform& transformHit)
{
	PX_UNUSED(px);
	PX_UNUSED(contactSet);
	PX_UNUSED(pxHit);
	PX_UNUSED(transformHit);
}

Collision* CallbackEventMgr::ExtractContactData(Collision* p_col, const PxContactPair& pair)
{
	if (p_col == NULL)
	{
		return p_col;
	}

	p_col->contactCount = pair.contactCount;
	if (p_col->contactCount > 0)
	{
		p_col->contacts = new ContactPoint[p_col->contactCount];
		const int buffLen = 64;
		PxContactPairPoint contacts[buffLen];
		pair.extractContacts(&contacts[0], buffLen >= p_col->contactCount ? p_col->contactCount : buffLen);
		for (int i = 0; i < p_col->contactCount; ++i)
		{
			p_col->contacts[i].normal = contacts[i].normal;
			p_col->contacts[i].point = contacts[i].position;
			p_col->contacts[i].separation = contacts[i].separation;
		}
	}

	return p_col;
}

Collision* CallbackEventMgr::CreateCollision(const PxContactPair& pair, PxRigidActor* pxHit)
{
    Collision* p_col = new Collision();
    
	p_col = ExtractContactData(p_col, pair);
    
    PxTransform tmp = pxHit->getGlobalPose();
    p_col->pTransform->p = tmp.p;
    p_col->pTransform->q = tmp.q;
    
    p_col->dominanceGroup = pxHit->getDominanceGroup();
    p_col->name = pxHit->getName();

	Behaviour* pBehaviour = (Behaviour*)pxHit->userData;
	if (pBehaviour != NULL)
	{
		p_col->pRigidbody = pBehaviour->m_pRigidbody;
	}
    return p_col;
}

void CallbackEventMgr::NotifyCollision(PxRigidActor* px, const PxContactPair& pair, PxRigidActor* pxHit, EuContactStep euStep)
{
    if(px != NULL && px->userData != NULL){
        unsigned int indentify = GetIndentify(px->userData);
        std::map<unsigned int, StBehaviourCallback*>::iterator iter;
        iter = m_mapBehaviour.find(indentify);
        if(iter != m_mapBehaviour.end())
        {
            std::vector<StCollisionEventNode*>::iterator iterNode;
            for (iterNode = iter->second->m_listCollisionEvent.begin(); iterNode != iter->second->m_listCollisionEvent.end(); ++iterNode)
            {
                StCollisionEventNode* pNode = *iterNode;
                if (pNode->m_pCollisionParams->name == pxHit->getName() &&
                    pNode->m_pCollisionParams->dominanceGroup == pxHit->getDominanceGroup() &&
                    *(pNode->m_pCollisionParams->pTransform) == pxHit->getGlobalPose())
                {
                    pNode->m_euContaceStep = euStep;
                    pNode->m_bReady = true;
                    pNode->m_pCollisionParams->CleanContacts();
                    pNode->m_pCollisionParams = ExtractContactData(pNode->m_pCollisionParams, pair);
                    return;
                }
            }
            StCollisionEventNode* tmpCollisionNode = new StCollisionEventNode();
            tmpCollisionNode->m_euContaceStep = euStep;
            tmpCollisionNode->m_bReady = true;
            tmpCollisionNode->m_pCollisionParams = this->CreateCollision(pair, pxHit);
            iter->second->m_listCollisionEvent.push_back(tmpCollisionNode);
        }
        else{
            printf("NotifyCollision no find %d, count is %lu \n", indentify, m_mapBehaviour.size());
        }
    }
}

Collider* CallbackEventMgr::CreateCollider(PxRigidActor* px)
{
	Collider* pColOther = new Collider();
	PxTransform trsOther = px->getGlobalPose();
	pColOther->pTransform->p = trsOther.p;
	pColOther->pTransform->q = trsOther.q;
	pColOther->name = px->getName();
	pColOther->dominanceGroup = px->getDominanceGroup();

    if(px->getNbShapes() > 0)
    {
        PxShape* arrShape[1];
        px->getShapes(arrShape, 1);
        pColOther->isTrigger = arrShape[0]->getFlags() & PxShapeFlag::eTRIGGER_SHAPE;
    }
    else{
        pColOther->isTrigger = false;
    }
	return pColOther;
}

void CallbackEventMgr::NotifyTriggerEnter(PxRigidActor* px, PxRigidActor* pxOther)
{
    if(px != NULL && px->userData != NULL)
	{
        unsigned int indentify = GetIndentify(px->userData);
        std::map<unsigned int, StBehaviourCallback*>::iterator iter;
        iter = m_mapBehaviour.find(indentify);
        if(iter != m_mapBehaviour.end())
        {
            //printf("NotifyTriggerEnter m_euContaceStep %d \n", iter->second->m_euTriggerStep);
			StTriggerEventNode* tmpTriggerNode = new StTriggerEventNode();
			tmpTriggerNode->m_euTriggerStep = EuContactStep::eu_enter;
			tmpTriggerNode->m_pColliderParams = CreateCollider(pxOther);

			iter->second->m_listTriggerEvent.push_back(tmpTriggerNode);
        }
        else
		{
            printf("NotifyTriggerEnter no find %d, count is %lu \n", indentify, m_mapBehaviour.size());
        }
    }
}

void CallbackEventMgr::NotifyTriggerExit(PxRigidActor* px, PxRigidActor* pxOther)
{
    if(px != NULL && px->userData != NULL)
	{
        unsigned int indentify = GetIndentify(px->userData);
        std::map<unsigned int, StBehaviourCallback*>::iterator iter;
        iter = m_mapBehaviour.find(indentify);
        if(iter != m_mapBehaviour.end())
        {
			std::vector<StTriggerEventNode*>::iterator iterNode;
			for (iterNode = iter->second->m_listTriggerEvent.begin(); iterNode != iter->second->m_listTriggerEvent.end(); ++iterNode)
			{
				StTriggerEventNode* pNode = *iterNode;
				if (pNode->m_pColliderParams->name == pxOther->getName() &&
					*(pNode->m_pColliderParams->pTransform) == pxOther->getGlobalPose())
				{
					printf("NotifyTriggerExit find success \n");
					pNode->m_euTriggerStep = EuContactStep::eu_exit;
					return;
				}
			}
			printf("NotifyTriggerExit find failed \n");
        }
        else{
            printf("NotifyTriggerExit no find %d, count is %lu \n", indentify, m_mapBehaviour.size());
        }
    }
}

void CallbackEventMgr::DoCollisionEvent()
{
    std::map<unsigned int, StBehaviourCallback*>::iterator iter;
    StBehaviourCallback* tmpCollision;
	std::vector<StCollisionEventNode*>* pList;
    for(iter = m_mapBehaviour.begin(); iter != m_mapBehaviour.end(); iter++)
    {
		tmpCollision = iter->second;
		pList = &(tmpCollision->m_listCollisionEvent);
		std::vector<StCollisionEventNode*>::iterator iterNode;

		for(iterNode = pList->begin(); iterNode != pList->end();)
		{
			StCollisionEventNode* pNode = *iterNode;
            if(!pNode->m_bReady)
            {
                ++iterNode;
                continue;
            }
            
			bool bRemoveNode = false;
			switch (pNode->m_euContaceStep) 
			{
			case EuContactStep::eu_enter:
				tmpCollision->m_pBehaviour->OnCollisionEnter(pNode->m_pCollisionParams);
				break;
			case EuContactStep::eu_stay:
				tmpCollision->m_pBehaviour->OnCollisionStay(pNode->m_pCollisionParams);
				break;
			case EuContactStep::eu_exit:
				tmpCollision->m_pBehaviour->OnCollisionExit(pNode->m_pCollisionParams);
				bRemoveNode = true;
				break;
			default:
				break;
			}
            pNode->m_bReady = false;
			
			if (bRemoveNode)
			{
				iterNode = pList->erase(iterNode);				
				delete pNode;
			}
			else
			{
				++iterNode;
			}
		}
    }
}

void CallbackEventMgr::DoTriggerEvent()
{
    std::map<unsigned int, StBehaviourCallback*>::iterator iter;
	StBehaviourCallback* tmpTrigger;
    for(iter = m_mapBehaviour.begin(); iter!= m_mapBehaviour.end(); iter++)
    {
        tmpTrigger = iter->second;
		std::vector<StTriggerEventNode*> list = tmpTrigger->m_listTriggerEvent;
		std::vector<StTriggerEventNode*>::iterator iterNode;
		for(iterNode = list.begin(); iterNode != list.end(); )
		{
			StTriggerEventNode* tmpTriggerNode = *iterNode;
			bool removeNode = false;
			switch (tmpTriggerNode->m_euTriggerStep)
			{
			case EuContactStep::eu_enter:
				tmpTrigger->m_pBehaviour->OnTriggerEnter(tmpTriggerNode->m_pColliderParams);
				tmpTriggerNode->m_euTriggerStep = EuContactStep::eu_stay;
				break;
			case EuContactStep::eu_stay:
				tmpTrigger->m_pBehaviour->OnTriggerStay(tmpTriggerNode->m_pColliderParams);
				break;
			case EuContactStep::eu_exit:
				tmpTrigger->m_pBehaviour->OnTriggerExit(tmpTriggerNode->m_pColliderParams);
				tmpTriggerNode->m_euTriggerStep = EuContactStep::eu_null;
				removeNode = true;
				break;
			default:
				break;
			}

			if (removeNode)
			{
				iterNode = list.erase(iterNode);
				delete tmpTriggerNode;
			}
			else
			{
				++iterNode;
			}
		}
    }
}

void CallbackEventMgr::DoEvent()
{
    DoCollisionEvent();
    DoTriggerEvent();
}

int CallbackEventMgr::GetIndentify(void* userdata)
{
    if(userdata == NULL)
    {
        return -1;
    }
    
	Behaviour* pBehaviour = (Behaviour*)userdata;
    if(pBehaviour == NULL)
    {
        return -2;
    }
    return pBehaviour->GetIndentity();
}

void CallbackEventMgr::OnFixedUpdate(float dt)
{
	std::map<unsigned int, StBehaviourCallback*>::iterator iter;	
	for (iter = m_mapBehaviour.begin(); iter != m_mapBehaviour.end(); iter++)
	{
		iter->second->m_pBehaviour->OnFixedUpdate(dt);
	}
}
