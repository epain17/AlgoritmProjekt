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
    class Projectile : LivingTile
    {
        Vector2 direction;
        float speed;


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
            this.startPos = position;
            speed = 20000;
            Shoot(targetVect);
        }

        public override void Update(ref float time)
        {
            velocity = direction * speed * time;
            position += velocity * time;
            LifeCycle(time);
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

        void LifeCycle(float time)
        {
            if (Vector2.Distance(position, startPos) > size * 11)
            {
                alive = false;
            }
        }

        public void InstaKillMe()
        {
            alive = false;
        }
    }
}
