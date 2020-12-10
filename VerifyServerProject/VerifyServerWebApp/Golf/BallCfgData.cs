using System.Collections;
using System;
using System.Collections.Generic;
using com.golf.proto;

public class BallCfgData
{
    public static int GetWindReduce(IList<CCItem> skillCCItemList)
    {
        for(int i = 0; i < skillCCItemList.Count; i++)
        {
            if(skillCCItemList[i].Type == BallSkillCfgData.wind_reduce)
            {
                return skillCCItemList[i].Id;
            }
        }

        return 0;
    }
}

public class BallSkillCfgData
{
    public const int wind_reduce = 2;
}