//
//  ContactModifyCallback.cpp
//  TestPhysx
//
//  Created by doodle on 2020/10/21.
//  Copyright © 2020 doodle. All rights reserved.
//

#include "ContactModifyCallback.h"
ContactModifyCallback::ContactModifyCallback(CallbackEventMgr* pCollbackMgr)
    :PxContactModifyCallback()
{
    m_pCallbackMgr = pCollbackMgr;
}

ContactModifyCallback::~ContactModifyCallback(){
    
}

void ContactModifyCallback::onContactModify(PxContactModifyPair* const pairs, PxU32 count)
{
    for(PxU32 i = 0; i < count ; i++)
    {
        m_pCallbackMgr->ModifyContact(pairs[i].actor[0], pairs[i].contacts, pairs[i].actor[1], pairs[i].transform[1]);
        m_pCallbackMgr->ModifyContact(pairs[i].actor[1], pairs[i].contacts, pairs[i].actor[0], pairs[i].transform[0]);
    }
}

CCDContactModifyCallback::CCDContactModifyCallback(CallbackEventMgr* pCollbackMgr)
    : PxCCDContactModifyCallback()
{
    m_pCallbackMgr = pCollbackMgr;
}

CCDContactModifyCallback::~CCDContactModifyCallback(){}

void CCDContactModifyCallback::onCCDContactModify(PxContactModifyPair* const pairs, PxU32 count)
{
    printf("onCCDModify %d  \n", count);
    for(PxU32 i = 0; i < count ; i++)
    {
        m_pCallbackMgr->ModifyContact(pairs[i].actor[0], pairs[i].contacts, pairs[i].actor[1], pairs[i].transform[1]);
        m_pCallbackMgr->ModifyContact(pairs[i].actor[1], pairs[i].contacts, pairs[i].actor[0], pairs[i].transform[0]);
    }
}
