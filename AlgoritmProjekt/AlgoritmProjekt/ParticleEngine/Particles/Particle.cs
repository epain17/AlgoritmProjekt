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
        protected Vector2 position, velocity, origin;
        protected Color color;
        protected float lifeTime, size;
        protected float startLife;


        public float myLifeSpan
        {
            get { return lifeTime; }
        }

        public float mySize
        {
            get { return size; }
            set { size = value; }
        }

        public Particle(Vector2 position, Vector2 velocity, float lifeTime, float size)
        {
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.startLife = lifeTime;
            this.size = size;

            origin = new Vector2();
            color = new Color(Color.Red, lifeTime / 1);
        }

        public virtual void Update(float time)
        {
            lifeTime--;
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
