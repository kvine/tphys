using System.Collections;

public class HitGolfPanel
{

    // public const float zhi_zhen_swing_angle = 28;//挥杆时指针转动的范围[-a, a]//
    // public const float zhi_zhen_push_angle = 28;//推杆时指针转动的范围[t-a, t+a]//
    // public const float zhi_zhen_extra_max_rv_f = 0.3f;//最大力度时，指针的转动速度 与 正常转动速度 的比例//
    // public const float zhi_zhen_extra_max_rotation_time_f = 0.23f;//最大力度时，指针的转动时间 与 正常转动时间 的比例//
    // public const float zhi_zhen_extra_max_v_k = 0.99f;//最大力度时， 指针匀加速运动Vt和V0的比例，k=V0/Vt;
    // public const float lun_pan_shake_f = 0.04f;//轮盘的抖动系数//
    // public const float zhi_zhen_shake_f = 0.04f;//指针的抖动系数//

    public const float maxDragDistance = 0.38f;//最大的拉球的距离//
    public const float perfectDragDistance = 0.326f;//完美的拉球的距离//
    public const float minDragDistance = 0.16f;//最小的拉球距离,拉球的距离超过这个值时，才会有UI显示//

    public const float minRadius = 110;//最小的半径//
    public const float perfectRadius = 170;//完美的半径//
    public const float maxRadius = 195;//最大的半径//
    public const float shakeRadiu = 175f;//半径大于这个时，开始抖动//

    //由抖动半径计算抖动距离//
    // public const float shakeDragDistance = perfectDragDistance + (shakeRadiu - perfectRadius) / (maxRadius - perfectRadius) * (maxDragDistance - perfectDragDistance);

    public const float circle_radiu = 27;//标靶的半径//
    // public const float point_circle_ratio = perfectRadius / circle_radiu;//球心到标靶的中心的距离 和 标靶的半径的比率//

    public const float pfct_efct_drag_angle_permit_offset = 1.2f;//UI上没有拉球角度的容忍值//
    public const float pfct_efct_radiu_permit_offset = 4;//UI上完美半径的容忍值//

    public static int GetZhiZhenRadiu(float dragGolfDis)
    {
        float radius = minRadius;

        if (dragGolfDis < perfectDragDistance)
        {
            radius = minRadius + (dragGolfDis - minDragDistance) / (perfectDragDistance - minDragDistance) * (perfectRadius - minRadius);
        }
        else
        {
            radius = perfectRadius + (dragGolfDis - perfectDragDistance) / (maxDragDistance - perfectDragDistance) * (maxRadius - perfectRadius);
        }

        return Mathf.RoundToInt(radius);
    }
}
