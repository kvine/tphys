using System;

//namespace VerifyServerWebApp.UnityEngine
//{
public struct Mathf
{
    public const float Deg2Rad = 0.0174532924F;
    public const float Epsilon = 1.4013e-045f;
    public const float Infinity = 1.0f / 0.0f;
    public const float NegativeInfinity = -1.0f / 0.0f;
    public const float PI = 3.14159274F;
    public const float Rad2Deg = 57.29578F;

    public static float Abs(float f)
    {
        return Math.Abs(f);
    }
    public static int Abs(int value)
    {
        return Math.Abs(value);
    }
    public static float Acos(float f)
    {
        return (float)Math.Acos(f);
    }
    //public static bool Approximately(float a, float b)
    //{
    //    return Math.
    //}
    public static float Asin(float f)
    {
        return (float)Math.Asin(f);
    }
    public static float Atan(float f)
    {
        return (float)Math.Atan(f);
    }
    public static float Atan2(float y, float x)
    {
        return (float)Math.Atan2(y, x);
    }
    // public static float Ceil(float f)
    // {
    //     return (float)Math.Ceiling(f);
    // }
    public static int CeilToInt(float f)
    {
        int tmp = (int)f;
        float left = f - tmp;
        if (f > 0)
        {
            if (left > 0.0f)
            {
                tmp += 1;
            }
        }
        return tmp;
    }

    // public static float Clamp(float value, float min, float max)
    // {
    //     return Math.Clamp(value, min, max);
    // }
    // public static int Clamp(int value, int min, int max)
    // {
    //     return Math.Clamp(value, min, max);
    // }
    // public static float Clamp01(float value)
    // {
    //     return Math.Clamp(value, 0, 1);
    // }
    //[WrapperlessIcall]
    //public static int ClosestPowerOfTwo(int value);
    public static float Cos(float f)
    {
        return (float)Math.Cos(f);
    }
    //public static float DeltaAngle(float current, float target);
    //public static float Exp(float power);
    //public static float Floor(float f);
    //public static int FloorToInt(float f);
    //public static float Gamma(float value, float absmax, float gamma);
    //[WrapperlessIcall]
    //public static float GammaToLinearSpace(float value);
    //public static float InverseLerp(float from, float to, float value);
    //[WrapperlessIcall]
    //public static bool IsPowerOfTwo(int value);
    //public static float Lerp(float from, float to, float t);
    //public static float LerpAngle(float a, float b, float t);
    //[WrapperlessIcall]
    //public static float LinearToGammaSpace(float value);
    public static float Log(float f)
    {
        return (float)Math.Log(f);
    }
    public static float Log(float f, float p)
    {
        return (float)Math.Log(f, p);
    }
    public static float Log10(float f)
    {
        return (float)Math.Log10(f);
    }
    //public static float Max(params float[] values)
    //{
    //    return (float)Math.Max()
    //}
    //public static int Max(params int[] values);
    //public static float Max(float a, float b);
    //public static int Max(int a, int b);
    //public static float Min(params float[] values);
    //public static int Min(params int[] values);
    public static float Min(float a, float b)
    {
        return Math.Min(a, b);
    }
    public static int Min(int a, int b)
    {
        return Math.Min(a, b);
    }
    //public static float MoveTowards(float current, float target, float maxDelta);
    //public static float MoveTowardsAngle(float current, float target, float maxDelta);
    //[WrapperlessIcall]
    //public static int NextPowerOfTwo(int value);
    //[WrapperlessIcall]
    //public static float PerlinNoise(float x, float y);
    //public static float PingPong(float t, float length);
    public static float Pow(float f, float p)
    {
        return (float)Math.Pow(f, p);
    }
    //public static float Repeat(float t, float length);
    //public static float Round(float f);
    public static int RoundToInt(float f)
    {
        int tmp = (int)f;
        float left = f - tmp;
        if (tmp % 2 == 0)
        {
            if (Math.Abs(left) > 0.5f)
            {
                if (tmp > 0) { tmp += 1; }
                else { tmp -= 1; }
            }
        }
        else
        {
            if (Math.Abs(left) >= 0.5f)
            {
                if (tmp > 0) { tmp += 1; }
                else { tmp -= 1; }
            }
        }
        return tmp;
    }

    //public static float Sign(float f)
    //{
    //    return Math.
    //}
    public static float Sin(float f)
    {
        return (float)Math.Sin(f);
    }
    //[ExcludeFromDocs]
    //public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime);
    //[ExcludeFromDocs]
    //public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);
    //public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
    //[ExcludeFromDocs]
    //public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime);
    //[ExcludeFromDocs]
    //public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed);
    //public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
    //public static float SmoothStep(float from, float to, float t);
    public static float Sqrt(float f)
    {
        return (float)Math.Sqrt(f);
    }
    public static float Tan(float f)
    {
        return (float)Math.Tan(f);
    }
}
//}
