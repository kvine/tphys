using System.Collections;
using com.golf.proto;
using VerifyServerWebApp.UnityEngine;
/// <summary>
/// Pack 的数据为加工后传输的数据， UnPack的数据为真实值
/// </summary>
public class StrickParamtTransfUtil
{
    public const int angle_scaled_f = 100;

    public static long PackForce(float value)
    {
        return MyMathUtils.ConvertFloatToLong(value);
    }

    public static float UnPackForce(long value)
    {
        return MyMathUtils.ConvertLongToFloat(value);
    }

    public static long PackAngle(float value)
    {
        float v1 = ((int)(value * angle_scaled_f)) * 1f / angle_scaled_f;

        return MyMathUtils.ConvertFloatToLong(v1);
    }

    public static float UnPackAngle(long value)
    {
        return MyMathUtils.ConvertLongToFloat(value);
    }

    public static long PackSpin(float value)
    {
        float v1 = ((int)(value * angle_scaled_f)) * 1f / angle_scaled_f;

        return MyMathUtils.ConvertFloatToLong(v1);
    }

    public static float UnPackSpin(long value)
    {
        return MyMathUtils.ConvertLongToFloat(value);
    }

    public static Vector3 UnPackWind(CCVec3FloatToLongLocal value)
    {
        float x = MyMathUtils.ConvertLongToFloat(value.x);
        float y = MyMathUtils.ConvertLongToFloat(value.y);
        float z = MyMathUtils.ConvertLongToFloat(value.z);

        return new Vector3(x, y, z);
    }

    public static void PackLandingPoint(CCVec3FloatToLongLocal packValue, Vector3 originValue)
    {
        packValue.x = MyMathUtils.ConvertFloatToLong(originValue.x);
        packValue.y = MyMathUtils.ConvertFloatToLong(originValue.y);
        packValue.z = MyMathUtils.ConvertFloatToLong(originValue.z);
    }

    public static Vector3 UnPackLandingPoint(CCVec3FloatToLongLocal value)
    {
        float x = MyMathUtils.ConvertLongToFloat(value.x);
        float y = MyMathUtils.ConvertLongToFloat(value.y);
        float z = MyMathUtils.ConvertLongToFloat(value.z);

        return new Vector3(x, y, z);
    }

    public static void PackBallPos(CCVec3FloatToLongLocal packValue, Vector3 originValue)
    {
        packValue.x = MyMathUtils.ConvertFloatToLong(originValue.x);
        packValue.y = MyMathUtils.ConvertFloatToLong(originValue.y);
        packValue.z = MyMathUtils.ConvertFloatToLong(originValue.z);
    }

    public static Vector3 UnPackBallPos(CCVec3FloatToLongLocal value)
    {
        float x = MyMathUtils.ConvertLongToFloat(value.x);
        float y = MyMathUtils.ConvertLongToFloat(value.y);
        float z = MyMathUtils.ConvertLongToFloat(value.z);

        return new Vector3(x, y, z);
    }

    public static void PackBallRot(CCVec4FloatToLongLocal packValue, Quaternion originValue)
    {
        packValue.x = MyMathUtils.ConvertFloatToLong(originValue.x);
        packValue.y = MyMathUtils.ConvertFloatToLong(originValue.y);
        packValue.z = MyMathUtils.ConvertFloatToLong(originValue.z);
        packValue.w = MyMathUtils.ConvertFloatToLong(originValue.w);
    }

    public static Quaternion UnPackBallRot(CCVec4FloatToLongLocal value)
    {
        float x = MyMathUtils.ConvertLongToFloat(value.x);
        float y = MyMathUtils.ConvertLongToFloat(value.y);
        float z = MyMathUtils.ConvertLongToFloat(value.z);
        float w = MyMathUtils.ConvertLongToFloat(value.w);

        return new Quaternion(x, y, z, w);
    }
}
