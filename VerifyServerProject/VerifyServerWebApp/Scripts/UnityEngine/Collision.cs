using System;
using AuthServer.PhysxVerify;
using System.Runtime.InteropServices;
namespace VerifyServerWebApp.UnityEngine
{
    public struct ContactPoint
    {
        public Vector3 point;
        public Vector3 normal;
        public float separation;
    }

    public class Collision
    {
        public static Collider ConstructCollider(string tag_name, bool isTrigger)
        {
            Collider collider = new Collider();
            string[] arr = tag_name.Split('%');
            if (arr.Length == 2)
            {
                collider.tag = arr[0];
                collider.name = arr[1];
            }
            else
            {
                collider.name = tag_name;
                collider.tag = string.Empty;
            }
            collider.isTrigger = isTrigger;
            return collider;
        }
        public Collision(IntPtr p_collision)
        {
            PxCollision oCol = Marshal.PtrToStructure<PxCollision>(p_collision);
            string tag_Name = Marshal.PtrToStringAnsi(oCol.name);
            

            //bool isTrigger = PhysicsInvoke

            collider = ConstructCollider(tag_Name, false);
            // Console.WriteLine(" " + (int)oCol.dominanceGroup + " hit name " + collider.name + " tag " + collider.tag + "  " + oCol.contactCount);
            contacts = new ContactPoint[oCol.contactCount];
            int off = Marshal.SizeOf(typeof(PxContactPoint));
            for(int i = 0; i < oCol.contactCount; ++i)
            {
                IntPtr contactPtr = IntPtr.Add(oCol.contacts, i*off);
                PxContactPoint pxcontactPoint = Marshal.PtrToStructure<PxContactPoint>(contactPtr);
                contacts[i] = new ContactPoint();
                contacts[i].normal = PxVec3.ToVector3(pxcontactPoint.normal);
                contacts[i].point = PxVec3.ToVector3(pxcontactPoint.point);
                contacts[i].separation = pxcontactPoint.separation;
            }
            rigidbody = new Rigidbody(oCol.pRigidbody);

            transform = new Transform(oCol.pTransform);
        }

        public Collider collider { get; }
        public ContactPoint[] contacts { get; }
       
        //public GameObject gameObject { get; }
       
        //public Vector3 relativeVelocity { get; }
        public Rigidbody rigidbody { get; }
        public Transform transform { get; }

        //public virtual IEnumerator GetEnumerator();
    }
}
