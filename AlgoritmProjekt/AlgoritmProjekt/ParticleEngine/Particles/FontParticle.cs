using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class FontParticle : Particle
    {
        SpriteFont font;
        Random rand = new Random();
        int stringNR;

        public FontParticle(SpriteFont font, Vector2 position, Vector2 velocity, float lifeTime, float size) 
            :base(position, velocity, lifeTime, size)
        {
            this.font = font;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;
            this.color = Color.White;
            stringNR = rand.Next(0, 10);
            origin = new Vector2(font.Texture.Width / 2, font.Texture.Height / 2);
        }

        public override void Update(float time)
        {
            base.Update(time);
            if (lifeTime >= startLife * 0.98f)
                color = Color.White;
            else
                color = new Color(0, 0.8f * (0.003f * lifeTime), 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "" + stringNR, position, color, 0, origin, 0.7f, SpriteEffects.None, 0);
        }
    }
}
