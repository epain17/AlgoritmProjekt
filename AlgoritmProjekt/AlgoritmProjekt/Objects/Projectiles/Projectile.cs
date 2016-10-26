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
    class Projectile
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float speed;
        float lifeSpan, lifeTime;
        //int damage;

        bool killMe = false;

        public bool DeadShot
        {
            get { return killMe; }
            set { killMe = value; }
        }

        public Projectile(Texture2D texture, Vector2 position, Vector2 targetVect)
        {
            this.texture = texture;
            this.position = position;
            lifeTime = 200;
            speed = 5f;
            Shoot(targetVect);
        }

        public void Update()
        {
            position += (velocity * speed);
            LifeCycle();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Shoot(Vector2 targetVect)
        {
            velocity = targetVect - position;
            velocity.Normalize();
        }

        public bool Hit(Enemy enemy)
        {
            if(Vector2.Distance(enemy.myPosition, position) < 1f)
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
