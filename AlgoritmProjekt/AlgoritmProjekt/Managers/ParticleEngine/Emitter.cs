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
        protected Vector2 velocity;
        private Random random;
        protected Texture2D texture;
        protected bool alive = true;
        protected int nrParticles = 10;
        protected float myLifeTime;
        int count, switchCase;

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
            get { return velocity; }
            set { velocity = value; }
        }

        public Emitter(Texture2D texture, Vector2 position, int switchCase)
        {
            this.texture = texture;
            this.position = position;
            this.switchCase = switchCase;
            switch (switchCase)
            {
                case 1:
                    myLifeTime = 3;
                    velocity = Vector2.Zero;
                    break;
                case 2:
                    myLifeTime = 8;
                    velocity.Y = 2;

                    break;
            }
        }

        public void Update(float time)
        {
            myLifeTime -= time;
            if (myLifeTime <= 0)
                alive = false;
            random = new Random();
            switch (switchCase)
            {
                case 1:
                    EmitEnemies();
                    break;
                case 2:
                    EmitRain();
                    break;
            }
            RemoveParticles(time);
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }

        private Particle EnemyParticle()
        {
            Vector2 velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            int lifeTime = 50 + random.Next(50);
            float size = 16;
            return new Particle(texture, position, velocity, lifeTime, size);
        }

        private void EmitEnemies()
        {
            if (count < nrParticles)
            {
                count++;
                for (int i = 0; i < nrParticles; i++)
                {
                    particles.Add(EnemyParticle());
                }
            }
        }

        private Particle RainParticle()
        {
            int lifeTime = 50 + random.Next(50);
            return new RainParticle(texture, position, Vector2.Zero, lifeTime, 16);
        }

        private void EmitRain()
        {
            particles.Add(RainParticle());
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
