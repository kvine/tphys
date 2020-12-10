//
//  Collsion.cpp
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#include "Collision.h"

Collision::Collision()
{
    contactCount = 0;
    contacts = NULL;
    pTransform = new PxTransform();
    pRigidbody = NULL;
}

//Collision::Collision(PxContactSet contactSet)
//{
//    contactCount = contactSet.size();
//    contacts = new ContactPoint[contactCount];
//    for(int k = 0; k < contactCount; ++k)
//    {
//        contacts[k].normal = contactSet.getNormal(k);
//        contacts[k].point = contactSet.getPoint(k);
//        contacts[k].separation = contactSet.getSeparation(k);
//    }
//    
//    pTransform = new PxTransform();
//    
//}

Collision::~Collision()
{
    delete pTransform;
	CleanContacts();
}

void Collision::CleanContacts()
{
	if (contactCount > 0) {
		delete[] contacts;
		contacts = NULL;
	}
	contactCount = 0;
}

Collider::Collider()
{
	pTransform = new PxTransform();
}

Collider::~Collider()
{
	delete pTransform;
}