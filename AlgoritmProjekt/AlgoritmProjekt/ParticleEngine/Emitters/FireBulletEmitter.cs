using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.ParticleEngine.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.ParticleEngine.Emitters
{
    class FireBulletEmitter : Emitter
    {
        Texture2D texture;
        Random rand;
        float speed;
        int size;

        public FireBulletEmitter(Texture2D texture, Vector2 position, int size, float speed, Vector2 target)
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.size = size;
            nrParticles = 1;
            myLifeTime = 10;
            SetDirection(target);
            rand = new Random();
        }

        public override void Update(ref float time)
        {
            RemoveParticles(time);
            EmitParticles();

            position += direction * speed * time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        void SetDirection(Vector2 target)
        {
            if (target != Vector2.Zero)
            {
                direction = target - myPosition;
                direction.Normalize();
            }
        }

        protected override Particle GenerateParticle()
        {
            float lifetime = 20 + rand.Next(-5, 10);
            return new FireParticle(texture, position, Vector2.Zero, lifetime, size);
        }

        protected override void EmitParticles()
        {
            particles.Add(GenerateParticle());
        }
    }
}
