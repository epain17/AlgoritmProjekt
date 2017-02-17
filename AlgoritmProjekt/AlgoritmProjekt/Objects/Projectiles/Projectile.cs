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
    class Projectile : MovingTile
    {

        int tileRange;

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
            this.myTexture = texture;
            this.position = position;
            this.alive = true;
            this.size = size;
            this.startPos = position;
            tileRange = 8;
            speed = 200;
            SetDirection(targetVect);
            velocity = direction * speed;
        }

        public override void Update(ref float time)
        {
            position += velocity * time;
            LifeCycle(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, position, Color.White);
        }

        void LifeCycle(float time)
        {
            if (Vector2.Distance(position, startPos) > size * tileRange)
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
