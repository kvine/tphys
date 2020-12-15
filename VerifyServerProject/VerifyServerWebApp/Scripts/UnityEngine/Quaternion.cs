namespace VerifyServerWebApp.UnityEngine
{
    public class Quaternion
    {
    public const float kEpsilon = 1e-006f;

    public float w;
    public float x;
    public float y;
    public float z;

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Quaternion(Quaternion q)
    {
        this.x = q.x;
        this.y = q.y;
        this.z = q.z;
        this.w = q.w;
    }

    //public static bool operator !=(Quaternion lhs, Quaternion rhs);
    public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
    {
        float tx = lhs.w * rhs.x + rhs.w * lhs.x + lhs.y * rhs.z - rhs.y * lhs.z;
        float ty = lhs.w * rhs.y + rhs.w * lhs.y + lhs.z * rhs.x - rhs.z * lhs.x;
        float tz = lhs.w * rhs.z + rhs.w * lhs.z + lhs.x * rhs.y - rhs.x * lhs.y;

        float w = lhs.w * rhs.w - rhs.x * lhs.x - lhs.y * rhs.y - rhs.z * lhs.z;

        return new Quaternion(tx, ty, tz, w);
    }
    public static Vector3 operator *(Quaternion rot, Vector3 point)
    {
        /*
         * # m11  m12   m13         x
           # m21  m22   m23   X     y
           # m31  m32   m33         z
         */
        float m11 = 1 - 2 * (rot.y * rot.y + rot.z * rot.z);
        float m12 = 2 * (rot.x * rot.y + rot.z * rot.w);
        float m13 = 2 * (rot.x * rot.z - rot.y * rot.w);

        float m21 = 2 * (rot.x * rot.y - rot.z * rot.w);
        float m22 = 1 - 2 * (rot.x * rot.x + rot.z * rot.z);
        float m23 = 2 * (rot.y * rot.z + rot.x * rot.w);

        float m31 = 2 * (rot.x * rot.z + rot.y * rot.w);
        float m32 = 2 * (rot.y * rot.z - rot.x * rot.w);
        float m33 = 1 - 2 * (rot.x * rot.x + rot.y * rot.y);

        Vector3 res = new Vector3(
            m11 * point.x + m21 * point.y + m31 * point.z,
            m12 * point.x + m22 * point.y + m32 * point.z,
            m13 * point.x + m23 * point.y + m33 * point.z
            );
        return res;
    }
    //public static bool operator ==(Quaternion lhs, Quaternion rhs);

    //public Vector3 eulerAngles { get; set; }
    public static Quaternion identity { get { return new Quaternion(0, 0, 0, 1); } }

    //public float this[int index] { get; set; }

    //public static float Angle(Quaternion a, Quaternion b)
    //{
    //}

    public static Quaternion AngleAxis(float angle, Vector3 unitAxis)
    {
        float angleRadians = angle * Mathf.Deg2Rad;
        float a = angleRadians * 0.5f;
        float s = Mathf.Sin(a);
        return new Quaternion(unitAxis.x * s, unitAxis.y * s, unitAxis.z * s, Mathf.Cos(a));
    }

    public static float Dot(Quaternion a, Quaternion b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }
    // public override bool Equals(object other);
    public static Quaternion Euler(Vector3 euler)
    {
        return Euler(euler.x, euler.y, euler.z);
    }
    public static Quaternion Euler(float x, float y, float z)
    {
        float heading = y * Mathf.Deg2Rad;
        float pitch = x * Mathf.Deg2Rad;
        float bank = z * Mathf.Deg2Rad;

        float cos_half_h = Mathf.Cos(heading / 2);
        float cos_half_p = Mathf.Cos(pitch / 2);
        float cos_half_b = Mathf.Cos(bank / 2);

        float sin_half_h = Mathf.Sin(heading / 2);
        float sin_half_p = Mathf.Sin(pitch / 2);
        float sin_half_b = Mathf.Sin(bank / 2);

        float w1 = cos_half_h * cos_half_p * cos_half_b + sin_half_h * sin_half_p * sin_half_b;

        float x1 = cos_half_h * sin_half_p * cos_half_b + sin_half_h * cos_half_p * sin_half_b;

        float y1 = sin_half_h * cos_half_p * cos_half_b - cos_half_h * sin_half_p * sin_half_b;

        float z1 = cos_half_h * cos_half_p * sin_half_b - sin_half_h * sin_half_p * cos_half_b;
        return new Quaternion(x1, y1, z1, w1);
    }
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public static Quaternion EulerAngles(Vector3 euler);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public static Quaternion EulerAngles(float x, float y, float z);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public static Quaternion EulerRotation(Vector3 euler);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public static Quaternion EulerRotation(float x, float y, float z);
    //public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection);
    //public override int GetHashCode();
    //public static Quaternion Inverse(Quaternion rotation){}
    public static Quaternion Lerp(Quaternion from, Quaternion to, float t)
    {
        float t1 = 1 - t;
        return new Quaternion(from.x * t1 + to.x * t, from.y * t1 + to.y * t, from.z * t1 + to.z * t, from.w * t1 + to.w * t);
    }
    //[ExcludeFromDocs]
    //public static Quaternion LookRotation(Vector3 forward);
    //public static Quaternion LookRotation(Vector3 forward, Vector3 upwards);
    //public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta);
    //public void Set(float new_x, float new_y, float new_z, float new_w);
    //[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
    //public void SetAxisAngle(Vector3 axis, float angle);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public void SetEulerAngles(Vector3 euler);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public void SetEulerAngles(float x, float y, float z);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public void SetEulerRotation(Vector3 euler);
    //[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
    //public void SetEulerRotation(float x, float y, float z);
    //public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection);
    //[ExcludeFromDocs]
    //public void SetLookRotation(Vector3 view);
    //public void SetLookRotation(Vector3 view, Vector3 up);

    public static Quaternion operator-(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, -q.w);
    }

    public static Quaternion operator-(Quaternion lhs, Quaternion rhs)
    {
        return new Quaternion(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
    }

    public static Quaternion operator+(Quaternion lhs, Quaternion rhs)
    {
        return new Quaternion(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
    }

    public static Quaternion operator*(float d, Quaternion q)
    {
        return new Quaternion(d * q.x, d * q.y, d * q.z, d * q.w);
    }

    public float MagnitudeSquared()
    {
        return x * x + y * y + z * z + w * w;
    }


    public float Magnitude()
    {
        return Mathf.Sqrt(MagnitudeSquared());
    }

    public float Normalize()
    {
        float mag = Magnitude();
        if(mag != 0.0f)
        {
            float imag = 1.0f / mag;
            x *= imag;
            y *= imag;
            z *= imag;
            w *= imag;
        }
        return mag;
    }
    public static Quaternion Slerp(Quaternion from, Quaternion to, float t)
    {
        // Only unit quaternions are valid rotations.
        // Normalize to avoid undefined behavior.
        from.Normalize();
        to.Normalize();

        // Compute the cosine of the angle between the two vectors.
        float dot = Dot(from, to);

        // If the dot product is negative, slerp won't take
        // the shorter path. Note that to and -to are equivalent when
        // the negation is applied to all four components. Fix by 
        // reversing one quaternion.
        if (dot < 0.0f)
        {
            to = -to;
            dot = -dot;
        }

        const double DOT_THRESHOLD = 0.9995;
        if (dot > DOT_THRESHOLD)
        {
            // If the inputs are too close for comfort, linearly interpolate
            // and normalize the result.

            Quaternion result = from + t * (to - from);
            result.Normalize();
            return result;
        }

        // Since dot is in range [0, DOT_THRESHOLD], acos is safe
        float theta_0 = Mathf.Acos(dot);        // theta_0 = angle between input vectors
        float theta = theta_0 * t;          // theta = angle between from and result
        float sin_theta = Mathf.Sin(theta);     // compute this value only once
        float sin_theta_0 = Mathf.Sin(theta_0); // compute this value only once

        float s0 = Mathf.Cos(theta) - dot * sin_theta / sin_theta_0;  // == sin(theta_0 - theta) / sin(theta_0);
        float s1 = sin_theta / sin_theta_0;

        return (s0 * from) + (s1 * to);
    }
    public void ToAngleAxis(out float angle, out Vector3 axis)
    {
        const float quatEpsilon = 1.0e-8f;
        float s2 = x * x + y * y + z * z;
        if (s2 < quatEpsilon * quatEpsilon) // can't extract a sensible axis
        {
            angle = 0.0f;
            axis = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            float s = 1 / Mathf.Sqrt(s2);
            axis = new Vector3(x, y, z) * s;
            angle = Mathf.Abs(w) < quatEpsilon ? Mathf.PI : Mathf.Atan2(s2 * s, w) * 2.0f;
        }
    }
    //[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
    //public void ToAxisAngle(out Vector3 axis, out float angle);
    //[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
    //public Vector3 ToEuler();
    //[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
    //public Vector3 ToEulerAngles();
    //[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
    //public static Vector3 ToEulerAngles(Quaternion rotation);
    public override string ToString()
    {
        return string.Format("({0}f,{1}f,{2}f,{3}f)", x.ToString("F6"), y.ToString("F6"), z.ToString("F6"), w.ToString("F6"));
    }
    //public string ToString(string format);
}
}
