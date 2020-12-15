using System;
using System.Collections;
using VerifyServerWebApp.UnityEngine;

public class GolfLogic: MonoBehaviour
{
    private float qiuDaoVelocityDec = qiu_dao_v_init_dec;
    public const float qiu_dao_v_init_dec = 0.999f;
    public const float qiu_dao_v_dec_f = 0.001f;

    private float changCaoQuVelocityDec = chang_cao_qu_v_init_dec;
    public const float chang_cao_qu_v_init_dec = 0.99f;
    public const float chang_cao_qu_v_dec_f = 0.001f;

    public const float sha_keng_v_dec_f = 0.8f;
    public const float guo_ling_bian_yuan_v_dec_f = 0.995f;

    public const float jie_wai_v_dec_f = 0.9f;

    private float curJieWaiRollTime = 0;
    private const float maxJieWaiRollTime = 2;

    private DontGoThroughThings m_oDontGoThrough;
    private PhysicsUtil m_oPhysicsUtil;
    // Use this for initialization
    public GolfLogic(DontGoThroughThings oDontGoThrough,PhysicsUtil oPhysicsUtil)
    {
        m_oDontGoThrough = oDontGoThrough;
        m_oPhysicsUtil = oPhysicsUtil;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //  Console.WriteLine("OnCollisionEnter isKinematic " + gameObject.rigidbody.isKinematic + " tag " + collision.collider.tag);
        if (collision.collider.tag.Equals("Untagged"))
        {
            return;
        }

        string tag = collision.collider.tag;


        switch (tag)
        {
            case Tags.guo_ling:
                break;
            case Tags.guo_ling_bian_yuan:
                break;
            case Tags.qiu_dao:
                qiuDaoVelocityDec = qiu_dao_v_init_dec;
                break;
            case Tags.chang_cao_qu:
                changCaoQuVelocityDec = chang_cao_qu_v_init_dec;
                break;
            case Tags.sha_keng:

                {
                    Vector3 normal = -collision.contacts[0].normal.normalized;
                    float angleInDeg = Vector3.Angle(normal, rigidbody.velocity);
                    float vyLen = Mathf.Cos(angleInDeg * Mathf.Deg2Rad) * rigidbody.velocity.magnitude;
                    rigidbody.velocity = rigidbody.velocity - normal * vyLen;
                }
                break;
            case Tags.shui:
                m_oPhysicsUtil.setGolfStopFlag(gameObject.rigidbody, Tags.GolfArea.shui);
                break;
            case Tags.jie_wai:
                curJieWaiRollTime = 0;
                break;
            case Tags.jie_wai_outer_bounds:
                m_oPhysicsUtil.setGolfStopFlag(gameObject.rigidbody, Tags.GolfArea.jie_wai_outer_bounds);
                break;
            case Tags.hole:
                m_oPhysicsUtil.setGolfStopFlag(gameObject.rigidbody, Tags.GolfArea.hole);
                break;
            case Tags.qiu_dong_qiang_bi:
                break;
            case Tags.qiu_zuo:
                break;
            case Tags.fa_qiu_tai_di_mian:
                break;
            case Tags.shu_zhi:
                break;
            case Tags.shu_gan:
                break;
            case Tags.qi_gan:
                break;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        // Console.WriteLine("OnCollisionStay isKinematic " + gameObject.rigidbody.isKinematic + " tag " + collision.collider.tag);
        // Console.WriteLine("OnCollisionStay " + gameObject.rigidbody.position.ToString() + "  velocity " + gameObject.rigidbody.velocity.ToString() + " kinematic " + gameObject.rigidbody.isKinematic + " stop " + PhysicsUtil.isGolfStopMoveCall);
        if (gameObject.rigidbody.isKinematic) return;

        
        if (collision.collider.tag.Equals("Untagged"))
        {
            return;
        }

        string tag = collision.collider.tag;
        switch (tag)
        {
            case Tags.guo_ling:
                ForceReduceVelocity(PhysicsUtil.push_simu_velocity_reduce_f);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.guo_ling_bian_yuan:
                ForceReduceVelocity(guo_ling_bian_yuan_v_dec_f);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.qiu_dao:
                if (qiuDaoVelocityDec > qiu_dao_v_dec_f)
                {
                    qiuDaoVelocityDec -= qiu_dao_v_dec_f;
                }
                else
                {
                    qiuDaoVelocityDec = 0;
                }

                ForceReduceVelocity(qiuDaoVelocityDec);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.chang_cao_qu:
                if (changCaoQuVelocityDec > chang_cao_qu_v_dec_f)
                {
                    changCaoQuVelocityDec -= chang_cao_qu_v_dec_f;
                }
                else
                {
                    changCaoQuVelocityDec = 0;
                }

                ForceReduceVelocity(changCaoQuVelocityDec);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.sha_keng:
                ForceReduceVelocity(sha_keng_v_dec_f);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.shui:
                break;
            case Tags.jie_wai:
                ForceReduceVelocity(jie_wai_v_dec_f);

                curJieWaiRollTime += Time.fixedDeltaTime;
                if(curJieWaiRollTime >= maxJieWaiRollTime)
                {
                    gameObject.rigidbody.velocity = Vector3.zero;
                    gameObject.rigidbody.angularVelocity = Vector3.zero;
                }

                CheckBallStopOnCollisionStay();
                break;
            case Tags.jie_wai_outer_bounds:
                break;
            case Tags.hole:
                break;
            case Tags.qiu_dong_qiang_bi:
                break;
            case Tags.qiu_zuo:
                break;
            case Tags.fa_qiu_tai_di_mian:
                if (qiuDaoVelocityDec > qiu_dao_v_dec_f)
                {
                    qiuDaoVelocityDec -= qiu_dao_v_dec_f;
                }
                else
                {
                    qiuDaoVelocityDec = 0;
                }

                ForceReduceVelocity(qiuDaoVelocityDec);
                CheckBallStopOnCollisionStay();
                break;
            case Tags.shu_zhi:
                break;
            case Tags.shu_gan:
                break;
            case Tags.qi_gan:
                break;
        }
    }

    private void ForceReduceVelocity(float reduceF)
    {
        gameObject.rigidbody.velocity *= reduceF;
        gameObject.rigidbody.angularVelocity *= reduceF;
    }

    public void CheckBallStopOnCollisionStay()
    {
        // Console.WriteLine(" v " + gameObject.rigidbody.velocity.magnitude);
        if (gameObject.rigidbody.velocity.magnitude <= PhysicsUtil.stop_velocity_len)
        {
            //fix golf position
            Vector3 st = gameObject.rigidbody.position;
            st.y += 1000;
            Ray ray = new Ray(st, Vector3.down);
            RaycastHit hitInfo;
            if (Physics.Raycast(rigidbody.SimulateMgr, ray, out hitInfo, 2000, PhysicsUtil.getGroundLayer()))
            {
                gameObject.rigidbody.position = hitInfo.point + hitInfo.normal * (GameConfig.golf_collide_radiu - GameConfig.golf_stop_adjust_y);
            }

            gameObject.transform.position = gameObject.rigidbody.position;
            Tags.GolfArea onArea = GameMainLogic.GetGolfOnArea(rigidbody.SimulateMgr, gameObject.transform.position);
            m_oPhysicsUtil.setGolfStopFlag(gameObject.rigidbody, onArea);
        }
        
    }

    public void OnCollisionExit(Collision collision)
    {
        // Console.WriteLine("OnCollisionExit isKinematic " + gameObject.rigidbody.isKinematic + " tag " + collision.collider.tag);
        string tag = collision.collider.tag;
        if (tag.Equals(Tags.guo_ling))
        {

        }
    }

    public void FixedUpdate()
    {
        if((!gameObject.rigidbody.isKinematic) && gameObject.rigidbody.IsSleeping() && (gameObject.rigidbody.velocity.magnitude <= PhysicsUtil.stop_velocity_len))
        {
            gameObject.rigidbody.WakeUp();

            Tags.GolfArea onArea = GameMainLogic.GetGolfOnArea(rigidbody.SimulateMgr, gameObject.transform.position);
            m_oPhysicsUtil.setGolfStopFlag(gameObject.rigidbody, onArea);
        }

        // m_oDontGoThrough.FixedUpdate();
    }
}
