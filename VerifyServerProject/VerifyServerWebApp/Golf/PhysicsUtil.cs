using com.golf.proto;
using System;
using System.Collections;
using System.Collections.Generic;
using VerifyServerWebApp.UnityEngine;
public class PhysicsUtil
{
    //Golf 物理因素
    public const float branch_reduce_v_f = 0.6f;//树枝对球的减速//
    public const float top_bottom_spin_f = 0.006f;//上下旋的因子//
    public const float side_spin_f = 0.004f;//侧旋的因子//
    public const float wind_f = 0.1f;//风的因子//
    public const float club_curvature_f = 0.4f;//球杆的曲度因子，影响球往后拉的角度//
    public const float push_golf_init_v_f = 9.1f;//推杆时候的拉球的距离对速度的影响因子// 8

    public const int push_simulate_count = 100;

    public const float stop_velocity_len = 0.26f;
    public const float simu_time = 0.02f;

    public const float push_simu_velocity_reduce_f = 0.998f;
    public const float push_simu_friction_factor = 0.35f;
    public const float push_perfect_angle_delta = 2;
    public const float swing_perfect_angle_delta = 2;
    //public float swing_perfect_angle_delta_new = 1;

    //反弹的物理系数
    public const float reflect_chang_cao_qu = 0.16f;
    public const float reflect_qiu_dao = 0.41f;
    public const float reflect_guo_ling = 0.39f;
    public const float reflect_guo_ling_bian_yuan = 0.41f;
    public const float reflect_qiu_zuo = 0.41f;
    public const float reflect_sha_keng = 0.1f;
    public const float reflect_shui = 0;
    public const float reflect_jie_wai = 0.1f;
    public const float reflect_jie_wai_outer_bounds = 0;
    public const float reflect_qiu_dong_qiang_bi = 0.1f;
    public const float reflect_hole = 0;
    public const float reflect_qi_gan = 0.02f;
    public const float reflect_qi_gan_new = 0.28f;
    public const float reflect_shu_gan = 0.4f;

    //球越过球洞飞起添加时向上的物理参数
    public const float ball_jump_hole_curV_f = 0.21f;
    public const float ball_jump_hole_intersect_len_f = 1f;

    //模拟推杆时判断球是否进洞的参数。
    public const float check_ball_in_hole_radiu_f = 4.3501f;//8f;
    public const float check_ball_in_hole_dis_f = 2.9f;//4f;

    //挥杆时，切线方向速度的参数
    public const float hit_golf_tangle_v_f = 0.4f;

    public const float perfect_strick_angle_limit = 2;
    public const float great_strick_angle_limit = 5;
    public const float good_strick_angle_limit = 10;
    public const float normal_strick_angle_limit = 14;

    public const float guo_ling_friction = 0.15f;
    public const float guo_ling_bian_yuan_friction = 0.2f;
    public const float qiu_dao_friction = 0.02f;
    public const float chang_cao_qu_friction = 0.4f;
    public const float sha_keng_friction = 0.4f;
    public const float jie_wai_friction = 0.6f;

    //射线检测的时候，额外增加射线的长度， 避免 当点处于mesh中时，unity的RayCast检测不到。
    public const float safe_ray_cast_add_dis = 0.0001f;

    private IntPtr m_ptrSimulateMgr;

    private  Vector3 golfHole;
    private  Vector3 golfBottomHole;

    public PhysicsUtil(IntPtr ptrSimulateMgr, Vector3 holePos)
    {
        m_ptrSimulateMgr = ptrSimulateMgr;

        golfHole = holePos;
        golfBottomHole = golfHole - new Vector3(0, -0.15f, 0);
    }

    public void SimuPushGolfRoadPath(float radiu, float mass)
    {
        Vector3 vel = GetPushGolfSimuVelocity();
        Console.WriteLine("push simu begin v " + vel.ToString());
        SimuPushGolfPositionAndVelocity(radiu, mass, vel);
        Console.WriteLine("last point pos is " + pushRoadPathPoints[pushRoadPathPoints.size-1].ToString());
        Console.WriteLine("last velocity is " + pushRoadPathVelocity[pushRoadPathVelocity.size-1].ToString());
        SimuPushGolfRotation();
    }

    /// <summary>
    /// 模拟球在地面滚动
    /// </summary>
    /// <returns></returns>
    public void SimuPushGolfPositionAndVelocity(float radius, float mass, Vector3 initVelocity)
    {
        Vector3 curPosition = strickData.ballPos;
        Vector3 curVelocity = adjustInitV(curPosition, initVelocity);

        ClearSimuPushRoadPathPointAndVel();

        pushRoadPathPoints.Add(curPosition);
        pushRoadPathVelocity.Add(curVelocity);

        Vector3 mg = Physics.gravity * mass;
        float friction = mg.magnitude * push_simu_friction_factor;

        for (int i = 0; i < push_simulate_count; i++)
        {
            Vector3 newPosition = curPosition;
            Vector3 newVelocity = curVelocity;

            RaycastHit hitInf;

            Vector3 rayStart = curPosition;
            rayStart.y += 1;

            if (Physics.Raycast(m_ptrSimulateMgr, rayStart, Vector3.down, out hitInf, 2, getGroundLayer()))
            {
                string hitTag = hitInf.collider.tag;
                if (hitTag == Tags.guo_ling)
                {
                    if (Vector3.Angle(Vector3.up, hitInf.normal) <= 1)
                    {
                        //水平面

                        friction = mg.magnitude * push_simu_friction_factor;
                        float frictionVLen = friction * simu_time;
                        if(curVelocity.magnitude <= frictionVLen)
                        {
                            newVelocity = Vector3.zero;
                            return;
                        }
                        else
                        {
                            newVelocity = curVelocity.normalized * (curVelocity.magnitude - frictionVLen);
                            newVelocity *= push_simu_velocity_reduce_f;
                        }
                    }
                    else
                    {
                        float slopInRadian = (180 - Vector3.Angle(Vector3.down, hitInf.normal)) * Mathf.Deg2Rad;
                        friction = mg.magnitude * Mathf.Cos(slopInRadian) * push_simu_friction_factor;

                        Vector3 gSlop = mg + hitInf.normal * mg.magnitude * Mathf.Cos(slopInRadian);

                        newVelocity = curVelocity + gSlop * simu_time;

                        float reduceVLen = friction * simu_time;
                        if(reduceVLen >= newVelocity.magnitude)
                        {
                            newVelocity = Vector3.zero;
                            return;
                        }
                        else
                        {
                            newVelocity = newVelocity.normalized * (newVelocity.magnitude - reduceVLen);
                            newVelocity *= push_simu_velocity_reduce_f;
                        }
                    }

                }
                else if(hitTag == Tags.hole || hitTag == Tags.qiu_dong_qiang_bi)
                {
                    newVelocity = curVelocity;
                }
                else
                {
                    Console.WriteLine("hit out");//GameDebug.LogInfo("hit out");
                    return;
                }


                newPosition = curPosition + newVelocity * simu_time;
                rayStart = newPosition;
                rayStart.y += 1;

                if (Physics.Raycast(m_ptrSimulateMgr, rayStart, Vector3.down, out hitInf, 2, getGroundLayer()))
                {
                    hitTag = hitInf.collider.tag;
                    if (hitTag == Tags.guo_ling)
                    {
                        newVelocity = adjustGuoLingVelocity(hitInf, newVelocity);

                        rayStart = newPosition + hitInf.normal;
                        Vector3 oppositeNormal = hitInf.normal * -1;
                        Ray ray = new Ray(rayStart, oppositeNormal);
                        if (Physics.Raycast(m_ptrSimulateMgr, ray, out hitInf, 2, getGroundLayer()))
                        {
                            if (IsAdjustNewPos_WithGroundNormal(hitInf.collider.tag))
                            {
                                newPosition = ray.GetPoint(hitInf.distance) + hitInf.normal * (radius - GameConfig.golf_stop_adjust_y);
                            }
                        }
                        else
                        {
                            Console.WriteLine("not hit ground");//GameDebug.LogInfo("not hit ground");
                        }

                    }
                    else if (hitTag == Tags.hole || hitTag == Tags.qiu_dong_qiang_bi)
                    {
                        //
                    }
                }
            }

            curVelocity = newVelocity;
            curPosition = newPosition;

            pushRoadPathVelocity.Add(curVelocity);
            pushRoadPathPoints.Add(curPosition);

            if(curVelocity.magnitude <= stop_velocity_len)
            {
                return;
            }
        }

        return ;
    }

