using System.Collections;

public class GameConfig
{
    // public const int one_pack_num = 9;
    public const float golf_collide_radiu = 0.04f;
    public const float golf_display_radius = 0.03f;

    public const float golf_stop_adjust_y = 0;
    public const float golf_hole_circle_radiu = 0.15f;

    public const int float_to_long_factor = 10000;

    //客户端收到的回合时间时，应该除去预留时间后做为当前的回合时间//
    // public const long reserve_time = 10000; //每个回合预留的时间5s（用于每回合球的运动，网络处理等的时间）//
    // public const long round_time = 40000; // 回合时间，30s//
    // public const long extra_game_round_time = 45000; // 加赛时首次的回合时间，35s （5s用于动画处理和加载）//

    // public const long room_prepare_time = 5000; //房间等待确认时间 5s//
    // public const long room_mgr_match_span_time = 5000; //匹配房间的处理时间间隔5s//

    // public const long player_heart_over_time = 120 * 1000; //玩家进程的最大心跳时间2min//

    // public const float ball_stop_wait_round_end_time = 0.3f;//球停止运动后，等待一定时间,回合在结束//


    // public const string player_mat_light_dir_name = "_LightDir";
    //public  Vector4 player_ui_light_dir = new Vector4(0.47f, 0.7f, -1.85f, 0);
    //public  Vector4 ball_ui_light_dir = new Vector4();

    // public const float pvp_round_begin_wait_opp = 4;
    // public const float pvp_round_end_wait_opp = 7;

    // public const int UpdateLandingPoint_ChangeBall = 1;
    // public const int UpdateLandingPoint_ChangeClubSuit = 2;

    /// <summary>
    /// 击球者在T时间击球， 观察者如果在 T+observer_strick_delay_max_time_ms时间内，指针还没有转到特定的击球角度， 则重连强制击球。
    /// 观察者操作的延时时间2秒，观察者指针最多转1个来回。也就是1.35 * 4 = 5.4秒 
    /// 设定最多等待8秒
    /// </summary>
    // public const float observer_strick_delay_max_time_ms = 8000;

    public long login_check_time_ms = 3000;

    //public const int open_box_req_extra_time = 50000;

    //public const long reconnect_app_pause_time_ms = 1000 * 60;
    //public const long reloading_app_pause_time_ms = 1000 * 60 * 10;

    //public const float qi_gan_qi_offset_y = 0.58f;
    //public readonly Vector3 qi_gan_qi_init_rotation = new Vector3(-90, 0, 0);

    public void SyncLoginCheckTime(long newValue)
    {
        if((newValue >= 500) && (newValue <= 10000))
        {
            login_check_time_ms = newValue;
        }
    }
}
