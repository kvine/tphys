using System;
public class ComState
{//场景中的区域
    public const int area_green = 0; // 果岭
    public const int area_green_edge = 1; // 果岭边缘
    public const int area_fairway = 2; // 球道
    public const int area_long_grass = 3; // 长草区
    public const int area_sand_pit = 4; // 沙坑
    public const int area_water = 5; // 水
    public const int area_out_bounds = 6; // 界外
    public const int area_hole = 7; // 球洞
    public const int area_hole_inner = 8; // 球洞内壁
    public const int area_ball_seat = 9; // 球座
    public const int area_ball_seat_ground = 10; // 发球台地面
    public const int area_branch = 11; // 树枝
    public const int area_trunk = 12; // 树干


    //strick type
    public const int strick_type_perfect = 0; //命中靶心
    public const int strick_type_great = 1; //命中靶子较中央位置
    public const int strick_type_good = 2;  // 命中靶子边缘区域
    public const int strick_type_normal = 3; // 偏离标靶一定范围
    public const int strick_type_hook = 4;  //较大幅度偏向左边
    public const int strick_type_slice = 5; //较大幅度偏向右边
}

public class MiscState
{
    public const int strick_swing = 0;
    public const int strick_push = 1;
}