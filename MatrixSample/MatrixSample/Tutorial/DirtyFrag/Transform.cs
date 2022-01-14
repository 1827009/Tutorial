using System;
using System.Collections.Generic;
using System.Text;
using MatrixSample.Tutorial.Matrix;

namespace Tutorial
{
    class Transform
    {
        static Vector3 position;

        public static Transform origin()
        {
            //return 相対座標;
            return new Transform();
        }
        public Transform combine(Transform othe)
        {
            //return 座標変換
            return new Transform();
        }
    }

}
