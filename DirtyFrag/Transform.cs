using System;
using System.Collections.Generic;
using System.Text;

namespace DirtyFragSample
{
    class Transform
    {
        static Vector3 position;

        public static Transform origin()
        {
            //return 相対座標;
        }
        public Transform combine(Transform othe)
        {
            //return 座標変換
        }
    }

    struct Vector3 {
        public float x;
        public float y;
        public float z;
        public static const Vector3 ZERO = new Vector3(0, 0, 0);

        Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        Vector3(Vector3 v)
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
}
