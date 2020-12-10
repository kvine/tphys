using System.Collections;
using System;
using System.Collections.Generic;

public class ClubCfgData
{
    public const int index_accuracy = 1;

    public const int index_guide = 5;

    public const int total_attr = 6;
   
    public static float GetProperty(IList<int> list, int propertyIndex, int level)
    {
        int index = level * total_attr + propertyIndex;
        if (index < 0 || index >= list.Count)
        {
            return -1;
        }

        if (propertyIndex == index_guide)
        {
            return list[index] / 100f;
        }

        return list[index];
    }
}
