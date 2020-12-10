#ifndef Behaviour_h
#define Behaviour_h

#include "rigidbody.h"
#include "Collision.h"

class PhysicsSceneManager;

//Action: no return value func
typedef void(*Action)(float f);
typedef void(*ActionCollision)(Collision* p);
typedef void(*ActionTrigger)(Collider* p_collider);
class Behaviour
{
public:
	Behaviour(PhysicsSceneManager* sceneMgr);
	virtual ~Behaviour();
	virtual void OnFixedUpdate(float dt);
	virtual void OnCollisionEnter(Collision* p_collision);
	virtual void OnCollisionStay(Collision* p_collision);
	virtual void OnCollisionExit(Collision* p_collision);

	virtual void OnTriggerEnter(Collider* p_collider);
	virtual void OnTriggerStay(Collider* p_collider);
	virtual void OnTriggerExit(Collider* p_collider);
	inline unsigned int GetIndentity() { return m_uiIndentify; }
public:
	const char* m_strName;
    Rigidbody* m_pRigidbody;
	PxMaterial* m_pMaterial;
protected:
	PhysicsSceneManager* m_pScene_mgr;
private:	
	unsigned int m_uiIndentify;
};

#endif
