using System;
using System.Collections.Generic;
using AuthServer.PhysxVerify;
using System.Runtime.InteropServices;

namespace VerifyServerWebApp.UnityEngine
{
    public class Physics
    {
        private static bool PtrToRaycastHit(bool bHit, IntPtr ptrHitInfo,out RaycastHit hitInfo)
        {
            hitInfo = new RaycastHit();
            if(bHit)
            {    
                RayCastHit tmpHit = Marshal.PtrToStructure<RayCastHit>(ptrHitInfo);
                hitInfo.normal = PxVec3.ToVector3(tmpHit.normal);
                hitInfo.point = PxVec3.ToVector3(tmpHit.point);
                hitInfo.distance = tmpHit.distance;
                string tag_name = PhysicsInvoke.getActorName(tmpHit.actor);
                //Console.WriteLine("tag_name " + tag_name);
                bool isTrigger = PhysicsInvoke.isTrigger(tmpHit.shape);
                hitInfo.collider = Collision.ConstructCollider(tag_name,isTrigger);
                // PhysicsInvoke.printfShapeMaterial(tmpHit.shape);
            }
            return bHit;
        }

        private static bool RaycastHitCall(IntPtr ptrSimulateMgr, Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layerMask)
        {
            direction = direction.normalized;
            IntPtr ptrRaycast = PhysicsInvoke.getRaycasthit(ptrSimulateMgr);
            bool res = PhysicsInvoke.RaycastHitCall(ptrSimulateMgr, new PxVec3(origin), new PxVec3(direction), distance, layerMask, ptrRaycast);

            return PtrToRaycastHit(res, ptrRaycast, out hitInfo);
        }

        private static bool SphereRaycastHitCall(IntPtr ptrSimulateMgr, float radius, Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layerMask)
        {
            direction = direction.normalized;
            IntPtr ptrRaycast = PhysicsInvoke.getRaycasthit(ptrSimulateMgr);
            bool res = PhysicsInvoke.SphereRaycastHitCall(ptrSimulateMgr, radius,new PxVec3(origin), new PxVec3(direction), distance, layerMask, ptrRaycast);

            return PtrToRaycastHit(res, ptrRaycast, out hitInfo);
        }

        public static Vector3 gravity = new Vector3(0, -9.81f, 0);

        public static bool Raycast(IntPtr ptrSimulateMgr, Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layerMask)
        {
            return RaycastHitCall(ptrSimulateMgr, origin, direction, out hitInfo, distance, layerMask);
        }

        public static bool Raycast(IntPtr ptrSimulateMgr, Ray ray, out RaycastHit hitInfo, float distance, int layerMask)
        {
            return RaycastHitCall(ptrSimulateMgr, ray.origin, ray.direction, out hitInfo, distance, layerMask);
        }


        public static bool SphereCast(IntPtr ptrSimulateMgr, Ray ray, float radius, out RaycastHit hitInfo, float distance, int layerMask)
        {
           return SphereRaycastHitCall(ptrSimulateMgr, radius, ray.origin, ray.direction, out hitInfo, distance, layerMask);
        }
    }
}
