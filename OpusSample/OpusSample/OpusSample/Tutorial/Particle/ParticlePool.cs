using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tutorial.Matrix;
using Tutorial.Vector;

namespace Tutorial
{
    class ParticlePool
    {
        private static int POOL_SIZE = 100;
        private Particle[] particles_;

        private Particle firstAvailable_;

        public ParticlePool() {
            // リストを作成
            particles_ = new Particle[POOL_SIZE];
            for (int i = 0; i < POOL_SIZE; i++)
            {
                particles_[i] = new Particle();
            }


            firstAvailable_ = particles_[0];
            for (int i = 0; i < POOL_SIZE-1; i++)
            {
                particles_[i].unionParticle.particle = particles_[i + 1];
            }
            particles_[POOL_SIZE - 1].unionParticle.particle = null;
        }

        public void create(Vector3 pos, Vector3 posVal, int lifetime)
        {
            // 足りなくなったらErrorを出す
            //Debug.Assert(firstAvailable_ != null);

            // 足りなくなったらparticleを生成しない
            if (firstAvailable_ != null)
            {
                // 空になっているリストの先を指定
                Particle newParticle = firstAvailable_;
                firstAvailable_ = newParticle.unionParticle.particle;

                newParticle.init(pos, posVal, lifetime);
            }
        }

        public void animate(Microsoft.Xna.Framework.GameTime time)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                // particleが死んだら
                if (particles_[i].animate(time))
                {
                    // 空にして、そこを空になっている先頭と記憶する
                    particles_[i].unionParticle.particle = firstAvailable_;
                    firstAvailable_ = particles_[i];

                    Console.WriteLine("今の先頭index：" + i);
                }
            }
        }
    }
}
