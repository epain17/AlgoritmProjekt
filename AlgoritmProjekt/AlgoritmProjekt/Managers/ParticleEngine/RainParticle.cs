using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class RainParticle : Particle
    {

        public RainParticle(Texture2D texture, Vector2 position, Vector2 velocity, float lifeTime, float size) 
            :base(texture, position, velocity, lifeTime, size)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;
            this.color = Color.White;
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            color = new Color(0.007f * lifeTime, 0.9f, 0.007f * lifeTime, 0.05f * lifeTime);
            spriteBatch.Draw(texture, position, null, color, 0, origin, 0.01f * lifeTime, SpriteEffects.None, 0);
        }
    }
}
