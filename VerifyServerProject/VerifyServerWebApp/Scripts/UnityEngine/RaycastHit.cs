﻿using System;

namespace VerifyServerWebApp.UnityEngine
{
    public struct RaycastHit
    {
        //public Vector3 barycentricCoordinate { get; set; }
        public Collider collider { get; set;}
        public float distance { get; set; }
        //public Vector2 lightmapCoord { get; }
        public Vector3 normal { get; set; }
        public Vector3 point { get; set; }
        //public Rigidbody rigidbody { get; }
        //public Vector2 textureCoord { get; }
        //[Obsolete("Use textureCoord2 instead")]
        //public Vector2 textureCoord1 { get; }
        //public Vector2 textureCoord2 { get; }
        //public Transform transform { get; }
        //public int triangleIndex { get; }
    }
}
