using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixSample.Tutorial.Matrix
{
    struct Vector3
    {
        public float x;
        public float y;
        public float z;
        public static Vector3 ZERO = new Vector3(0, 0, 0);

        public String Text { get { return x + ",\t" + y + ",\t" + z; } }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(Vector3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            Vector3 output;
            output.x = a.x + b.x;
            output.y = a.y + b.y;
            output.z = a.z + b.z;
            return output;
        }
    }
    struct Vector2
    {
        public float x;
        public float y;
        public static Vector2 ZERO = new Vector2(0, 0);

        public String Text { get { return x + ",\t" + y; } }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }
        public static Vector2 operator +(Vector2 a, Vector3 b)
        {
            Vector2 output;
            output.x = a.x + b.x;
            output.y = a.y + b.y;
            return output;
        }
    }
}
