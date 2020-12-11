//
//  Collision.h
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#ifndef Collision_h
#define Collision_h
#include "PxShape.h"
#include "PxContactModifyCallback.h"
using namespace physx;

class Rigidbody;

struct ContactPoint
{
    PxVec3 point;
    PxVec3 normal;
    float separation;
};

class Collision
{
public:
    Collision();
    //Collision(PxContactSet contactSet);
    ~Collision();
	void CleanContacts();
public:
    int contactCount;
    ContactPoint* contacts;
    Rigidbody* pRigidbody;
    PxTransform* pTransform;
    PxU8 dominanceGroup;	
    const char* name;
};

class Collider
{
public:
    Collider();
    ~Collider();
public:
	PxTransform* pTransform;
	PxU8 dominanceGroup;
    bool isTrigger;
	const char* name;
};

#endif /* Collision_h */