    /// <summary>
    /// 1：如果使用 安全的物理， 则 newHitTag 必须是果岭，才更新
    /// 2：如果没有使用 安全的物理， 则使用老版本的逻辑。直接更新
    /// </summary>
    /// <param name="newHitTag"></param>
    /// <returns></returns>
    private static bool IsAdjustNewPos_WithGroundNormal(string newHitTag)
    {
        if (ConfigInfo.config.UseNewPhysics && (newHitTag == Tags.guo_ling))
        {
            return true;
        }

        if (!ConfigInfo.config.UseNewPhysics)
        {
            return true;
        }

        return false;
    }

    public Vector3 adjustInitV(Vector3 position, Vector3 newVelocity)
    {
        Vector3 rayStart = position;
        rayStart.y += 1;

        RaycastHit hitInf;
        if (Physics.Raycast(m_ptrSimulateMgr, rayStart, Vector3.down, out hitInf, 2, getGroundLayer()))
        {
            string hitTag = hitInf.collider.tag;
            if (hitTag == Tags.guo_ling)
            {
                newVelocity = adjustGuoLingVelocity(hitInf, newVelocity);
            }
            else if (hitTag == Tags.hole || hitTag == Tags.qiu_dong_qiang_bi)
            {
                //
            }
        }

        return newVelocity;
    }

    private static Vector3 adjustGuoLingVelocity(RaycastHit hitInf, Vector3 newVelocity)
    {
        if (Vector3.Angle(Vector3.up, hitInf.normal) <= 1)
        {
            Vector3 tmpV = new Vector3(newVelocity.x, 0, newVelocity.z);
            newVelocity = newVelocity.magnitude * tmpV.normalized;
        }
        else
        {

            float angle = Vector3.Angle(newVelocity, hitInf.normal);
            Vector3 vV;
            if (angle > 90)
            {
                angle -= 90;
                vV = hitInf.normal * -1 * newVelocity.magnitude * Mathf.Sin(angle * Mathf.Deg2Rad);
            }
            else
            {
                vV = hitInf.normal * newVelocity.magnitude * Mathf.Cos(angle * Mathf.Deg2Rad);
            }

            Vector3 vH = newVelocity - vV;
            newVelocity = vH.normalized * newVelocity.magnitude;
        }

        return newVelocity;
    }

    private Vector3 pushGolfSimuLastAngularVelocity;
    private Quaternion tmpObjRotation;
    private void SimuPushGolfRotation()
    {
        pushRoadPathRotList.Clear();

        pushRoadPathRotList.Add(strickData.ballRot);

        tmpObjRotation = new Quaternion(strickData.ballRot);//TmpObj.transform.rotation = strickData.ballRot;
        pushGolfSimuLastAngularVelocity = Vector3.zero;

        for (int index = 1; index < pushRoadPathPoints.size; index++)
        {
            Vector3 fromP = pushRoadPathPoints[index - 1];
            Vector3 toP = pushRoadPathPoints[index];
            Vector3 tmp = toP - fromP;
            tmp.y = 0;
            float rotateAngle = tmp.magnitude / (2 * Mathf.PI * GameConfig.golf_collide_radiu) * 360;
            Vector3 rotateAxis = Vector3.Cross(pushRoadPathVelocity[index].normalized, Vector3.down);

            rotateAngle = Mathf.RoundToInt(rotateAngle) / 100f;
            if (rotateAngle > 1f)
            {
                rotateAngle = 1f;
            }

            tmpObjRotation = Quaternion.AngleAxis(rotateAngle * Mathf.Rad2Deg, rotateAxis.normalized) * tmpObjRotation;//TmpObj.transform.RotateAroundLocal(rotateAxis.normalized, rotateAngle);
            pushRoadPathRotList.Add(tmpObjRotation);

            if (index == pushRoadPathPoints.size - 1)
            {
                pushGolfSimuLastAngularVelocity = rotateAxis.normalized * rotateAngle;
            }
        }

    }

    public static int getGroundLayer()
    {
        return 1 << LayerMask.NameToLayer(Layers.ground) | 1 << LayerMask.NameToLayer(Layers.water);
    }

    public static int GetGolfLayer()
    {
        return 1 << LayerMask.NameToLayer(Layers.golf);
    }

    /// <summary>
    /// 根据推杆时的力度和角度调整初始速度
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPushGolfSimuVelocity()
    {
        {
            //pushGolfAngle = 10;
            //pushGolfFixedAngle = 10;
            //dragGolfDistanceWhenFire = HitGolfPanel.perfectDragDistance;

        }

        float pushGolfAngleNew = Mathf.RoundToInt(strickData.pointerAngle * 10) / 10f;

        {
            float g1 = pushGolfAngleNew;
            float g2 = strickData.golfAngle;
            if (g1 >= 90)
            {
                g1 = g1 - 360;
            }
            if (g2 >= 90)
            {
                g2 = g2 - 360;
            }
            float absAngle = Mathf.Abs(g1 - g2);

            //操作上的优化//
            if (Mathf.Abs(absAngle) <= push_perfect_angle_delta)
            {
                pushGolfAngleNew = strickData.golfAngle;
            }
        }

        Vector3 dir = golfHole - strickData.ballPos;
        dir.y = 0;
        dir.Normalize();
        dir = Quaternion.AngleAxis(-pushGolfAngleNew, Vector3.up) * dir;

        //int dragGolfDistanceWhenFireFixed = Mathf.RoundToInt(dragGolfDistanceWhenFire * 5);
        //Vector3 vt = dir * dragGolfDistanceWhenFireFixed;
        float forcePer = strickData.force / HitGolfPanel.maxDragDistance;
        Vector3 vt = dir * (Mathf.Pow(forcePer, 3) * PhysicsUtil.push_golf_init_v_f);

        return vt;
    }

