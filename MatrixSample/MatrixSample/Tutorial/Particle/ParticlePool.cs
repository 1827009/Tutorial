using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MatrixSample.Tutorial.Matrix;

namespace Tutorial
{
    class ParticlePool
    {
        private static int POOL_SIZE = 100;
        private Particle[] particles_;

        private Particle firstAvailable_;

        public ParticlePool() {
            particles_ = new Particle[POOL_SIZE];
            for (int i = 0; i < POOL_SIZE; i++)
            {
                particles_[i] = new Particle();
            }

            firstAvailable_ = particles_[0];
            for (int i = 0; i < POOL_SIZE-1; i++)
            {
                particles_[i].posOrParticle.particle = particles_[i + 1];
            }
            particles_[POOL_SIZE - 1].posOrParticle.particle = null;
        }

        public void create(Vector3 pos, Vector3 posVal, int lifetime)
        {
            Debug.Assert(firstAvailable_ != null);

            Particle newParticle = firstAvailable_;
            firstAvailable_ = newParticle.posOrParticle.particle;

            newParticle.init(pos, posVal, lifetime);
        }

        public void animate()
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                if (particles_[i].animate())
                {
                    particles_[i].posOrParticle.particle = firstAvailable_;
                    firstAvailable_ = particles_[i];
                }
            }
        }
    }
}
