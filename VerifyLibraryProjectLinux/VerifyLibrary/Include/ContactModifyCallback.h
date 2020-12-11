//
//  ContactModifyCallback.h
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#ifndef ContactModifyCallback_h
#define ContactModifyCallback_h
#include "CallbackEventMgr.h"
#include "PxContactModifyCallback.h"
using namespace physx;

class ContactModifyCallback: public PxContactModifyCallback
{
private:
    CallbackEventMgr* m_pCallbackMgr;
public:
    ContactModifyCallback(CallbackEventMgr* pCollbackMgr);
        
    ~ContactModifyCallback();
    
    void onContactModify(PxContactModifyPair* const pairs, PxU32 count);
};

class CCDContactModifyCallback : public PxCCDContactModifyCallback
{
private:
    CallbackEventMgr* m_pCallbackMgr;
public:
    CCDContactModifyCallback(CallbackEventMgr* pCollbackMgr);

    ~CCDContactModifyCallback();

    void onCCDContactModify(PxContactModifyPair* const pairs, PxU32 count);
};

#endif /* ContactModifyCallback_h */