    /// <summary>
    /// 反弹时候的速度保留系数
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static float getBounciness(string tag)
    {
        float reflect = 1f;
        switch (tag)
        {
            case Tags.chang_cao_qu:
                reflect = reflect_chang_cao_qu;
                break;
            case Tags.qiu_dao:
                reflect = reflect_qiu_dao;
                break;
            case Tags.guo_ling:
                reflect = reflect_guo_ling;
                break;
            case Tags.guo_ling_bian_yuan:
                reflect = reflect_guo_ling_bian_yuan;
                break;
            case Tags.fa_qiu_tai_di_mian:
                reflect = reflect_qiu_dao;
                break;
            case Tags.qiu_zuo:
                reflect = reflect_qiu_zuo;
                break;
            case Tags.sha_keng:
                reflect = reflect_sha_keng;
                break;
            case Tags.shui:
                reflect = reflect_shui;
                break;
            case Tags.jie_wai:
                reflect = reflect_jie_wai;
                break;
            case Tags.jie_wai_outer_bounds:
                reflect = reflect_jie_wai_outer_bounds;
                break;
            case Tags.qiu_dong_qiang_bi:
                reflect = reflect_qiu_dong_qiang_bi;
                break;
            case Tags.hole:
                reflect = reflect_hole;
                break;
            case Tags.qi_gan:
                if((ConfigInfo.config != null) && ConfigInfo.config.UseNewPhysics)
                {
                    reflect = reflect_qi_gan_new;
                }
                else
                {
                    reflect = reflect_qi_gan;
                }
                break;
            case Tags.shu_gan:
                reflect = reflect_shu_gan;
                break;
            default:
                reflect = 1;
                break;

        }

        return reflect;
    }



    ////////////////////////////
    //GameMainLogic的物理移到此处//
    ////////////////////////////

    public StrickDataRealValue strickData = new StrickDataRealValue();
    public CCStrickDataLocal strickDataPacked = new CCStrickDataLocal();



    //推杆的路线
    public BetterList<Vector3> pushRoadPathPoints = new BetterList<Vector3>();
    public BetterList<Vector3> pushRoadPathVelocity = new BetterList<Vector3>();
    public BetterList<Quaternion> pushRoadPathRotList = new BetterList<Quaternion>();

    public void InitPushGolfSimu()
    {
        pushRoadPathIndex = 1;
        pushGolfSubStep = push_golf_move_update_simulate_path;
        isSimuGolfReadyToJump = false;
    }

    

    public void PushGolfMoveForServer(GameObject ball, DontGoThroughThings oDontGoThrough)
    {
        while(pushRoadPathIndex < pushRoadPathPoints.size)
        {
            updatePushGolfSimuPathToBall(ball);

            processPushGolfCrossHole(ball, null);

            if(pushGolfSubStep != push_golf_move_update_simulate_path)
            {
                break;
            }

            pushRoadPathIndex++;
        }

        if (pushGolfSubStep == push_golf_move_update_simulate_path)
        {
            pushGolfSubStep = push_golf_move_real_physics;

            ball.rigidbody.isKinematic = false;
            ball.rigidbody.velocity = pushRoadPathVelocity[pushRoadPathVelocity.size - 1];
            ball.rigidbody.angularVelocity = pushGolfSimuLastAngularVelocity;
            Console.WriteLine("正常模拟结束切换到真实物理");
            Console.WriteLine("ball postion " + ball.rigidbody.position.ToString());
            Console.WriteLine("ball rotation " + ball.rigidbody.rotation.ToString());
            Console.WriteLine("ball velocity " + ball.rigidbody.velocity.ToString());
            Console.WriteLine("ball angularVelocity " + ball.rigidbody.angularVelocity.ToString());
        }
        else if (pushGolfSubStep == push_golf_move_kinematic_to_hole)
        {
            Console.WriteLine("模拟阶段直接进洞  pushRoadPathIndex = " + pushRoadPathIndex);
            Console.WriteLine("pushRoadPathPoints[pushRoadPathIndex]  " + pushRoadPathPoints[pushRoadPathIndex].ToString());
            Console.WriteLine("pushRoadPathVelocity[pushRoadPathIndex]  " + pushRoadPathVelocity[pushRoadPathIndex].ToString());
            //球模拟进洞, 直接设置为目标点，在球洞
            pushGolfSubStep = push_golf_move_finish_kinematic_to_hole;

            setGolfStopFlag(ball.rigidbody, Tags.GolfArea.hole, true);
            ball.transform.position = golfBottomHole;
        }

        if(pushGolfSubStep == push_golf_move_real_physics)
        {
            oDontGoThrough.updateRigidBodyPreviousPos();
        }
    }

    public void updatePushGolfSimuPathToBall(GameObject ball)
    {
        if (pushRoadPathIndex < pushRoadPathPoints.size)
        {
            ball.transform.position = pushRoadPathPoints[pushRoadPathIndex];
            ball.transform.rotation = pushRoadPathRotList[pushRoadPathIndex];
        }
    }

    public  int pushGolfSubStep;
    private const int push_golf_move_update_simulate_path = 1;
    private const int push_golf_move_real_physics = 2;
    private const int push_golf_move_kinematic_to_hole = 3;
    private const int push_golf_move_finish_kinematic_to_hole = 4;

    public int pushRoadPathIndex = 0;
    private bool isSimuGolfReadyToJump = false;

    private Vector3 kinematicDir;
    private float kinematicToHoleTime;
    private float intersectLength = 0.1f;
    public void processPushGolfCrossHole(GameObject ball, System.Action moveCameraToLookAtGolfHoleAction)
    {
        Vector3 lastPosition = pushRoadPathPoints[pushRoadPathIndex - 1];
        Vector3 curPosition = pushRoadPathPoints[pushRoadPathIndex];

        if (!isSimuGolfReadyToJump)
        {
            //线段与球洞的圆平面是否相交
            bool isSegIntersect = MyMathUtils.intersectSegmentCircle(lastPosition.x, lastPosition.z, curPosition.x, curPosition.z, golfHole.x, golfHole.z, GameConfig.golf_hole_circle_radiu * GameConfig.golf_hole_circle_radiu);
            if (isSegIntersect)
            {
                intersectLength = MyMathUtils.intersectLineCircleLen(lastPosition.x, lastPosition.z, curPosition.x, curPosition.z, golfHole.x, golfHole.z, GameConfig.golf_hole_circle_radiu);

                Vector3 curVelocity = pushRoadPathVelocity[pushRoadPathIndex];
                if (isGolfToInHole(curVelocity, intersectLength))
                {
                    pushGolfSubStep = push_golf_move_kinematic_to_hole;

                    if(moveCameraToLookAtGolfHoleAction != null)
                    {
                        moveCameraToLookAtGolfHoleAction();
                    }

                    if (!MyMathUtils.isPointInCircle(curPosition, golfHole, GameConfig.golf_hole_circle_radiu))
                    {
                        ball.transform.position = new Vector3(golfHole.x, ball.transform.position.y, golfHole.z);
                    }

                    kinematicDir = (golfBottomHole - ball.transform.position).normalized;
                    kinematicToHoleTime = 0;
                }
                else
                {
                    bool hasJump = processGolfCrossHoleJump(ball, curPosition);
                    if (hasJump)
                    {
                        isSimuGolfReadyToJump = false;
                    }
                    else
                    {
                        isSimuGolfReadyToJump = true;
                    }

                }
            }
        }
        else
        {
            bool hasJump = processGolfCrossHoleJump(ball, curPosition);
            if (hasJump)
            {
                isSimuGolfReadyToJump = false;
            }
            else
            {
                isSimuGolfReadyToJump = true;
            }
        }
    }

