using System;
using System.Collections;
using VerifyServerWebApp.UnityEngine;
public class DontGoThroughThings
{
   // Careful when setting this to true - it might cause double
   // events to be fired - but it won't pass through the trigger
//    public bool sendTriggerMessage = false;

   private LayerMask layerMask = -1; //make sure we aren't in this layer 
   public float skinWidth = 0.1f; //probably doesn't need to be changed 

   
   private float partialExtent;
   private float sqrMinimumExtent;
   private Vector3 previousPosition;
   private Rigidbody myRigidbody;
   //private Collider myCollider;

   //initialize values 
    private PhysicsUtil m_oPhysicsUtil;

   public DontGoThroughThings(Rigidbody rigidbody,PhysicsUtil oPhysicsUtil, float minimumExtent)
   {
       m_oPhysicsUtil = oPhysicsUtil;

       myRigidbody = rigidbody;//GetComponent<Rigidbody>();
       //myCollider = GetComponent<Collider>();
       previousPosition = myRigidbody.position;
       partialExtent = minimumExtent * (1.0f - skinWidth);
       sqrMinimumExtent = minimumExtent * minimumExtent;

       layerMask = GameMainLogic.GetAllCollisionLayer();
   }

   public void updateRigidBodyPreviousPos()
   {
       if(myRigidbody != null)
       {
           previousPosition = myRigidbody.position;
       }
   }

   public void FixedUpdate()
   {
       //Console.WriteLine("ball pos " + myRigidbody.position.ToString());
       if (myRigidbody.isKinematic)
       {
           previousPosition = myRigidbody.position;
           return;
       }

       {
           bool isGoThrough = CheckThroughJieWaiOrWater();
           if (isGoThrough)
           {
               return;
           }
       }


       //have we moved more than our minimum extent? 
       Vector3 movementThisStep = myRigidbody.position - previousPosition;
       float movementSqrMagnitude = movementThisStep.sqrMagnitude;

       if (movementSqrMagnitude > sqrMinimumExtent)
       {
           float movementMagnitude = (float)Mathf.Sqrt(movementSqrMagnitude);
           RaycastHit hitInfo;

           bool hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value);
           if((!hasGoThrough) && ConfigInfo.config.UseNewPhysics)
           {
               hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, movementThisStep, out hitInfo, movementMagnitude + PhysicsUtil.safe_ray_cast_add_dis, layerMask.value);
           }

           //check for obstructions we might have missed 
           if (hasGoThrough)
           {
               if (hitInfo.collider == null)
                   return;
                //todo:
            //    if (hitInfo.collider.isTrigger)
            //        hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);

               if (!hitInfo.collider.isTrigger)
                   myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;

           }
       }


       ReCheckGoThrough();

       previousPosition = myRigidbody.position;
   }


   /// <summary>
   /// 先检查是否穿过界外outer_bounds或者水，如果穿过，则将球停止。
   /// </summary>
   /// <returns></returns>
   private bool CheckThroughJieWaiOrWater()
   {
       Vector3 moveStep = myRigidbody.position - previousPosition;
       RaycastHit hitInfo;

       bool hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, moveStep, out hitInfo, moveStep.magnitude, layerMask.value);
       if((!hasGoThrough) && ConfigInfo.config.UseNewPhysics)
       {
           hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, moveStep, out hitInfo, moveStep.magnitude + PhysicsUtil.safe_ray_cast_add_dis, layerMask.value);
       }

       if (hasGoThrough)
       {
           string collideTag = hitInfo.collider.tag;
           Tags.GolfArea golfStopArea = Tags.GolfArea.jie_wai_outer_bounds;
           bool isStop = false;

           if (collideTag.Equals(Tags.jie_wai_outer_bounds))
           {
               golfStopArea = Tags.GolfArea.jie_wai_outer_bounds;
               isStop = true;
           }
           else if (collideTag.Equals(Tags.shui))
           {
               golfStopArea = Tags.GolfArea.shui;
               isStop = true;
           }

           if (isStop)
           {
               myRigidbody.position = hitInfo.point + hitInfo.normal * GameConfig.golf_collide_radiu;
               //myRigidbody.gameObject.transform.position = myRigidbody.position;

               m_oPhysicsUtil.setGolfStopFlag(myRigidbody, golfStopArea);

               return true;
           }
       }

       return false;
   }

   /// <summary>
   /// 经过常规的DontGoThrough处理后，可能还是会有穿过现象
   /// 这时候, 反弹物体。
   /// </summary>
   private void ReCheckGoThrough()
   {
       Vector3 movementThisStep = myRigidbody.position - previousPosition;
       float movementMagnitude = movementThisStep.magnitude;

       if (movementMagnitude >= 0.0001f)
       {
           RaycastHit hitInfo;

           bool hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value);
           if((!hasGoThrough) && ConfigInfo.config.UseNewPhysics)
           {
               hasGoThrough = Physics.Raycast(myRigidbody.SimulateMgr, previousPosition, movementThisStep, out hitInfo, movementMagnitude + PhysicsUtil.safe_ray_cast_add_dis, layerMask.value);
           }

           //check for obstructions we might have missed 
           if (hasGoThrough)
           {
               if (hitInfo.collider != null)
                   return;

               if (!hitInfo.collider.isTrigger)
               {
                   myRigidbody.position = hitInfo.point + hitInfo.normal * GameConfig.golf_collide_radiu;

                   Vector3 bounsV = Vector3.Reflect(myRigidbody.velocity, hitInfo.normal) * PhysicsUtil.getBounciness(hitInfo.collider.tag);
                   if (hitInfo.collider.tag.Equals(Tags.sha_keng))
                   {
                       bounsV.y = 0;
                   }

                   myRigidbody.velocity = bounsV;
               }
           }
       }
   }
}