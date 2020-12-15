using System;
using AuthServer.PhysxVerify;
using System.Runtime.InteropServices;

namespace VerifyServerWebApp.UnityEngine
{
    public class Rigidbody
    {
        IntPtr m_pRigidbody;
        IntPtr m_ptrSimulateMgr;
        bool m_bInitSimulateMgr;
        public Rigidbody(IntPtr pRigidbody)
        {
            m_pRigidbody = pRigidbody;
            m_bInitSimulateMgr = false;;
        }

        public void SetSimulateMgr(IntPtr ptrSimulateMgr)
        {
            m_bInitSimulateMgr = true;
            m_ptrSimulateMgr = ptrSimulateMgr;
        }

        public IntPtr SimulateMgr
        {
            get{return m_ptrSimulateMgr;}
        }
        IntPtr ptrRigidbody
        {
            get{
                if(m_bInitSimulateMgr && PhysicsInvoke.isNullRigidbody(m_pRigidbody))
                {
                    m_pRigidbody = PhysicsInvoke.GetRigidbodyPtr(m_ptrSimulateMgr); 
                }
                return m_pRigidbody;
            }
        }

        public Vector3 angularVelocity 
        { 
            get{
                return PxVec3.ToVector3(PhysicsInvoke.getAngularVelocity(ptrRigidbody));
            }
            set{ 
                PhysicsInvoke.setAngularVelocity(ptrRigidbody, new PxVec3(value));
            }
        }
    
        public bool isKinematic 
        { 
            get{
                return PhysicsInvoke.getIsKinematic(ptrRigidbody);
            }
            set{
                PhysicsInvoke.setIsKinematic(ptrRigidbody, value);
            }
        }
        public float mass 
        { 
            get{
                return PhysicsInvoke.getMass(ptrRigidbody);
            }
            set{
                PhysicsInvoke.setMass(ptrRigidbody, value);
            }
        }
        
        public Vector3 position 
        { 
            get
            {
                return PxVec3.ToVector3(PhysicsInvoke.getGlobalPos(ptrRigidbody).p);
            }

            set
            {
                PxTransform pxTrans = PhysicsInvoke.getGlobalPos(ptrRigidbody);
                pxTrans.p = new PxVec3(value); 
                PhysicsInvoke.setGlobalPos(ptrRigidbody, pxTrans);
            }
        }

        public Quaternion rotation
        {
            get
            {
                return PxQuat.toQuat(PhysicsInvoke.getGlobalPos(ptrRigidbody).q);
            }

            set
            {
                PxTransform pxTrans = PhysicsInvoke.getGlobalPos(ptrRigidbody);
                pxTrans.q = new PxQuat(value); 
                PhysicsInvoke.setGlobalPos(ptrRigidbody, pxTrans);
            }
        }
       
        public Vector3 velocity 
        { 
            get{
                return PxVec3.ToVector3(PhysicsInvoke.getLinearVelocity(ptrRigidbody));
            }
            set{ 
                PhysicsInvoke.setLinearVelocity(ptrRigidbody, new PxVec3(value));
            }
        }

        public bool IsSleeping()
        {
            return PhysicsInvoke.isSleeping(ptrRigidbody);
        }
       
        public void WakeUp()
        { 
            PhysicsInvoke.wakeUp(ptrRigidbody);
        }
    }
}
