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
        Vector2 velocity;
        float speed;
        float lifeSpan, lifeTime;

        bool killMe = false;

        public bool DeadShot
        {
            get { return killMe; }
            set { killMe = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Projectile(Texture2D texture, Vector2 position, int size, Vector2 targetVect)
            :base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            lifeTime = 200;
            speed = 10f;
            Shoot(targetVect);
        }

        public void Update()
        {
            position += (velocity * speed);
            LifeCycle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Shoot(Vector2 targetVect)
        {
            velocity = targetVect - position;
            velocity.Normalize();
        }

        public override bool CheckMyCollision(Tile target)
        {
            if(Vector2.Distance(target.myPosition, position) < mySize + target.mySize)
            {
                InstaKillMe();
                return true;
            }
            return false;
        }

        void LifeCycle()
        {
            ++lifeSpan;
            if (lifeSpan > lifeTime)
            {
                killMe = true;
            }
        }

        public void InstaKillMe()
        {
            killMe = true;
        }
    }
}
