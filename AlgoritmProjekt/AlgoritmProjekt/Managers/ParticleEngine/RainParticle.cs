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
        SpriteFont font;
        float startLife;
        Random rand = new Random();
        int stringNR;

        public RainParticle(Texture2D texture, SpriteFont font, Vector2 position, Vector2 velocity, float lifeTime, float size) 
            :base(texture, position, velocity, lifeTime, size)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;
            this.color = Color.White;
            this.startLife = lifeTime;
            stringNR = rand.Next(0, 9);
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (lifeTime >= startLife * 0.95f)
                color = Color.White;
            else
                color = new Color(0.0005f * lifeTime, 1, 0.0005f * lifeTime, 0.001f);
            spriteBatch.DrawString(font, "" + stringNR, position, color, 0, origin, 1, SpriteEffects.None, 0);
        }
    }
}
