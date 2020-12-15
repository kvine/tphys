using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerifyServerWebApp.UnityEngine;
using com.golf.proto;

namespace AuthServer.PhysxVerify
{
    public class Verify
    {
        public static CCRoundResultStatus CalculateResult(int sceneId, string sceneName, string sceneFile, CCStrickData ccStrickData, Vector3 golfHolePos)
        {
            //获得有效的闲置索引， 
            int slotIndex = PhysicsInvoke.GetIndex(sceneId);
            if(slotIndex < 0)
            {
                return CalculateResult(sceneId, sceneName, sceneFile, ccStrickData, golfHolePos);
            }

            //创建一个模拟对象
            IntPtr ptrSimulateMgr = PhysicsInvoke.getSimulationMgr(slotIndex);
            bool bInit = PhysicsInvoke.Init(ptrSimulateMgr);
            if(!bInit)
            {
                Console.WriteLine("init physx failed");
                PhysicsInvoke.BackIndex(sceneId, slotIndex);
                return null;
            }
            // Console.WriteLine("init success");

            bool bloadFile = PhysicsInvoke.LoadSceneFile(ptrSimulateMgr, sceneFile);
            if(!bloadFile)
            {
                Console.WriteLine("load scene file failed ");
                PhysicsInvoke.BackIndex(sceneId, slotIndex);
                return null;
            }
            // Console.WriteLine("load scene file success ");
            
            //1.创建golf对象，new GameObject()
            float radius = 0.04f;
            float mass = 0.5f;
            Vector3 pos = new Vector3(0, 0.1f, 0);
            PxQuat quat= new PxQuat(0, 0, 0, 1);
            PhysicsInvoke.createBall(ptrSimulateMgr, radius, mass, new PxVec3(pos), quat);
            IntPtr pRigidbody = PhysicsInvoke.GetRigidbodyPtr(ptrSimulateMgr);

            Rigidbody _oRigid = new Rigidbody(pRigidbody);
            _oRigid.SetSimulateMgr(ptrSimulateMgr);

            Transform _trs = new Transform(pRigidbody);
            _trs.SetSimulateMgr(ptrSimulateMgr);

            GameObject oGolf = new GameObject();
            oGolf.rigidbody = _oRigid;
            oGolf.transform = _trs;

            //2.new GolfLogic, 注册update,collision回调
            PhysicsUtil _oPhysicsUtil = new PhysicsUtil(ptrSimulateMgr, golfHolePos);
            DontGoThroughThings _oDontGoThrough = new DontGoThroughThings(oGolf.rigidbody, _oPhysicsUtil, radius);
            
            GolfLogic oGolfLogic = new GolfLogic(_oDontGoThrough, _oPhysicsUtil);
            oGolfLogic.gameObject = oGolf;
            oGolfLogic.rigidbody = oGolf.rigidbody;

            PhysicsStart _oPhysicsStart = new PhysicsStart(_oDontGoThrough, _oPhysicsUtil);
            GolfCallback oCallback = new GolfCallback(oGolfLogic);
            PhysicsInvoke.DelAction actionUpdate = new PhysicsInvoke.DelAction(oCallback.OnFixedUpdate);
            PhysicsInvoke.ConlisionAction collisionEnter = new PhysicsInvoke.ConlisionAction(oCallback.OnCollisionEnter);
            PhysicsInvoke.ConlisionAction collisionStay = new PhysicsInvoke.ConlisionAction(oCallback.OnCollisionStay);
            PhysicsInvoke.ConlisionAction collisionExit = new PhysicsInvoke.ConlisionAction(oCallback.OnCollisionExit);
            PhysicsInvoke.TriggerAction triggerEnter = new PhysicsInvoke.TriggerAction(oCallback.OnTriggerEnter);
            PhysicsInvoke.TriggerAction triggerStay = new PhysicsInvoke.TriggerAction(oCallback.OnTriggerStay);
            PhysicsInvoke.TriggerAction triggerExit = new PhysicsInvoke.TriggerAction(oCallback.OnTriggerExit);
            PhysicsInvoke.RegisterCallback(ptrSimulateMgr, actionUpdate,collisionEnter,collisionStay,collisionExit,triggerEnter,triggerStay,triggerExit);
            GC.KeepAlive(oCallback);
            GC.KeepAlive(actionUpdate);
            GC.KeepAlive(collisionEnter);
            GC.KeepAlive(collisionStay);
            GC.KeepAlive(collisionExit);
            GC.KeepAlive(triggerEnter);
            GC.KeepAlive(triggerStay);
            GC.KeepAlive(triggerExit);
            _oPhysicsUtil.isGolfStopMoveCall = false;
            
            bool bStrikeBall = true;
            bool bUpdate = true;

            // BetterList<Vector3> listPos = new BetterList<Vector3>();

            while(bUpdate)
            {
                PhysicsInvoke.SimulateStep(ptrSimulateMgr);

                if(bStrikeBall)
                {
                    bStrikeBall = false;
                    
                    _oPhysicsStart.Init(ccStrickData, sceneName);

                    if(ccStrickData.Type == MiscState.strick_swing)
                    {
                        PhysicsInvoke.setFlagpoleActive(ptrSimulateMgr,true);
                        _oPhysicsStart.GolfSwing(oGolf);
                    }
                    else if(ccStrickData.Type == MiscState.strick_push)
                    {
                        PhysicsInvoke.setFlagpoleActive(ptrSimulateMgr,false);
                        _oPhysicsStart.GolfPuter(oGolf);
                    }
                    else
                    {
                        Console.WriteLine("invalid params, error type " + ccStrickData.Type);
                        PhysicsInvoke.BackIndex(sceneId, slotIndex);
                        return null;
                    }
                }
                else if(_oPhysicsUtil.isGolfStopMoveCall)
                {
                    break;
                }
                GC.KeepAlive(ptrSimulateMgr);
                //Console.WriteLine("pos " + oGolf.rigidbody.position.ToString());
                // listPos.Add(oGolf.rigidbody.position);
            }

            for(int i = 0; i < 3; ++i)
            {
                PhysicsInvoke.SimulateStep(ptrSimulateMgr);
            }
            
            pos = oGolf.rigidbody.position;
            Quaternion rotation = oGolf.rigidbody.rotation;

            
            Console.WriteLine("end pos is " + pos.x + " " + pos.y + " " + pos.z);
            
            // string points = string.Empty;
            // for(int i = 0; i < listPos.size; ++i)
            // {
            //     points += "new Vector3" + listPos[i].ToString() + ",";
            // }
            // Console.WriteLine("-------------physx points--------- \n" + points);

            // Console.WriteLine("--------------simulate points-----------------------");
            // points = string.Empty;
            // for(int i = 0; i < PhysicsUtil.hitGolfSimuPointList.size; ++i)
            // {
            //     points += "new Vector3" + PhysicsUtil.hitGolfSimuPointList[i].ToString() + ",";
            // }
            // Console.WriteLine("" + points);
            int localState = Tags.GetGolfStopLocState(_oPhysicsUtil.golfStopArea);
            int strick_type = _oPhysicsUtil.GetStrickType();
            bool has_bounced = _oPhysicsUtil.HasBounced();
            bool start_on_green = _oPhysicsUtil.IsStartOnGreen(ptrSimulateMgr);
            int ball_area = _oPhysicsUtil.BallArea();
	        //optional string scene_name=5;
            // Console.WriteLine("strick_type " + strick_type);
            // Console.WriteLine("has_bounced " + has_bounced);
            // Console.WriteLine("start_on_green " + start_on_green);
            // Console.WriteLine("ball_area " + ball_area);
            CCStrickResultStatus ccResulatStatus = CCStrickResultStatus.CreateBuilder().SetStrickType(strick_type).SetHasBounced(
                has_bounced).SetStartOnGreen(start_on_green).SetBallArea(ball_area).SetSceneName(sceneName).Build();
            const int factor = 10000;

            CCVec3FloatToLong ccPos = CCVec3FloatToLong.CreateBuilder().SetX((long)(pos.x*factor)).SetY((long)(pos.y*factor)
                ).SetZ((long)(pos.z*factor)).Build();
            CCVec4FloatToLong ccRot = CCVec4FloatToLong.CreateBuilder().SetX((long)(rotation.x*factor)).SetY((long)(rotation.y*factor)
            ).SetZ((long)(rotation.z*factor)).SetW((long)(rotation.w*factor)).Build();
            CCBallStatus ballStatus = CCBallStatus.CreateBuilder().SetPosition(ccPos).SetRotation(ccRot).Build();

            CCBallStatusEx ccStatusEx = CCBallStatusEx.CreateBuilder().SetStatus(ballStatus).SetLocState(localState).Build();

            CCRoundResultStatus cCRoundResultStatus= CCRoundResultStatus.CreateBuilder().SetBallStatusEx(ccStatusEx).SetStrickRs(
                ccResulatStatus).Build();
            PhysicsInvoke.cleanCallback(ptrSimulateMgr);
            PhysicsInvoke.BackIndex(sceneId, slotIndex);
            return cCRoundResultStatus;
        }
    }
}
