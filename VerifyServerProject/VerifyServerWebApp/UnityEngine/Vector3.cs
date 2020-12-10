using System;
namespace VerifyServerWebApp.UnityEngine
{
    public struct Vector3
    {
    public const float kEpsilon = 1e-005f;

    public float x;
        public float y;
        public float z;

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        //public static bool operator !=(Vector3 lhs, Vector3 rhs)
        //{
        //    return (lhs.x != rhs.x) || (lhs.y != rhs.y) || (lhs.z != rhs.z);
        //}
        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }
        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        static float off = 0.0000001f;
        public static Vector3 operator /(Vector3 a, float d)
        {
            if (d > -off && d < off)
            {
                throw new System.Exception("a is 0");
            }
            d = 1 / d;
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        //public static bool operator ==(Vector3 lhs, Vector3 rhs)
        //{
        //    return (lhs.x == rhs.x) && (lhs.y == rhs.y) && (lhs.z == rhs.z);
        //}

        public static Vector3 back { get { return new Vector3(0, 0, -1); } }
        public static Vector3 down { get { return new Vector3(0, -1, 0); } }
        public static Vector3 forward { get { return new Vector3(0, 0, 1); } }

        public static Vector3 left { get { return new Vector3(-1, 0, 0); } }
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt(sqrMagnitude);
            }
        }
        public Vector3 normalized
        {
            get
            {
                float m = magnitude;
                if (magnitude > 0.0f)
                {
                    m = 1 / m;
                    return new Vector3(x*m, y*m, z*m);
                }
                return new Vector3(0,0,0);
            }
        }

        public void Normalize()
        {
            float m = magnitude;
            if (magnitude > 0.0f)
            {
                m = 1 / m;
                this *= m;
            }
        }
        public static Vector3 one { get { return new Vector3(1, 1, 1); } }
        public static Vector3 right { get { return new Vector3(1, 0, 0); } }
        public float sqrMagnitude { get { return x * x + y * y + z * z; } }
        public static Vector3 up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 zero { get { return new Vector3(0, 0, 0); } }

        // public float this[int index] { get; set; }

        public static float Angle(Vector3 from, Vector3 to)
        {
            float cos = Vector3.Dot(from, to) / (from.magnitude * to.magnitude);
            return Mathf.Acos(cos) * 180 / Mathf.PI;
        }

        //public static Vector3 ClampMagnitude(Vector3 vector, float maxLength);
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x
                );
        }
        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 v = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return v.magnitude;
        }
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }
        //public override bool Equals(object other)
        //{
        //    return false;
        //}

        public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            float t1 = 1 - t;
            return new Vector3(from.x * t1 + to.x * t, from.y * t1 + to.y * t, from.z * t1 + to.z * t);
        }
        public static float Magnitude(Vector3 a)
        {
            float sq = a.x * a.x + a.y * a.y + a.z * a.z;
            return Mathf.Sqrt(sq);
        }
    //public static Vector3 Max(Vector3 lhs, Vector3 rhs);
    //public static Vector3 Min(Vector3 lhs, Vector3 rhs);
    //public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);
    //public void Normalize();
    //public static Vector3 Normalize(Vector3 value);
    //public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent);
    //public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal);
    //public static Vector3 Project(Vector3 vector, Vector3 onNormal);
    //public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal);
    public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
    {
        float d = Vector3.Dot(-inDirection, inNormal);
        Vector3 op = (d / inNormal.magnitude) * inNormal;

        op = inDirection + op * 2;
        return op;
    }
    //public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta);
    //public void Scale(Vector3 scale);
    //public static Vector3 Scale(Vector3 a, Vector3 b);
    public void Set(float new_x, float new_y, float new_z)
    {
        this.x = new_x;
        this.y = new_y;
        this.z = new_z;
    }
        //public static Vector3 Slerp(Vector3 from, Vector3 to, float t);
        //[ExcludeFromDocs]
        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime);
        //[ExcludeFromDocs]
        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed);
        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
        //public static float SqrMagnitude(Vector3 a);
        public override string ToString()
        {
            return string.Format("({0}f,{1}f,{2}f)", x.ToString("F6"), y.ToString("F6"), z.ToString("F6"));
        }
        //public string ToString(string format);
    }
}