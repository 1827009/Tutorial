using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using MatrixSample.Tutorial.Matrix;

namespace Tutorial
{
    class Particle
    {
        private int framesLeft_;
        
        public bool inUse { get { return framesLeft_ > 0; } }

        [StructLayout(LayoutKind.Explicit)]
        public struct union{
            [FieldOffset(0)]
            public Positions positions;

            [FieldOffset(0)]
            public Particle particle;
        }
        public union posOrParticle;

        public void init(Vector3 pos, Vector3 posVel, int lifetime) {
            Positions p = new Positions(pos, posVel);
            posOrParticle.positions = p;

            framesLeft_ = lifetime;
        }

        public bool animate()
        {
            if (!inUse) return false;

            framesLeft_--;
            posOrParticle.positions.position_ += posOrParticle.positions.positionVel_;

            return framesLeft_ <= 0;
        }


    }
    struct Positions
    {
        public Vector3 position_;
        public Vector3 positionVel_;

        public Positions(Vector3 pos, Vector3 posVal) {
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
    }
}
