using AlgoritmProjekt.Managers.ParticleEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.ParticleEngine.Emitters
{
    class NeonPulse : Emitter
    {
        Texture2D texture;

        public NeonPulse(Texture2D texture, Vector2 position)
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            nrParticles = 8;
            myLifeTime = 1;
            velocity = Vector2.Zero;
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override Particle GenerateParticle()
        {
            Vector2 velocity = new Vector2(random.Next(-2, 2), random.Next(-2, 2));
            int lifeTime = 20;
            float size = 32;
            return new TextureParticle(texture, position, velocity, lifeTime, size);

        }
    }
}