    private bool processGolfCrossHoleJump(GameObject ball, Vector3 curPosition)
    {
        if (!MyMathUtils.isPointInCircle(curPosition, golfHole, GameConfig.golf_hole_circle_radiu))
        {
            pushGolfSubStep = push_golf_move_real_physics;

            ball.rigidbody.isKinematic = false;
            ball.rigidbody.angularVelocity = pushGolfSimuLastAngularVelocity;

            //增加一个向上的速度
            Vector3 curVelocity = pushRoadPathVelocity[pushRoadPathIndex];
            Vector3 upVelocity = (curVelocity.magnitude * ball_jump_hole_curV_f + intersectLength * ball_jump_hole_intersect_len_f) * Vector3.up;
            ball.rigidbody.velocity = curVelocity + upVelocity;
            Console.WriteLine("越过球洞切换到真实物理 index " + pushRoadPathIndex);
            Console.WriteLine("pushRoadPathPoints[pushRoadPathIndex]  " + pushRoadPathPoints[pushRoadPathIndex].ToString());
            Console.WriteLine("pushRoadPathVelocity[pushRoadPathIndex]  " + pushRoadPathVelocity[pushRoadPathIndex].ToString());
            Console.WriteLine("ball postion " + ball.rigidbody.position.ToString());
            Console.WriteLine("ball rotation " + ball.rigidbody.rotation.ToString());
            Console.WriteLine("ball velocity " + ball.rigidbody.velocity.ToString());
            Console.WriteLine("ball angularVelocity " + ball.rigidbody.angularVelocity.ToString());
            return true;
        }

        return false;
    }

    /// <summary>
    /// 球跨越球洞时，是否进入球洞
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="intersectLen"></param>
    /// <returns></returns>
    private static bool isGolfToInHole(Vector3 velocity, float intersectLen)
    {
        //Debug.Log("intersectLen=" + intersectLen);
        //Debug.Log("velocity.magnitude * Time.fixedDeltaTime=" + velocity.magnitude * Time.fixedDeltaTime);
        if ((intersectLen >= GameConfig.golf_collide_radiu * check_ball_in_hole_radiu_f) && (velocity.magnitude * Time.fixedDeltaTime * check_ball_in_hole_dis_f < intersectLen))
        {
            return true;
        }

        return false;
    }

    

    public void ClearSimuPushRoadPathPointAndVel()
    {
        pushRoadPathPoints.Clear();
        pushRoadPathVelocity.Clear();
    }



    

    /// <summary>
    /// 根据操作的结果，重新确定落点，预测球的轨迹线。
    /// UI上球心到标靶的向量 对应 场景中球心到firsthitPoint的向量
    /// 球杆的准确度影响场景中的标靶的大小
    /// </summary>
    public Vector3 CalculateHitPoint()
    {
        {
            //根据击球时的参数调整firstHitPoint

            float zhiZhenRadiu = HitGolfPanel.GetZhiZhenRadiu(strickData.force);
            if(Mathf.Abs(zhiZhenRadiu - HitGolfPanel.perfectRadius) <= HitGolfPanel.pfct_efct_radiu_permit_offset)
            {
                zhiZhenRadiu = HitGolfPanel.perfectRadius;
            }
            Console.WriteLine("CalculateHitPoint zhiZhenRadiu" + zhiZhenRadiu);
            float fac = zhiZhenRadiu / HitGolfPanel.perfectRadius;
            Console.WriteLine("CalculateHitPoint fac" + fac);
            Vector3 ballToFirstHitDir = strickData.firstHitPoint - strickData.ballPos;
            Console.WriteLine("CalculateHitPoint ballToFirstHitDir  " + ballToFirstHitDir.ToString());
            float pointAngleNew = Mathf.RoundToInt(strickData.pointerAngle * 10) / 10f;
            Console.WriteLine("CalculateHitPoint pointAngleNew" + pointAngleNew);
            float final_swing_perfect_angle_delta = PhysicsUtil.swing_perfect_angle_delta;
            //if((ConfigInfo.config != null) && ConfigInfo.config.UseNewPhysics)
            //{
            //    final_swing_perfect_angle_delta = PhysicsUtil.swing_perfect_angle_delta_new;
            //}

            if ((Mathf.Abs(360 - pointAngleNew) <= final_swing_perfect_angle_delta) || (Mathf.Abs(pointAngleNew) <= final_swing_perfect_angle_delta))
            {
                pointAngleNew = 0;
            }

            Vector3 newDir = Quaternion.AngleAxis(-pointAngleNew, Vector3.up) * ballToFirstHitDir.normalized;
            Vector3 newP = strickData.ballPos + newDir * ballToFirstHitDir.magnitude * fac;
            Console.WriteLine("ballToFirstHitDir.magnitude " + ballToFirstHitDir.magnitude + "  fac " + fac  + " strickData.ballPos " + strickData.ballPos.ToString());
            Console.WriteLine("CalculateHitPoint newDir " + newDir);
            Console.WriteLine("CalculateHitPoint newP " + newP);
            float clubAccuracy = ClubCfgData.GetProperty(strickData.clubAttrs, ClubCfgData.index_accuracy, strickData.useClubLvl);
            Console.WriteLine("CalculateHitPoint clubAccuracy " + clubAccuracy);
            newP = strickData.firstHitPoint + (newP - strickData.firstHitPoint) * MainLogicHelp.GetClubAccuracyEffectToLandPoint(clubAccuracy);
            Console.WriteLine("CalculateHitPoint newP " + newP);
            {
                Vector3 finalP = newP;
                RaycastHit rayHit;
                if (GameMainLogic.GetHitGround(m_ptrSimulateMgr, newP, out rayHit))
                {
                    finalP = rayHit.point + rayHit.normal * GameConfig.golf_collide_radiu;
                    Console.WriteLine("CalculateHitPoint rayHit " + rayHit.point.ToString() + " rayHit.normal " + rayHit.normal.ToString());
                }
                Console.WriteLine("CalculateHitPoint finalP " + finalP.ToString());
                if (MyMathUtils.isPointInCircle(finalP, golfHole, GameConfig.golf_hole_circle_radiu))
                {
                    //如果落点在球洞，则修正落点的y坐标
                    finalP.y = golfHole.y + GameConfig.golf_collide_radiu;
                }
                Console.WriteLine("CalculateHitPoint finalP " + finalP.ToString());
                return finalP;
            }
        }
    }

