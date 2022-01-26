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
        public MeshGraphNode mesh;

        private float framesLeft_;
        
        public bool inUse { get { return framesLeft_ > 0; } }

        // 共用体でメモリを節約するらしい。エラー出るので保留
        //[StructLayout(LayoutKind.Explicit)]
        public struct PositionOrParticle{
            // 生きてる場合使用
            //[FieldOffset(0)]
            public Positions positions;

            // 死んでる場合使用
            //[FieldOffset(0)]
            public Particle particle;
        }
        public PositionOrParticle unionParticle;

        public void init(Vector3 pos, Vector3 moveVec, int lifetime)
        {
            Positions p = new Positions(pos, moveVec);
            unionParticle.positions = p;
            mesh = new MeshGraphNode();
            mesh.GetLocal().positionMatrix = Matrix3x3.CreateTrancerate(unionParticle.positions.position_);

            framesLeft_ = lifetime;
        }

        public bool animate(Microsoft.Xna.Framework.GameTime time)
        {
            if (!inUse) return false;

            framesLeft_ -= (float)time.ElapsedGameTime.TotalSeconds;
            
            mesh.GetLocal().positionMatrix *= Matrix3x3.CreateTrancerate(unionParticle.positions.moveVec_ * (float)time.ElapsedGameTime.TotalSeconds);
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
        public Vector3 moveVec_;

        public Positions(Vector3 pos, Vector3 posVal)
        {
            position_ = pos;
            moveVec_ = posVal;
        }

        public static Positions operator +(Positions a, Positions b)
        {
            Positions output;
            output.position_= a.position_+b.position_;
            output.moveVec_= a.moveVec_+b.moveVec_;
            return output;
        }
        public static Positions operator *(Positions a, Positions b)
        {
            Positions output;
            output.position_ = a.position_ * b.position_;
            output.moveVec_ = a.moveVec_ * b.moveVec_;
            return output;
        }
        public static Positions operator *(Positions a, float b)
        {
            Positions output;
            output.position_ = a.position_ * b;
            output.moveVec_ = a.moveVec_ * b;
            return output;
        }
    }
}
