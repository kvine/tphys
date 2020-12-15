using System;
namespace VerifyServerWebApp.UnityEngine
{
    public class Collider : Component
    {
        public Collider() { }

        //public Rigidbody attachedRigidbody { get; }
        //public Bounds bounds { get; }
        //public bool enabled { get; set; }
        public bool isTrigger { get; set; }
        //public PhysicMaterial material { get; set; }
        //public PhysicMaterial sharedMaterial { get; set; }

        //public Vector3 ClosestPointOnBounds(Vector3 position);
        //public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance);
    }
}
