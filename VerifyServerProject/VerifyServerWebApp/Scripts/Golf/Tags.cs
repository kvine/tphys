//using UnityEngine;

public class Tags {

    public enum GolfArea
    {

        //GameMainLogic 中有关于是否出界的判定，更新GolfArea要注意

        guo_ling,
        guo_ling_bian_yuan,
        qiu_dao,
        chang_cao_qu,
        sha_keng,
        shui,
        jie_wai,
        jie_wai_outer_bounds,
        hole,
        qiu_dong_qiang_bi,
        qiu_zuo,
        fa_qiu_tai_di_mian,
        shu_zhi,
        shu_gan,
        qi_gan
    }

    public const string guo_ling = "guo_ling";
    public const string guo_ling_bian_yuan = "guo_ling_bian_yuan";
    public const string qiu_dao = "qiu_dao";
    public const string chang_cao_qu = "chang_cao_qu";
    public const string sha_keng = "sha_keng";
    public const string shui = "shui";
    public const string jie_wai = "jie_wai";
    public const string jie_wai_outer_bounds = "jie_wai_outer_bounds";
    public const string hole = "hole";
    public const string qiu_dong_qiang_bi = "qiu_dong_qiang_bi";
    public const string qiu_zuo = "qiu_zuo";
    public const string fa_qiu_tai_di_mian = "fa_qiu_tai_di_mian";
    public const string shu_zhi = "shu_zhi";
    public const string shu_gan = "shu_gan";
    public const string qi_gan = "qi_gan";


    //other
    public const string golf = "golf";
    public const string Seagull = "Seagull";

    /// <summary>
    /// Golf 停止时的状态判断
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    public static int GetGolfStopLocState(GolfArea area)
    {
        switch (area)
        {
            case GolfArea.guo_ling:
                return BallState.loc_in_bounds;
            case GolfArea.guo_ling_bian_yuan:
                return BallState.loc_in_bounds;
            case GolfArea.qiu_dao:
                return BallState.loc_in_bounds;
            case GolfArea.chang_cao_qu:
                return BallState.loc_in_bounds;
            case GolfArea.sha_keng:
                return BallState.loc_in_bounds;
            case GolfArea.shui:
                return BallState.loc_out_bounds;
            case GolfArea.jie_wai:
                return BallState.loc_out_bounds;
            case GolfArea.jie_wai_outer_bounds:
                return BallState.loc_out_bounds;
            case GolfArea.hole:
                return BallState.loc_in_hole;
            case GolfArea.qiu_dong_qiang_bi:
                return BallState.loc_out_bounds;
            case GolfArea.qiu_zuo:
                return BallState.loc_in_bounds;
            case GolfArea.fa_qiu_tai_di_mian:
                return BallState.loc_in_bounds;
            case GolfArea.shu_zhi:
                return BallState.loc_out_bounds;
            case GolfArea.shu_gan:
                return BallState.loc_out_bounds;
            case GolfArea.qi_gan:
                return BallState.loc_out_bounds;
            default:
                return BallState.loc_out_bounds;
        }
    }
}
public class BallState
{
    //球的位置状态
    public const int loc_none = 0;  // none
    public const int loc_in_bounds = 1;  // 界内
    public const int loc_out_bounds = 2;  // 界外
    public const int loc_in_hole = 3;  // 进洞

    //球的选择状态
    // public const int select_none = 0; // 没有选择
    // public const int select_unconsume = 1; //选择了未消耗
    // public const int select_consumed = 2; //选择了已消耗

}