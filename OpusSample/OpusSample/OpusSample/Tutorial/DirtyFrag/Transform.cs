using System;
using System.Collections.Generic;
using System.Text;
using Tutorial.Matrix;
using Tutorial.Vector;

namespace Tutorial.DirtyFrag
{
    class Transform
    {
        static Vector3 position;

        public static Transform origin()
        {
            //return 単位座標;
            return new Transform();
        }
        public Transform combine(Transform othe)
        {
            //return 座標変換
            return new Transform();
        }
    }

}