    /// <summary>
    /// 根据操作的结果，重新确定落点，预测球的轨迹线。
    /// UI上球心到标靶的向量 对应 场景中球心到firsthitPoint的向量
    /// 球杆的准确度影响场景中的标靶的大小
    /// </summary>
    public void CalculateHitPointAndPreditGolfPath()
    {
        Vector3 finalFirstHitPoint = CalculateHitPoint();
        CalculatePaoWuXian(finalFirstHitPoint, true);
    }


    public  BetterList<Vector3> hitGolfSimuVelocityList = new BetterList<Vector3>();
    public  BetterList<Vector3> hitGolfSimuPointList = new BetterList<Vector3>();
    public BetterList<Quaternion> hitGolfSimuRotList = new BetterList<Quaternion>();
    public  BetterList<int> hitGolfSimuBounceEndVerIndex = new BetterList<int>();
    public  BetterList<string> hitGolfSimuCollideTagList = new BetterList<string>();

    private  Vector3 paoWuXianOriginPoint = new Vector3();
    private  Vector3 paoWuXianLastPoint = new Vector3();
    private  Vector3 paoWuXianNewPoint = new Vector3();
    private  Vector3 paoWuXianNewVelocity = new Vector3();
    private  string paoWuXianNewCollideTag = string.Empty;
    private  int paoWuXianPointIndex = 0;
    private  Vector3 paoWuXianStartVelocity = new Vector3();
    private  float paoWuXianTime = 0;
    private const float fixedDeltaTime = 0.02f;
    private  bool isPaoWuXian;
    //上一个位置是否在树枝里面
    private  bool isLastInShuZhi;
    //挥杆时预测球的轨迹线时候的加速度
    private  Vector3 hitGolfSimuGravity = new Vector3();

    public bool hasBouncedOnGround;//挥杆的时候球是否反弹//

    /// <summary>
    /// 使用unity的物理引擎，计算果岭外击球的轨迹线
    /// </summary>
    public void CalculatePaoWuXian(Vector3 firstHitPoint, bool isAddWind)
    {
        hasBouncedOnGround = false;

        hitGolfSimuPointList.Clear();
        hitGolfSimuVelocityList.Clear();
        hitGolfSimuBounceEndVerIndex.Clear();
        hitGolfSimuCollideTagList.Clear();

        SetGolfInitVelocityAndGravityInHitGolf(firstHitPoint,isAddWind);
        paoWuXianOriginPoint = strickData.ballPos;
        paoWuXianLastPoint = paoWuXianOriginPoint;
        paoWuXianTime = 0;
        isPaoWuXian = true;
        isLastInShuZhi = false;

        float guideValue = ClubCfgData.GetProperty(strickData.clubAttrs, ClubCfgData.index_guide, strickData.useClubLvl);
        int guideValueCeilInt = Mathf.CeilToInt(guideValue);

        paoWuXianPointIndex = 0;
        hitGolfSimuPointList.Add(strickData.ballPos);
        hitGolfSimuVelocityList.Add(paoWuXianStartVelocity);
        hitGolfSimuCollideTagList.Add(string.Empty);
        paoWuXianPointIndex++;
        Console.WriteLine("strickData.ballPos " + strickData.ballPos.ToString() + " paoWuXianStartVelocity " + paoWuXianStartVelocity.ToString());
        while (isPaoWuXian)
        {
            CalculateBallPosition(fixedDeltaTime);
            
            if(isPaoWuXian)
            {
                paoWuXianPointIndex++;

                if ((paoWuXianPointIndex >= 1000) || hitGolfSimuBounceEndVerIndex.size >= guideValueCeilInt)
                {
                    Console.WriteLine("aaa " + paoWuXianPointIndex + " hitGolfSimuBounceEndVerIndex.size  " + hitGolfSimuBounceEndVerIndex.size + "  guideValueCeilInt " + guideValueCeilInt);
                    isPaoWuXian = false;
                }
            }
        }

        CalculateHitGolfSimuRot();
    }

    /// <summary>
    /// 挥杆时球的初始速度和加速度
    /// </summary>
    public void SetGolfInitVelocityAndGravityInHitGolf(Vector3 firstFitPoint, bool IsAddWind)
    {
        // Console.WriteLine("SetGolfInitVelocityAndGravityInHitGolf  firstFitPoint" + firstFitPoint.ToString() + " IsAddWind " + IsAddWind);
        //初始抛物线的夹角
        float hitBallAngleInDegree = strickData.clubAngle;
        if(hitBallAngleInDegree <= 0)
        {
            hitBallAngleInDegree = 45;
            Console.WriteLine("Error hitBallAngleInDegree, Reset To Default");//GameDebug.LogError("Error hitBallAngleInDegree, Reset To Default");
        }

        Vector3 ballPosition = strickData.ballPos;

        //水平方向的距离
        Vector3 tmp = ballPosition - firstFitPoint;
        tmp.y = 0;
        float distance = tmp.magnitude;

        Vector3 v0 = firstFitPoint - ballPosition;
        v0.y = 0;
        v0.Normalize();

        float dy = firstFitPoint.y - ballPosition.y;
        float tan = (float)Mathf.Tan(hitBallAngleInDegree * Mathf.Deg2Rad);
        v0 = v0 * (distance / Mathf.Sqrt(2 * (dy - tan * distance) / Physics.gravity.y));
        Vector3 v1 = v0 + new Vector3(0, tan * v0.magnitude, 0);

        //计算飞行的总时间
        Vector3 p1 = strickData.ballPos;
        p1.y = 0;
        Vector3 p2 = firstFitPoint;
        p2.y = 0;
        float time = Vector3.Distance(p1, p2) / v0.magnitude;

        //添加切线方向的速度//
        Vector3 v1xz = v1;
        v1xz.y = 0;
        //拉球角度的完美值范围
        float golfAngle = strickData.golfAngle;
        if(Mathf.Abs(golfAngle) <= HitGolfPanel.pfct_efct_drag_angle_permit_offset)
        {
            golfAngle = 0;
        }
        Vector3 tangleV = Vector3.Cross(Vector3.down, v1xz).normalized * golfAngle * hit_golf_tangle_v_f;

        //添加风的影响速度//
        Vector3 windV = Vector3.zero;
        if (IsAddWind)
        {
            float windPower = strickData.wind.z;

            int ballWindReduce = BallCfgData.GetWindReduce(strickData.ballSkills);
            if(ballWindReduce != 0)
            {
                windPower = strickData.wind.z * (100 - ballWindReduce) / 100f;
            }

            windV = new Vector3(strickData.wind.x, 0, strickData.wind.y).normalized * windPower * PhysicsUtil.wind_f;
        }
        // Console.WriteLine(" V1 " + v1 + " " + tangleV + "  " + windV);
        paoWuXianStartVelocity = v1 + tangleV + windV;

        hitGolfSimuGravity = Physics.gravity + -2 / time * tangleV;
    }

