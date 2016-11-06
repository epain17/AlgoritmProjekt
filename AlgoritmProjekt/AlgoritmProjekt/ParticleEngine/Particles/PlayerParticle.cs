using AlgoritmProjekt.Managers.ParticleEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.ParticleEngine.Particles
{
    class PlayerParticle : TextureParticle
    {

        public PlayerParticle(Texture2D texture, Vector2 position, Vector2 velocity, float lifeTime, float size)
           :base(texture, position, velocity, lifeTime, size)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = new Color(Color.LimeGreen, lifeTime / 1);
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotate * 2, origin, 0.02f * (lifeTime / 2), SpriteEffects.None, 0);
        }
    }
}
