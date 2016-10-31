using AlgoritmProjekt.Objects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class TextureParticle : Particle
    {
        protected Texture2D texture;
        float rotate;

        public TextureParticle(Texture2D texture, Vector2 position, Vector2 velocity, float lifeTime, float size)
            : base(position, velocity, lifeTime, size)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = new Color(Color.Red, lifeTime / 1);
        }

        public override void Update(float time)
        {
            rotate += time;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotate, origin, 0.008f * lifeTime, SpriteEffects.None, 0);
        }
    }
}
