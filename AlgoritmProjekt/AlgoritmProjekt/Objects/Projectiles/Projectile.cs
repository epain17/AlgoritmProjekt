using AlgoritmProjekt.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Projectiles
{
    class Projectile : Tile
    {
        Vector2 direction;
        float speed;
        float lifeSpan, lifeTime;

        public Vector2 Position
        {
            get { return position; }
        }

        public override bool CheckMyCollision(Tile target)
        {
            if (target.myHitBox.Contains(myPosition))
            {
                InstaKillMe();
                return true;
            }
            return false;
        }

        public Projectile(Texture2D texture, Vector2 position, int size, Vector2 targetVect)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.alive = true;
            this.size = size;
            lifeTime = 200;
            speed = 6;
            Shoot(targetVect);
        }

        public override void Update()
        {
            velocity = direction * speed;
            position += velocity;
            LifeCycle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Shoot(Vector2 targetVect)
        {
            direction = targetVect - position;
            direction.Normalize();
        }

        void LifeCycle()
        {
            ++lifeSpan;
            if (lifeSpan > lifeTime)
            {
                alive = false;
            }
        }

        public void InstaKillMe()
        {
            lifeSpan = 1000;
        }
    }
}
