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
    class Particle
    {
        protected Texture2D texture;
        protected Vector2 position, velocity, origin;
        protected Color color;
        protected float lifeTime, size;
        float rotate;

        public float myLifeSpan
        {
            get { return lifeTime; }
        }

        public float mySize
        {
            get { return size; }
            set { size = value; }
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float lifeTime, float size)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = new Color(Color.Red, lifeTime / 1);
        }

        public virtual void Update(float time)
        {
            lifeTime--;
            position += velocity;
            rotate += time;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotate, origin, 0.008f * lifeTime, SpriteEffects.None, 0);
        }
    }
}
