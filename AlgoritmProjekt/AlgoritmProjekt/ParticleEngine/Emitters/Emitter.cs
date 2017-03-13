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
        protected List<Particle> particles = new List<Particle>();
        protected Vector2 position;
        protected Vector2 direction;
        protected Random random;
        protected bool alive = true;
        protected int nrParticles = 10;
        protected int count;
        protected float myLifeTime;
        protected float timer, timeLimit = 0;

        public bool IsAlive
        {
            get { return alive; }
        }

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 myVelocity
        {
            get { return direction; }
            set { direction = value; }
        }

        public Emitter(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Update(ref float time)
        {
            RemoveParticles(time);
            myLifeTime -= time;
            if (myLifeTime <= 0)
                alive = false;
            random = new Random();
            timer++;
            if (timer > timeLimit)
            {
                timer = 0;
                EmitParticles();
            }
            position += direction;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }


        protected virtual Particle GenerateParticle()
        {
            Vector2 velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            return new Particle(position, velocity, 50 + random.Next(50), 16);
        }

        protected virtual void EmitParticles()
        {
            if (count < nrParticles)
            {
                count++;
                for (int i = 0; i < nrParticles; i++)
                {
                    particles.Add(GenerateParticle());
                }
            }
        }

        protected void RemoveParticles(float time)
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
