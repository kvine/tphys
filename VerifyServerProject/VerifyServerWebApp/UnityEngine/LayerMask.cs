using System;

//namespace VerifyServerWebApp.UnityEngine
//{
public struct LayerMask
{
    public static implicit operator LayerMask(int intVal)
    {
        LayerMask lm = new LayerMask
        {
            value = intVal
        };
        return lm;
    }
    public static implicit operator int(LayerMask mask)
    {
        return mask.value;
    }

    public int value { get; set; }
    public static int NameToLayer(string name)
    {
        int layer = 0;
        switch (name)
        {
            case Layers.defaultLayer: layer = 0; break;
            case Layers.ground: layer = 8; break;
            case Layers.water: layer = 4; break;
            case Layers.ui_2d: layer = 11; break;
            case Layers.ui_invisible: layer = 10; break;
            case Layers.outer_bounds: layer = 18; break;
            case Layers.player_model: layer = 17; break;
            case Layers.water_land_point: layer = 27; break;
            case Layers.golfTwo_down: layer = 29; break;
            case Layers.golf: layer = 13; break;
            case Layers.golf_two: layer = 9; break;
            case Layers.club: layer = 25; break;
            case Layers.ui_buy_golf_window_ball: layer = 23; break;
            case Layers.ui_game_in_ball: layer = 24; break;
        }
        return layer;
    }
}
//}
