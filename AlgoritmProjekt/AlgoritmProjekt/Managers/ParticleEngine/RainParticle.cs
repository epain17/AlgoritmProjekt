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
            this.color = new Color(0.3f, 0.9f, 0.3f);
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, origin, 0.7f, SpriteEffects.None, 0);
        }
    }
}
