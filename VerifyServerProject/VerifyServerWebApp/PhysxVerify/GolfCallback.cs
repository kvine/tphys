using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
// using System.Threading.Tasks;

namespace AuthServer.PhysxVerify
{
    public class GolfCallback
    {
        private GolfLogic m_oGolfLogic;
        public GolfCallback(GolfLogic oGolfLogic)
        {
            m_oGolfLogic = oGolfLogic;
        }
    
        public void OnFixedUpdate(float dt)
        {
            // Console.WriteLine("c# on fixed update " + dt);
            if(m_oGolfLogic!= null){
                m_oGolfLogic.FixedUpdate();
            }
        }

        public void OnCollisionEnter(IntPtr p_collision)
        {
            //Console.WriteLine("c# OnCollisionEnter ");
            if(m_oGolfLogic!= null){
                m_oGolfLogic.OnCollisionEnter(new VerifyServerWebApp.UnityEngine.Collision(p_collision));
            }
        }
        public void OnCollisionStay(IntPtr p_collision) 
        {
            // Console.WriteLine("c# OnCollisionStay ");
            if(m_oGolfLogic!= null){
                m_oGolfLogic.OnCollisionStay(new VerifyServerWebApp.UnityEngine.Collision(p_collision));
            }
        }
        public void OnCollisionExit(IntPtr p_collision) 
        {
            //Console.WriteLine("c# OnCollisionExit ");
            m_oGolfLogic.OnCollisionExit(new VerifyServerWebApp.UnityEngine.Collision(p_collision));
        }

        public void OnTriggerEnter(IntPtr p_colider)
        {

        }
        public void OnTriggerStay(IntPtr p_colider)
        {

        }

        public void OnTriggerExit(IntPtr p_colider)
        {

        }
    }
}
