using VerifyServerWebApp.UnityEngine;
using System;
// using System.Collections;
// using System.Collections.Generic;
// using com.golf.proto;

public class GameMainLogic
{
    public static bool GetHitGround(IntPtr ptrSimulateMgr, Vector3 position, out RaycastHit hit)
    {
        position.y = 1000;
        Ray ray = new Ray(position, Vector3.down);
        bool isHit = Physics.Raycast(ptrSimulateMgr, ray, out hit, 2000f, getGroundLayer());
        return isHit;
    }

    private static int getGroundLayer()
    {
        return PhysicsUtil.getGroundLayer();
    }

    public static int GetAllCollisionLayer()
    {
        int groundLayer = 1 << LayerMask.NameToLayer(Layers.ground);
        int waterLayer = 1 << LayerMask.NameToLayer(Layers.water);
        int outerBound = 1 << LayerMask.NameToLayer(Layers.outer_bounds);
        int defaultLayer = 1 << LayerMask.NameToLayer(Layers.defaultLayer);
        return groundLayer | waterLayer | outerBound | defaultLayer;
    }

    public static Tags.GolfArea GetGolfOnArea(IntPtr ptrSimulateMgr, Vector3 golfPosition)
    {
        return PhysicsUtil.GetGolfOnArea(ptrSimulateMgr, golfPosition);
    }
}