using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class EnemyEmitter : Emitter
    {
        Texture2D texture;

        public EnemyEmitter(Texture2D texture, Vector2 position) 
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            nrParticles = 6;
            myLifeTime = 3;
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
            Vector2 velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            int lifeTime = 50 + random.Next(50);
            float size = 16;
            return new TextureParticle(texture, position, velocity, lifeTime, size);
        }
    }
}
