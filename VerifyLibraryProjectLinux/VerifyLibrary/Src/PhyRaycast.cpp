//
//  PhyRaycast.cpp
//  TestPhysx
//
//  Created by doodle on 2020/10/23.
//  Copyright © 2020 doodle. All rights reserved.
//
#include "PhyRaycast.h"

MyRaycastCallback::MyRaycastCallback()
	:PxHitCallback(NULL, 0)
{}
MyRaycastCallback::~MyRaycastCallback()
{}

PxAgain MyRaycastCallback::processTouches(const PxRaycastHit* buffer, PxU32 nbHits)
{
	PX_UNUSED(buffer);
	PX_UNUSED(nbHits);
    return this->hasBlock;
}

MySweepRaycastCallback::MySweepRaycastCallback()
 :PxSweepCallback(NULL, 0)
{
    
}
MySweepRaycastCallback::~MySweepRaycastCallback()
{
    
}

PxAgain MySweepRaycastCallback::processTouches(const PxSweepHit* buffer, PxU32 nbHits)
{
	PX_UNUSED(buffer);
	PX_UNUSED(nbHits);
    return this->hasBlock;
}


MyQueryFilterCallback::MyQueryFilterCallback(int layerMask) 
{
	m_iLayerMask = layerMask;
}

MyQueryFilterCallback::~MyQueryFilterCallback()
{

}

PxQueryHitType::Enum MyQueryFilterCallback::preFilter(
	const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags)
{
	PX_UNUSED(filterData);
	PX_UNUSED(actor);
	PX_UNUSED(queryFlags);
	if (m_iLayerMask == -1)
	{
		return PxQueryHitType::eBLOCK;
	}

	PxFilterData queryFilterData = shape->getQueryFilterData();
	if (queryFilterData.word0 & m_iLayerMask)
	{
		return PxQueryHitType::eBLOCK;
	}
	return PxQueryHitType::eNONE;
}

PxQueryHitType::Enum MyQueryFilterCallback::postFilter(const PxFilterData& filterData, const PxQueryHit& hit)
{
	PX_UNUSED(filterData);
	PX_UNUSED(hit);
	return PxQueryHitType::eTOUCH;
}


PhyRayCast::PhyRayCast(PxScene* pScene)
{
	m_scene = pScene;
}

PhyRayCast::~PhyRayCast()
{
}


bool PhyRayCast::RaycastHit(const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
	if (m_scene == NULL)
	{
		return false;
	}
	MyQueryFilterCallback filterCallback = MyQueryFilterCallback(layerMask);
	MyRaycastCallback hitCall;
	bool res = m_scene->raycast(origin, unitDir.getNormalized(), distance, hitCall, PxHitFlag::eDEFAULT, PxQueryFilterData(PxQueryFlag::eDYNAMIC | PxQueryFlag::eSTATIC | PxQueryFlag::ePREFILTER), &filterCallback);
	if (res)
	{
		hitInfo.distance = hitCall.block.distance;
		hitInfo.point = hitCall.block.position;
		hitInfo.normal = hitCall.block.normal;
		hitInfo.actor = hitCall.block.actor;
        hitInfo.shape = hitCall.block.shape;
	}
	return res;
}


bool PhyRayCast::SphereCast(const PxReal radius, const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask)
{
    if (m_scene == NULL)
    {
        return false;
    }
    MyQueryFilterCallback filterCallback = MyQueryFilterCallback(layerMask);
    MySweepRaycastCallback hitCall;
    bool res = m_scene->sweep(PxSphereGeometry(radius), PxTransform(origin), unitDir, distance, hitCall, PxHitFlag::eDEFAULT, PxQueryFilterData(PxQueryFlag::eDYNAMIC | PxQueryFlag::eSTATIC | PxQueryFlag::ePREFILTER), &filterCallback);
    
    if(res)
    {
        hitInfo.distance = hitCall.block.distance;
        hitInfo.point = hitCall.block.position;
        hitInfo.normal = hitCall.block.normal;
        hitInfo.actor = hitCall.block.actor;
        hitInfo.shape = hitCall.block.shape;
    }
    
    return res;
}
