//
//  PhyRaycast.h
//  TestPhysx
//
//  Created by doodle on 2020/10/23.
//  Copyright © 2020 doodle. All rights reserved.
//

#ifndef PhyRaycast_h
#define PhyRaycast_h
#include "PxScene.h"
#include "PxQueryReport.h"
#include "PxShape.h"
using namespace physx;


struct RayCastHit {
	PxRigidActor*   actor;
	PxVec3          point;
	PxVec3          normal;
	float           distance;
    PxShape*        shape;
};

class MyRaycastCallback : public PxRaycastCallback
{
public:
	MyRaycastCallback();
	~MyRaycastCallback();

	PxAgain processTouches(const PxRaycastHit* buffer, PxU32 nbHits) override;
};

class MySweepRaycastCallback: public PxSweepCallback
{
public:
    MySweepRaycastCallback();
    ~MySweepRaycastCallback();
    
    PxAgain processTouches(const PxSweepHit* buffer, PxU32 nbHits) override;
};

class MyQueryFilterCallback : public PxQueryFilterCallback
{
private:
	int m_iLayerMask;
public:
	MyQueryFilterCallback(int layerMask);
	~MyQueryFilterCallback();

	virtual PxQueryHitType::Enum preFilter(
		const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags);

	virtual PxQueryHitType::Enum postFilter(const PxFilterData& filterData, const PxQueryHit& hit);

};

class PhyRayCast
{
private:
	PxScene*           m_scene;
public:
	PhyRayCast(PxScene* pScene);
	~PhyRayCast();
	bool RaycastHit(const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask);
    
    bool SphereCast(const PxReal radius, const PxVec3 &origin, const PxVec3 &unitDir, const PxReal distance, RayCastHit& hitInfo, int layerMask);
};

#endif /* PhyRaycast_h */
