using System;

namespace VerifyServerWebApp.UnityEngine
{
    public class GameObject : Object
    {
        public GameObject()
        {

        }
        //public Collider collider { get; }
       
        //public GameObject gameObject { get; }
        
        //public bool isStatic { get; set; }
        //public int layer { get; set; }
       
        public Rigidbody rigidbody { get; set; }
      
        //public string tag { get; set; }
        public Transform transform { get; set; }
    }
}
