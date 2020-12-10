#include "Behaviour.h"

#define UNUSED(x) (void)x

Behaviour::Behaviour(PhysicsSceneManager* sceneMgr)
{
	m_pRigidbody = NULL;
	m_pMaterial = NULL;
	m_strName = "";

	m_pScene_mgr = sceneMgr;
	if (sceneMgr != NULL)
	{
		//set userdata
		m_uiIndentify = sceneMgr->GetUniqueIndentity();
		sceneMgr->AttachCollisionEvent(m_uiIndentify, this);
	}
	printf("create behaviour %d \n", m_uiIndentify);
}

Behaviour::~Behaviour()
{
	if (m_pScene_mgr != NULL) 
	{
		m_pScene_mgr->DetachCollisionEvent(m_uiIndentify);
	}

	if (m_pMaterial != NULL)
	{
		m_pMaterial->release();
	}

	if (m_pRigidbody != NULL)
	{
		delete m_pRigidbody;
		m_pRigidbody = NULL;
	}
}

void Behaviour::OnFixedUpdate(float dt)
{	
	UNUSED(dt);
}
void Behaviour::OnCollisionEnter(Collision* p_collision)
{	
	UNUSED(p_collision);
}
void Behaviour::OnCollisionStay(Collision* p_collision) 
{
	UNUSED(p_collision);
}
void Behaviour::OnCollisionExit(Collision* p_collision) 
{
	UNUSED(p_collision);
}
void Behaviour::OnTriggerEnter(Collider* p_collider) {UNUSED(p_collider);}
void Behaviour::OnTriggerStay(Collider* p_collider) {UNUSED(p_collider);}
void Behaviour::OnTriggerExit(Collider* p_collider) {UNUSED(p_collider);}
