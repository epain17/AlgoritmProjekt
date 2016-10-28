using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class Emitter
    {
        private List<Particle> particles = new List<Particle>();
        protected Vector2 position;
        private Random random;
        protected Texture2D texture;
        protected bool alive = true;
        protected int nrParticles = 15;
        protected float myLifeTime = 3f;
        int count;

        public bool IsAlive
        {
            get { return alive; }
        }

        public Emitter(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            
        }

        public void Update(float time)
        {
            myLifeTime -= time;
            if (myLifeTime <= 0)
                alive = false;

            Emit();
            RemoveParticles(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }

        private Particle GenerateNewParticle()
        {
            Vector2 velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            int lifeTime = 50 + random.Next(50);
            float size = 16;
            return new Particle(texture, position, velocity, lifeTime, size);
        }

        private void Emit()
        {
            if (count < nrParticles)
            {
                count++;
                for (int i = 0; i < nrParticles; i++)
                {
                    random = new Random();
                    particles.Add(GenerateNewParticle());
                }
            }
        }

        private void RemoveParticles(float time)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(time);
                if (particles[i].myLifeSpan <= 0)
                    particles.RemoveAt(i);
            }
        }
    }
}
