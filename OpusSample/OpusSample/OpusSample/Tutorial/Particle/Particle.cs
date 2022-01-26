using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Tutorial.Matrix;
using Tutorial.Vector;

namespace Tutorial
{
    class Particle
    {
        MeshGraphNode mesh;

        private float framesLeft_;
        
        public bool inUse { get { return framesLeft_ > 0; } }

        //[StructLayout(LayoutKind.Explicit)]
        public struct union{
            //[FieldOffset(0)]
            public Positions positions;

            //[FieldOffset(0)]
            public Particle particle;
        }
        public union posOrParticle;

        public void init(Vector3 pos, Vector3 posVel, int lifetime)
        {
            Positions p = new Positions(pos, posVel);
            posOrParticle.positions = p;
            mesh = new MeshGraphNode();

            framesLeft_ = lifetime;
        }

        public bool animate(Microsoft.Xna.Framework.GameTime time)
        {
            if (!inUse) return false;

            framesLeft_ -= (float)time.ElapsedGameTime.TotalSeconds;
            
            mesh.GetLocal().positionMatrix *= Matrix3x3.CreateTrancerate(posOrParticle.positions.positionVel_ * (float)time.ElapsedGameTime.TotalSeconds);
            mesh.render(MeshTransform.origin, mesh.Dirty);

            // このフレームで死んだら
            if (framesLeft_ <= 0)
            {
                mesh.Destroy();
                return true;
            }
            return false;
        }


    }
    struct Positions
    {
        // 位置
        public Vector3 position_;
        // 移動速度
        public Vector3 positionVel_;

        public Positions(Vector3 pos, Vector3 posVal)
        {
            position_ = pos;
            positionVel_ = posVal;
        }

        public static Positions operator +(Positions a, Positions b)
        {
            Positions output;
            output.position_= a.position_+b.position_;
            output.positionVel_= a.positionVel_+b.positionVel_;
            return output;
        }
        public static Positions operator *(Positions a, Positions b)
        {
            Positions output;
            output.position_ = a.position_ * b.position_;
            output.positionVel_ = a.positionVel_ * b.positionVel_;
            return output;
        }
        public static Positions operator *(Positions a, float b)
        {
            Positions output;
            output.position_ = a.position_ * b;
            output.positionVel_ = a.positionVel_ * b;
            return output;
        }
    }
}