    //抛物线反弹轨迹
    private void CalculateBallPosition(float delta)
    {
        paoWuXianTime += delta;
        float siuTime = Mathf.RoundToInt(paoWuXianTime / fixedDeltaTime) * fixedDeltaTime;

        paoWuXianNewPoint = paoWuXianOriginPoint + paoWuXianStartVelocity * siuTime + hitGolfSimuGravity * siuTime * siuTime / 2;
        paoWuXianNewVelocity = paoWuXianStartVelocity + hitGolfSimuGravity * siuTime;
        paoWuXianNewCollideTag = string.Empty;

        bool isLineEnd = false;

        Vector3 rayDir = paoWuXianNewPoint - paoWuXianLastPoint;
        Ray ray = new Ray(paoWuXianLastPoint, rayDir);
        RaycastHit hitInfo;

        Vector3 end = ray.origin + ray.direction.normalized * rayDir.magnitude;
        
        bool hasGoThrough = Physics.SphereCast(m_ptrSimulateMgr, ray, GameConfig.golf_collide_radiu, out hitInfo, rayDir.magnitude, GameMainLogic.GetAllCollisionLayer());
        
        // Console.WriteLine("ray origin " + ray.origin.ToString() + "  dir " + ray.direction.normalized + "  rayDir.magnitude " +rayDir.magnitude + "   ray end " + end.ToString() + "  hasGoThrough " + hasGoThrough);
        if(hasGoThrough)
        {
            if(hitInfo.collider.tag.Equals("qiu_zuo") && hitInfo.collider.name.Equals("qiu_zuo"))
            {
                hasGoThrough = false;
            }
            // Console.WriteLine("SphereCast " + hitInfo.collider.name + "  " + hitInfo.collider.tag  + " hitPoint " + hitInfo.point.ToString());
        }

        // if ((!hasGoThrough) && ConfigInfo.config.UseNewPhysics)
        // {
        //     hasGoThrough = Physics.Raycast(m_ptrSimulateMgr, ray, out hitInfo, rayDir.magnitude + safe_ray_cast_add_dis, GameMainLogic.GetAllCollisionLayer());
        //     if(hasGoThrough){Console.WriteLine("Raycast " + hitInfo.collider.name + "  " + hitInfo.collider.tag);}
        //     if(hasGoThrough && hitInfo.collider.tag.Equals(Tags.shu_zhi))
        //     {
        //         if (!ConfigInfo.config.UseNewPhysics)
        //         {
        //             hasGoThrough = false;
        //         }
        //     }
        // }

        if (hasGoThrough)
        {
            SetIsBounceOnGround(hitInfo.collider.tag);

            paoWuXianNewCollideTag = hitInfo.collider.tag;

            bool curInShuZhi = false;
            if (hitInfo.collider.tag.Equals(Tags.shu_zhi))
            {
                curInShuZhi = true;
            }

            if (curInShuZhi)
            {
                if (!isLastInShuZhi)
                {
                    isLastInShuZhi = curInShuZhi;

                    paoWuXianStartVelocity = paoWuXianNewVelocity * PhysicsUtil.branch_reduce_v_f;
                    paoWuXianNewVelocity = paoWuXianStartVelocity;

                    paoWuXianTime = 0;

                    paoWuXianNewPoint = hitInfo.point;
                    paoWuXianOriginPoint = paoWuXianNewPoint;
                    paoWuXianLastPoint = paoWuXianOriginPoint;
                    Console.WriteLine("isLineEnd BBB ");
                    isLineEnd = true;
                }
                else
                {
                    isLastInShuZhi = curInShuZhi;
                    paoWuXianLastPoint = paoWuXianNewPoint;
                    isLineEnd = false;
                }
            }
            else
            {
                isLastInShuZhi = curInShuZhi;

                float reflectF = PhysicsUtil.getBounciness(hitInfo.collider.tag);
                Vector3 t = Vector3.Reflect(paoWuXianNewVelocity, hitInfo.normal);
                Vector3 bounsV = t * reflectF;
                if (hitInfo.collider.tag.Equals(Tags.sha_keng))
                {
                    //击中沙坑，垂直于沙坑方向的速度设置为0.
                    float angleInDeg = Vector3.Angle(hitInfo.normal, bounsV);
                    float vyLen = (float)Mathf.Cos(angleInDeg * Mathf.Deg2Rad) * bounsV.magnitude;
                    bounsV = bounsV - hitInfo.normal.normalized * vyLen;
                }

                //第一次反弹，考虑前后旋和侧旋对速度的影响//
                if (hitGolfSimuBounceEndVerIndex.size == 0)
                {
                    paoWuXianStartVelocity = AdjustBallFirstBounceVelocity(bounsV);
                }
                else
                {
                    paoWuXianStartVelocity = bounsV;
                }
                paoWuXianNewVelocity = paoWuXianStartVelocity;

                hitGolfSimuGravity = Physics.gravity;

                paoWuXianTime = 0;

                paoWuXianNewPoint = hitInfo.point + hitInfo.normal * GameConfig.golf_collide_radiu;//hitInfo.point - rayDir.normalized * 0.02f;
                paoWuXianOriginPoint = paoWuXianNewPoint;
                paoWuXianLastPoint = paoWuXianOriginPoint;

                Console.WriteLine("isLineEnd AAA ");
                isLineEnd = true;

                if (ConfigInfo.config.UseNewPhysics)
                {
                    //抛物线起始速度很小时，停止抛物线
                    if(paoWuXianStartVelocity.magnitude <= 0.1f)
                    {
                        Console.WriteLine("bbb " + paoWuXianStartVelocity.magnitude);
                        isPaoWuXian = false;
                    }
                    else
                    {
                        //如果沿碰撞点的法线方向速度分量小于1（反弹的高度为0.05m），停止抛物线。
                        float angleInDeg = Vector3.Angle(hitInfo.normal, paoWuXianStartVelocity);
                        float vInNormalDirLenth = (float)Mathf.Cos(angleInDeg * Mathf.Deg2Rad) * paoWuXianStartVelocity.magnitude;
                        if (vInNormalDirLenth <= 1f)
                        {
                            Console.WriteLine("ccc " + vInNormalDirLenth);
                            isPaoWuXian = false;
                        }
                    }
                }
                else
                {
                    if (paoWuXianStartVelocity.y <= 1f)
                    {
                        Console.WriteLine("ddd " + paoWuXianStartVelocity.y);
                        isPaoWuXian = false;
                    }
                }
            }
        }
        else
        {
            paoWuXianLastPoint = paoWuXianNewPoint;
            isLineEnd = false;
        }

        hitGolfSimuPointList.Add(paoWuXianNewPoint);
        hitGolfSimuVelocityList.Add(paoWuXianNewVelocity);
        hitGolfSimuCollideTagList.Add(paoWuXianNewCollideTag);

        if (MyMathUtils.isPointBellowInCircle(paoWuXianNewPoint, golfHole, GameConfig.golf_hole_circle_radiu))
        {
             Console.WriteLine("eee isPointBellowInCircle");
            isPaoWuXian = false;
            isLineEnd = true;
        }

        if (isLineEnd)
        {
            Console.WriteLine("isLineEnd   hitGolfSimuBounceEndVerIndex.Add ");
            hitGolfSimuBounceEndVerIndex.Add(paoWuXianPointIndex);
        }
    }

