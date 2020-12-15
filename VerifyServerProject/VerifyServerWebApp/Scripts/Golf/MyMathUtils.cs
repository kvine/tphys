using System.Collections;
using System;
using VerifyServerWebApp.UnityEngine;
public class MyMathUtils
{

    public static bool isPointInCircle(Vector3 point, Vector3 center, float radiu)
    {
        point.y = 0;
        center.y = 0;

        if (Vector3.Distance(point, center) <= radiu)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 如果point.y小于等于center.y,并且point.xz 在圆内，返回true
    /// </summary>
    /// <param name="point"></param>
    /// <param name="center"></param>
    /// <param name="radiu"></param>
    /// <returns></returns>
    public static bool isPointBellowInCircle(Vector3 point, Vector3 center, float radiu)
    {
        if (point.y > center.y)
        {
            return false;
        }

        point.y = 0;
        center.y = 0;

        if (Vector3.Distance(point, center) <= radiu)
        {
            return true;
        }

        return false;
    }


    /** Returns whether the given line segment intersects the given circle.
	 * @param start The start point of the line segment
	 * @param end The end point of the line segment
	 * @param center The center of the circle
	 * @param squareRadius The squared radius of the circle
	 * @return Whether the line segment and the circle intersect */
    public static bool intersectSegmentCircle(float startX, float startY, float endX, float endY, float centerX, float centerY, float squareRadius)
    {
        Vector3 tmp = new Vector3(endX - startX, endY - startY, 0);
        Vector3 tmp1 = new Vector3(centerX - startX, centerY - startY, 0);
        Vector3 tmp2 = new Vector3();
        Vector3 tmp3 = new Vector3();
        float l = tmp.magnitude;
        tmp.Normalize();
        float u = Vector3.Dot(tmp1, tmp);
        if (u <= 0)
        {
            tmp2.Set(startX, startY, 0);
        }
        else if (u >= l)
        {
            tmp2.Set(endX, endY, 0);
        }
        else
        {
            tmp3 = tmp * u; // remember tmp is already normalized
            tmp2.Set(tmp3.x + startX, tmp3.y + startY, 0);
        }

        float x = centerX - tmp2.x;
        float y = centerY - tmp2.y;

        return x * x + y * y <= squareRadius;
    }

    public static float intersectLineCircleLen(float startX, float startY, float endX, float endY, float centerX, float centerY, float radiu)
    {
        float dis = distanceLinePoint(startX, startY, endX, endY, centerX, centerY);
        if (dis < radiu)
        {
            return Mathf.Sqrt(radiu * radiu - dis * dis) * 2;
        }

        return 0;
    }

    /** Returns the distance between the given line and point. Note the specified line is not a line segment. */
    public static float distanceLinePoint(float startX, float startY, float endX, float endY, float pointX, float pointY)
    {
        float normalLength = (float)Mathf.Sqrt((endX - startX) * (endX - startX) + (endY - startY) * (endY - startY));
        return Mathf.Abs((pointX - startX) * (endY - startY) - (pointY - startY) * (endX - startX)) / normalLength;
    }

    public static long clamp(long value, long min, long max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

    public static bool isNumberBetween(float a, float b, float c)
    {
        if (a >= b && a <= c)
        {
            return true;
        }
        else if (a >= c && a <= b)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static long ConvertFloatToLong(float value)
    {
        return (long)(value * GameConfig.float_to_long_factor);
    }

    public static float ConvertLongToFloat(long value)
    {
        return value * 1f / GameConfig.float_to_long_factor;
    }


    /// <summary>
    /// 与0度的夹角
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float GetMinAbsDeltaAngleToZero(float angle)
    {
        if (angle >= 0)
        {
            if (Mathf.Abs(angle) <= Mathf.Abs(360 - angle))
            {
                return Mathf.Abs(angle);
            }
            else
            {
                return Mathf.Abs(360 - angle);
            }
        }
        else
        {
            if (Mathf.Abs(angle) <= Mathf.Abs(-360 - angle))
            {
                return Mathf.Abs(angle);
            }
            else
            {
                return Mathf.Abs(-360 - angle);
            }
        }
    }

    /// <summary>
    /// 获取num的二进制的特定位数
    /// </summary>
    /// <param name="num"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int GetBits(int num, int index)
    {
        int ans = num & (1 << index);
        if (ans == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// 修改num的二进制的特定位
    /// </summary>
    /// <param name="num"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ModifyBits(int num, int index, int value)
    {
        if(value == 1)
        {
            num = num | (1 << index);
        }
        else
        {
            int t = ~(1 << index);
            num = num & t;
        }

        return num;
    }
}
