//
//  ContactReportCallback.cpp
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#include "ContactReportCallback.h"

ContactReportCallback::ContactReportCallback(CallbackEventMgr* pCollbackMgr):
    PxSimulationEventCallback()
{
    m_pCallbackMgr = pCollbackMgr;
}

ContactReportCallback::~ContactReportCallback(){
   
}

void ContactReportCallback::onConstraintBreak(PxConstraintInfo* constraints, PxU32 count)    {
   PX_UNUSED(constraints); PX_UNUSED(count);
   printf("onConstraintBreak\n");
}

void ContactReportCallback::onWake(PxActor** actors, PxU32 count)
{
   PX_UNUSED(actors); PX_UNUSED(count);
   printf("onWake\n");
}

void ContactReportCallback::onSleep(PxActor** actors, PxU32 count)
{ PX_UNUSED(actors); PX_UNUSED(count);
   printf("onSleep\n");
}

void ContactReportCallback::onTrigger(PxTriggerPair* pairs, PxU32 count)
{
   PX_UNUSED(pairs);
   PX_UNUSED(count);
   printf("onTrigger %d   status %d \n", count, pairs->status);
   for (uint32_t i = 0; i < count; ++i)
   {	   
	   PxTriggerPair pair = pairs[i];
	   if (pair.status == PxPairFlag::eNOTIFY_TOUCH_FOUND)
	   {
		   m_pCallbackMgr->NotifyTriggerEnter(pair.otherActor, pair.triggerActor);
		   m_pCallbackMgr->NotifyTriggerEnter(pair.triggerActor, pair.otherActor);
	   }
	   else if (pair.status == PxPairFlag::eNOTIFY_TOUCH_LOST)
	   {
		   m_pCallbackMgr->NotifyTriggerExit(pair.otherActor, pair.triggerActor);
		   m_pCallbackMgr->NotifyTriggerExit(pair.triggerActor, pair.otherActor);
	   }
   }
}

void ContactReportCallback::onAdvance(const PxRigidBody*const*, const PxTransform*, const PxU32)
{
   printf("onAdvance\n");
}

void ContactReportCallback::onContact(const PxContactPairHeader& pairHeader, const PxContactPair* pairs, PxU32 nbPairs)
{
   //printf("flags %d oncontact \n", ;
   PX_UNUSED((pairHeader));
   for (PxU32 i = 0; i<nbPairs; i++)
   {	   
       CallbackEventMgr::EuContactStep euStep = CallbackEventMgr::EuContactStep::eu_null;
       if(pairs->events & PxPairFlag::eNOTIFY_TOUCH_FOUND)
       {
           euStep = CallbackEventMgr::EuContactStep::eu_enter;
       }
       else if(pairs->events & PxPairFlag::eNOTIFY_TOUCH_PERSISTS)
       {
           euStep = CallbackEventMgr::EuContactStep::eu_stay;
       }
       else if(pairs->events & PxPairFlag::eNOTIFY_TOUCH_LOST)
       {
           euStep = CallbackEventMgr::EuContactStep::eu_exit;
       }	  
	   //printf("oncontact nbPairs %d , events %d ,  euStep %d \n", nbPairs, pairs->events, euStep);
       if(euStep != CallbackEventMgr::EuContactStep::eu_null)
       {
           m_pCallbackMgr->NotifyCollision(pairHeader.actors[0], pairs[i], pairHeader.actors[1], euStep);
           m_pCallbackMgr->NotifyCollision(pairHeader.actors[1], pairs[i], pairHeader.actors[0], euStep);
       }
   }
}