    private void SetIsBounceOnGround(string tag)
    {
        bool isBounceOnGround = false;

        switch (tag)
        {
            case Tags.fa_qiu_tai_di_mian:
            case Tags.chang_cao_qu:
            case Tags.qiu_dao:
            case Tags.sha_keng:
            case Tags.guo_ling:
            case Tags.guo_ling_bian_yuan:
                isBounceOnGround = true;
                break;
            default:
                isBounceOnGround = false;
                break;

        }

        if (isBounceOnGround)
        {
            hasBouncedOnGround = true;
        }
    }

    /// <summary>
    /// 调整第一次落点时的速度，前后旋，左右旋
    /// </summary>
    /// <param name="originVelocity"></param>
    /// <returns></returns>
    private Vector3 AdjustBallFirstBounceVelocity(Vector3 originVelocity)
    {
        float topBottomSpinVF = strickData.topBottomSpinEulerAngle * PhysicsUtil.top_bottom_spin_f;
        float sideSpinVF = - strickData.sideSpinEulerAngle * PhysicsUtil.side_spin_f;

        Vector3 adjV = originVelocity;

        //前后旋
        adjV = adjV + new Vector3(originVelocity.x, 0, originVelocity.z).normalized * originVelocity.magnitude * topBottomSpinVF;

        //侧旋
        adjV = adjV + Vector3.Cross(Vector3.up, originVelocity).normalized * originVelocity.magnitude * sideSpinVF;

        return adjV;
    }

    public Vector3 hitGolfLastSimuAngularVelocity;
    private void CalculateHitGolfSimuRot()
    {
        hitGolfSimuRotList.Clear();

        hitGolfSimuRotList.Add(strickData.ballRot);

        tmpObjRotation = new Quaternion(strickData.ballRot);
        hitGolfLastSimuAngularVelocity = Vector3.zero;

        for (int index = 1; index < hitGolfSimuPointList.size; index++)
        {
            float rotateAngle = 0;
            if ((hitGolfSimuBounceEndVerIndex.size == 0) || (index <= hitGolfSimuBounceEndVerIndex[0]))
            {
                rotateAngle = 0.1f;
            }
            else
            {
                for (int i = 0; i < hitGolfSimuBounceEndVerIndex.size - 1; i++)
                {
                    if ((index >= hitGolfSimuBounceEndVerIndex[i]) && (index < hitGolfSimuBounceEndVerIndex[i + 1]))
                    {
                        int bounceVIndex = hitGolfSimuBounceEndVerIndex[i];
                        Vector3 bounceV = hitGolfSimuVelocityList[bounceVIndex];
                        bounceV.y = 0;

                        rotateAngle = bounceV.magnitude * 0.01f;
                        if (rotateAngle < 0.1f)
                        {
                            rotateAngle = 0.1f;
                        }
                    }
                }
            }

            //最后一个点
            if(index == hitGolfSimuPointList.size - 1)
            {
                Vector3 bounceV = hitGolfSimuVelocityList[index];
                bounceV.y = 0;

                rotateAngle = bounceV.magnitude * 0.01f;
                if (rotateAngle < 0.1f)
                {
                    rotateAngle = 0.1f;
                }
            }

            Vector3 rotateAxis = Vector3.Cross(hitGolfSimuVelocityList[index].normalized, Vector3.down);

            tmpObjRotation = Quaternion.AngleAxis(rotateAngle * Mathf.Rad2Deg, rotateAxis.normalized) * tmpObjRotation;
            hitGolfSimuRotList.Add(tmpObjRotation);

            if (index == hitGolfSimuPointList.size - 1)
            {
                hitGolfLastSimuAngularVelocity = rotateAxis.normalized * rotateAngle * (1 / fixedDeltaTime);
            }
        }

    }


    //挥杆golf的三个状态//
    public int hitGolfSubStep;
    public const int hit_golf_move_update_simulate_path = 1;
    public const int hit_golf_move_real_physics = 2;
    public const int hit_golf_move_kinematic_to_hole = 3;

    private int hitRoadPathIndex = 0;

    public void InitHitGolfSimu()
    {
        hitRoadPathIndex = 1;
        hitGolfSubStep = hit_golf_move_update_simulate_path;
    }

    public  float timeAfterFixedUpdate = 0;
    public  long lastUpdateTime = 0;

    public void TransformPackValueToUnPackValue()
    {
        strickData.type = strickDataPacked.type;
        strickData.force = StrickParamtTransfUtil.UnPackForce(strickDataPacked.force);
        strickData.golfAngle = StrickParamtTransfUtil.UnPackAngle(strickDataPacked.angle);
        strickData.pointerAngle = StrickParamtTransfUtil.UnPackAngle(strickDataPacked.pointerAngle);
        strickData.useClubId = strickDataPacked.clubID;
        strickData.ballId = strickDataPacked.ballId;
        strickData.topBottomSpinEulerAngle = StrickParamtTransfUtil.UnPackSpin(strickDataPacked.hitPoint.x);
        strickData.sideSpinEulerAngle = StrickParamtTransfUtil.UnPackSpin(strickDataPacked.hitPoint.y);
        strickData.wind = StrickParamtTransfUtil.UnPackWind(strickDataPacked.wind);
        strickData.firstHitPoint = StrickParamtTransfUtil.UnPackLandingPoint(strickDataPacked.landingPos);
        strickData.ballPos = StrickParamtTransfUtil.UnPackBallPos(strickDataPacked.ballPos);
        strickData.ballRot = StrickParamtTransfUtil.UnPackBallRot(strickDataPacked.ballRot);
        strickData.useClubLvl = strickDataPacked.clubLv;

        strickData.clubAttrs.Clear();
        strickData.clubAttrs.AddRange(strickDataPacked.ClubAttributes);

        strickData.ballSkills.Clear();
        strickData.ballSkills.AddRange(strickDataPacked.BallSkills);

        strickData.clubAngle = strickDataPacked.clubAngle;
    }

    public int GetGolfOnServerArea(Tags.GolfArea golfArea)
    {
        switch (golfArea)
        {
            case Tags.GolfArea.guo_ling:
                return ComState.area_green;
            case Tags.GolfArea.guo_ling_bian_yuan:
                return ComState.area_green_edge;
            case Tags.GolfArea.qiu_dao:
                return ComState.area_fairway;
            case Tags.GolfArea.chang_cao_qu:
                return ComState.area_long_grass;
            case Tags.GolfArea.sha_keng:
                return ComState.area_sand_pit;
            case Tags.GolfArea.shui:
                return ComState.area_water;
            case Tags.GolfArea.jie_wai:
                return ComState.area_out_bounds;
            case Tags.GolfArea.jie_wai_outer_bounds:
                return ComState.area_out_bounds;
            case Tags.GolfArea.hole:
                return ComState.area_hole;
            case Tags.GolfArea.qiu_dong_qiang_bi:
                return ComState.area_hole_inner;
            case Tags.GolfArea.qiu_zuo:
                return ComState.area_ball_seat;
            case Tags.GolfArea.fa_qiu_tai_di_mian:
                return ComState.area_ball_seat_ground;
            case Tags.GolfArea.shu_zhi:
                return ComState.area_out_bounds;
            case Tags.GolfArea.shu_gan:
                return ComState.area_out_bounds;
            case Tags.GolfArea.qi_gan:
                return ComState.area_out_bounds;
        }

        return ComState.area_out_bounds;
    }

