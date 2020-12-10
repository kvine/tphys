using System.Collections;
using com.golf.proto;
using VerifyServerWebApp.UnityEngine;
using System;
public class PhysicsStart
{

    private CCStrickData ccStrickData;
    private string sceneName;

    private DontGoThroughThings m_oDontGoThrough;
    private PhysicsUtil m_oPhysicsUtil;

    public PhysicsStart(DontGoThroughThings oDontGoThrough, PhysicsUtil oPhysicsUtil)
    {
        m_oDontGoThrough = oDontGoThrough;
        m_oPhysicsUtil = oPhysicsUtil;
    }

    public void Init(CCStrickData pStrickData, string pSceneName)
    {
        ccStrickData = pStrickData;
        sceneName = pSceneName;
    }

    /// <summary>
    /// 挥杆
    /// </summary>
    /// <param name="ball"></param>
    public void GolfSwing(GameObject ball)
    {
        ball.rigidbody.isKinematic = true;
        m_oPhysicsUtil.InitHitGolfSimu();

        GenStrickPackedData(ccStrickData);

        m_oPhysicsUtil.CalculateHitPointAndPreditGolfPath();

        GolfSwing_RealPhysics(ball);
    }

    private void GenStrickPackedData(CCStrickData ccStrickData)
    {
        CCStrickDataLocal ccStrickDataLocal = m_oPhysicsUtil.strickDataPacked;

        ccStrickDataLocal.type = ccStrickData.Type;
        ccStrickDataLocal.force = ccStrickData.Force;
        ccStrickDataLocal.pointerAngle = ccStrickData.PointerAngle;
        ccStrickDataLocal.angle = ccStrickData.Angle;
        ccStrickDataLocal.clubID = ccStrickData.ClubId;
        ccStrickDataLocal.ballId = ccStrickData.BallId;
        ccStrickDataLocal.hitPoint.Set(ccStrickData.HitPoint);
        ccStrickDataLocal.wind.Set(ccStrickData.Wind);
        ccStrickDataLocal.landingPos.Set(ccStrickData.LandingPos);
        ccStrickDataLocal.ballPos.Set(ccStrickData.BallPos);
        ccStrickDataLocal.ballRot.Set(ccStrickData.BallRot);
        ccStrickDataLocal.clubLv = ccStrickData.ClubLv;
        ccStrickDataLocal.ClubAttributes = ccStrickData.ClubAttributesList;
        ccStrickDataLocal.BallSkills = ccStrickData.BallSkillsList;
        ccStrickDataLocal.clubAngle = ccStrickData.ClubAngle;

        m_oPhysicsUtil.TransformPackValueToUnPackValue();
    }

    /// <summary>
    /// 挥杆直接获得最后的模拟点的数据， 开始真实物理
    /// </summary>
    /// <param name="ball"></param>
    private void GolfSwing_RealPhysics(GameObject ball)
    {
        int lastIndex = m_oPhysicsUtil.hitGolfSimuPointList.size - 1;

        ball.rigidbody.isKinematic = false;

        ball.transform.position = m_oPhysicsUtil.hitGolfSimuPointList[lastIndex];
        ball.transform.rotation = m_oPhysicsUtil.hitGolfSimuRotList[lastIndex];
        ball.rigidbody.velocity = m_oPhysicsUtil.hitGolfSimuVelocityList[lastIndex];
        ball.rigidbody.angularVelocity = m_oPhysicsUtil.hitGolfLastSimuAngularVelocity;

        // ball.transform.position = new Vector3(3.819190f, -4.318446f, 75.055510f);
        // ball.transform.rotation =  new Quaternion(-0.424403f, 0.003463f, 0.046099f, 0.904292f);
        // ball.rigidbody.velocity = new Vector3(0.132185f, 4.291435f, 5.026379f);
        // ball.rigidbody.angularVelocity = new Vector3(4.998272f, 0.000000f, -0.131446f);
        // Console.WriteLine("RealPhysics1111 pos " + PhysicsUtil.hitGolfSimuPointList[lastIndex].ToString() + "  velocity " + PhysicsUtil.hitGolfSimuVelocityList[lastIndex].ToString() + " angularVelocity " + PhysicsUtil.hitGolfLastSimuAngularVelocity.ToString());
        Console.WriteLine("RealPhysics2222 pos " + ball.transform.position.ToString() + " rot  " + ball.transform.rotation.ToString() + "  velocity " + ball.rigidbody.velocity.ToString() + " angularVelocity " + ball.rigidbody.angularVelocity.ToString());
        m_oDontGoThrough.updateRigidBodyPreviousPos();
    }

    /// <summary>
    /// 推杆
    /// </summary>
    /// <param name="ball"></param>
    public void GolfPuter(GameObject ball)
    {
        ball.rigidbody.isKinematic = true;

        m_oPhysicsUtil.InitPushGolfSimu();

        GenStrickPackedData(ccStrickData);

        m_oPhysicsUtil.SimuPushGolfRoadPath(GameConfig.golf_collide_radiu, ball.rigidbody.mass);

        GolfPuter_RealPhysics(ball);
    }

    /// <summary>
    /// 获取最后模拟点的数据， 然后切换到真实物理
    /// </summary>
    private void GolfPuter_RealPhysics(GameObject ball)
    {
        m_oPhysicsUtil.PushGolfMoveForServer(ball, m_oDontGoThrough);
    }
}
