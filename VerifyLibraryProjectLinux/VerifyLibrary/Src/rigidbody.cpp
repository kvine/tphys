//
//  rigidbody.cpp
//  PxVisualization
//
//  Created by doodle on 2020/10/13.
//  Copyright © 2020 com.TN. All rights reserved.
//

#include "rigidbody.h"
using namespace physx;

Rigidbody::Rigidbody(PxRigidDynamic* pRigidDynamic)
{
    m_pRigidDynamic = pRigidDynamic;
    
    /*
      CCD friction was enabled in previous versions of the SDK. Raising this flag will result in behavior
    that is a closer match for previous versions of the SDK.
    */
//    m_pRigidDynamic->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD_FRICTION, true);
    
    //lock the motion
    //m_pRigidDynamic->setRigidDynamicLockFlag(<#PxRigidDynamicLockFlag::Enum flag#>, <#bool value#>)
    //阻力
    m_pRigidDynamic->setAngularDamping(0);
    m_pRigidDynamic->setLinearDamping(0);
    
}

Rigidbody::~Rigidbody()
{
	m_pRigidDynamic->release();
}

void Rigidbody::SetName(const char* name)
{
	m_pRigidDynamic->setName(name);
}

void Rigidbody::SetMass(PxReal mass)
{
	m_pRigidDynamic->setMass(mass);
    PxRigidBodyExt::setMassAndUpdateInertia(*m_pRigidDynamic, &mass, 1);
//	PxVec3 v = m_pRigidDynamic->getMassSpaceInertiaTensor();
//	printf("InertiaTensor %f, %f, %f \n", v.x, v.y, v.z);
}

void Rigidbody::SetKinematic(bool bKinematic)
{
   if(m_pRigidDynamic != nullptr)
   {
       if(bKinematic && (m_euCollisionDetection == EuCollsionDetection::eu_continuous || m_euCollisionDetection == EuCollsionDetection::eu_continuous_dynamic))
       {
           SetRigidFlag(false);
       }
       m_pRigidDynamic->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, bKinematic);
   }
   else
   {
       printf("m_pRigidDynamic is null \n");
   }
}

void Rigidbody::SetGravity(bool useGravity)
{
    //注：模拟过程中，更改重力后，需要遍历所有参与者并手动调用PxRigidDynamic::WakeUp()
    if(m_pRigidDynamic != nullptr)
	{
        m_bUseGravity = useGravity;
        m_pRigidDynamic->setActorFlag(PxActorFlag::eDISABLE_GRAVITY, !useGravity);
    }
}

void Rigidbody::SetCollisionDetection(EuCollsionDetection flag, PhysicsSceneManager* pPhysxSceneMgr)
{
    if(m_pRigidDynamic != nullptr)
	{
        //取消掉旧的状态
        SetRigidFlag(false);
        
        m_euCollisionDetection = flag;
        
        //新的碰撞检测方式
        SetRigidFlag(true);
        
        if(m_euCollisionDetection == EuCollsionDetection::eu_continuous_dynamic)
        {
            pPhysxSceneMgr->DisableCCDResweep(true);
        }
    }
}

void Rigidbody::SetRigidFlag(bool active)
{
    switch (m_euCollisionDetection)
    {
       case EuCollsionDetection::eu_continuous:
           m_pRigidDynamic->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, active);
           break;
       case EuCollsionDetection::eu_continuous_dynamic:
           m_pRigidDynamic->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, active);
           break;
       case EuCollsionDetection::eu_continuous_speculative:
          m_pRigidDynamic->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_SPECULATIVE_CCD, active);
          break;
       case EuCollsionDetection::eu_discrete:
           break;
       default:
           break;
    }
}

void Rigidbody::AddForce(const PxVec3& force, PxForceMode::Enum mode)
{
    if(m_pRigidDynamic != nullptr)
    {
        m_pRigidDynamic->addForce(force, mode);
    }
}

void Rigidbody::SetGlobalPos(const PxTransform& transform)
{
    if(m_pRigidDynamic != nullptr)
    {
        m_pRigidDynamic->setGlobalPose(transform);
    }
}

bool Rigidbody::IsKinematic()
{
    return m_pRigidDynamic->getRigidBodyFlags().isSet(PxRigidBodyFlag::eKINEMATIC);
}

void Rigidbody::WakeUp()
{
    m_pRigidDynamic->wakeUp();
}

void Rigidbody::Sleep()
{
    m_pRigidDynamic->putToSleep();
}

bool Rigidbody::IsSleeping()
{
    return m_pRigidDynamic->isSleeping();
}

void Rigidbody::SetLayer(PxDominanceGroup layer)
{
    m_pRigidDynamic->setDominanceGroup(layer);
}

PxDominanceGroup Rigidbody::Layer()
{
    return m_pRigidDynamic->getDominanceGroup();
}