    public static Tags.GolfArea GetGolfOnArea(IntPtr ptrSimulateMgr, Vector3 golfPosition)
    {
        Tags.GolfArea golfArea = Tags.GolfArea.jie_wai;

        golfPosition.y += 1;
        Ray ray = new Ray(golfPosition, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ptrSimulateMgr, ray, out hit, 100, GameMainLogic.GetAllCollisionLayer()))
        {
            string tag = hit.collider.tag;
            if (tag.Equals(Tags.guo_ling))
            {
                golfArea = Tags.GolfArea.guo_ling;
            }
            else if (tag.Equals(Tags.guo_ling_bian_yuan))
            {
                golfArea = Tags.GolfArea.guo_ling_bian_yuan;
            }
            else if (tag.Equals(Tags.qiu_dao))
            {
                golfArea = Tags.GolfArea.qiu_dao;
            }
            else if (tag.Equals(Tags.chang_cao_qu))
            {
                golfArea = Tags.GolfArea.chang_cao_qu;
            }
            else if (tag.Equals(Tags.sha_keng))
            {
                golfArea = Tags.GolfArea.sha_keng;
            }
            else if (tag.Equals(Tags.shui))
            {
                golfArea = Tags.GolfArea.shui;
            }
            else if (tag.Equals(Tags.jie_wai))
            {
                golfArea = Tags.GolfArea.jie_wai;
            }
            else if (tag.Equals(Tags.jie_wai_outer_bounds))
            {
                golfArea = Tags.GolfArea.jie_wai_outer_bounds;
            }
            else if (tag.Equals(Tags.hole))
            {
                golfArea = Tags.GolfArea.hole;
            }
            else if (tag.Equals(Tags.qiu_dong_qiang_bi))
            {
                golfArea = Tags.GolfArea.qiu_dong_qiang_bi;
            }
            else if (tag.Equals(Tags.qiu_zuo))
            {
                golfArea = Tags.GolfArea.qiu_zuo;
            }
            else if (tag.Equals(Tags.fa_qiu_tai_di_mian))
            {
                golfArea = Tags.GolfArea.fa_qiu_tai_di_mian;
            }
            else if (tag.Equals(Tags.shu_zhi))
            {
                golfArea = Tags.GolfArea.jie_wai;
            }
            else if (tag.Equals(Tags.shu_gan))
            {
                golfArea = Tags.GolfArea.jie_wai;
            }
            else if (tag.Equals(Tags.qi_gan))
            {
                golfArea = Tags.GolfArea.jie_wai;
            }
            
        }

        return golfArea;
    }

    public  bool isGolfStopMoveCall = false;
    public  bool isStopByKinematicToHole = false;
    public  Tags.GolfArea golfStopArea;
    public void setGolfStopFlag(Rigidbody ballRigidBody, Tags.GolfArea area)
    {
        setGolfStopFlag(ballRigidBody, area, false);
    }

    public void setGolfStopFlag(Rigidbody ballRigidBody, Tags.GolfArea area, bool pIsKinematicToHole)
    {
        Console.WriteLine("-------------------------------------------setGolfStopFlag------------------" + area);
        if (!ballRigidBody.isKinematic)
        {
            ballRigidBody.angularVelocity = Vector3.zero;
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.isKinematic = true;
        }
        

        isGolfStopMoveCall = true;
        isStopByKinematicToHole = pIsKinematicToHole;
        golfStopArea = area;
        if(golfStopArea == Tags.GolfArea.qiu_dong_qiang_bi)
        {
            golfStopArea = Tags.GolfArea.hole;
        }
    }

    public int GetStrickType()
    {
        int strickType;
        if (strickData.type == MiscState.strick_swing)
        {
            strickType = TextTipManager.GetSwingStrickResultType(strickData.pointerAngle);
        }
        else
        {
            strickType = TextTipManager.GetPushStrickResultType(strickData.golfAngle, strickData.pointerAngle);
        }
        return strickType;
    }

    public bool HasBounced()
    {
        return hasBouncedOnGround;
    }

    public int BallArea()
    {
        return GetGolfOnServerArea(golfStopArea);;
    }

    public bool IsStartOnGreen(IntPtr ptrSimulateMgr)
    {
        Tags.GolfArea golfRoundBeginArea = GetGolfOnArea(ptrSimulateMgr, strickData.ballPos);
        if (golfRoundBeginArea == Tags.GolfArea.guo_ling)
        { 
            return true;
        }
        else 
        {
            return false;
        }
    }
}

/// <summary>
/// 计算球的运动轨迹需要的参数
/// </summary>
public class StrickDataRealValue
{
    public int type;

    public float force;

    public float pointerAngle;

    public float golfAngle;

    public int useClubId;

    public int useClubLvl;

    public int ballId;

    public float topBottomSpinEulerAngle;

    public float sideSpinEulerAngle;

    public Vector3 wind;

    public Vector3 firstHitPoint;

    public Vector3 ballPos;

    public Quaternion ballRot;

    public List<int> clubAttrs = new List<int>();
    public List<CCItem> ballSkills = new List<CCItem>();

    public int clubAngle;
}

public class CCStrickDataLocal
{
    public int type;

    public long force;

    public long angle;

    public long pointerAngle;

    public int clubID;

    public int ballId;

    public CCVec3FloatToLongLocal hitPoint = new CCVec3FloatToLongLocal();

    public CCVec3FloatToLongLocal wind = new CCVec3FloatToLongLocal();

    public CCVec3FloatToLongLocal landingPos = new CCVec3FloatToLongLocal();

    public CCVec3FloatToLongLocal ballPos = new CCVec3FloatToLongLocal();

    public CCVec4FloatToLongLocal ballRot = new CCVec4FloatToLongLocal();

    public int clubLv;

    public IList<int> ClubAttributes = new List<int>();
    public IList<CCItem> BallSkills = new List<CCItem>();

    public int clubAngle;
}

public class CCVec3FloatToLongLocal
{
    public long x;
    public long y;
    public long z;

    public CCVec3FloatToLongLocal()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public CCVec3FloatToLongLocal(long x, long y, long z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Set(CCVec3FloatToLong value)
    {
        x = value.X;
        y = value.Y;
        z = value.Z;
    }

    public void Set(CCVec3FloatToLongLocal value)
    {
        x = value.x;
        y = value.y;
        z = value.z;
    }

    public void Set(long x, long y, long z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void SetIntpltValue(CCVec3FloatToLong v1, CCVec3FloatToLong v2, float p)
    {
        x = (long)(v1.X + (v2.X - v1.X) * p);
        y = (long)(v1.Y + (v2.Y - v1.Y) * p);
        z = (long)(v1.Z + (v2.Z - v1.Z) * p);
    }
}

public class CCVec4FloatToLongLocal
{
    public long x;
    public long y;
    public long z;
    public long w;

    public void Set(CCVec4FloatToLong value)
    {
        x = value.X;
        y = value.Y;
        z = value.Z;
        w = value.W;
    }
}

