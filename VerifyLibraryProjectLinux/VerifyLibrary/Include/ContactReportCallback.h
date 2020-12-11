//
//  ContactReportCallback.h
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#ifndef ContactReportCallback_h
#define ContactReportCallback_h
#include "CallbackEventMgr.h"
#include "PxSimulationEventCallback.h"
using namespace physx;

class ContactReportCallback : public PxSimulationEventCallback
{
    CallbackEventMgr* m_pCallbackMgr;
    
public:
    ContactReportCallback(CallbackEventMgr* pCollbackMgr);

    ~ContactReportCallback();
    void onConstraintBreak(PxConstraintInfo* constraints, PxU32 count);
    
    void onWake(PxActor** actors, PxU32 count);
    
    void onSleep(PxActor** actors, PxU32 count);
    
    void onTrigger(PxTriggerPair* pairs, PxU32 count);
    
    void onAdvance(const PxRigidBody*const*, const PxTransform*, const PxU32);
    
    void onContact(const PxContactPairHeader& pairHeader, const PxContactPair* pairs, PxU32 nbPairs);
};

#endif /* ContactReportCallback_h */
