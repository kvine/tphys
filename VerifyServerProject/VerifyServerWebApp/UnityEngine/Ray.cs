namespace VerifyServerWebApp.UnityEngine
{
    public struct Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            this.direction = direction;
            this.origin = origin;
        }

        public Vector3 direction { get; set; }
        public Vector3 origin { get; set; }

        public Vector3 GetPoint(float distance)
        {
            return origin + direction * distance;
        }
        //public override string ToString();
        //public string ToString(string format);
    }
}
