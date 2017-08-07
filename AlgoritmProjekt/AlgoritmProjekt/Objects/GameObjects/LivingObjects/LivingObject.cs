using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmProjekt.Objects.GameObjects.LivingObjects
{
    class LivingObject : GameObject
    {
        public bool Alive()
        {
            if (myHP <= 0)
                return false;
            return true;
        }
        public float mySpeed;
        public int myHP;

        protected Vector2 myDirection;

        public LivingObject(Vector2 position, int size, int hp, float speed)
            :base(position, size)
        {
            myPosition = position;
            mySize = size;
            myHP = hp;
            mySpeed = speed;
        }

        public override void Update(float time)
        {
            base.Update(time);
            myPosition += myDirection * mySpeed * time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void ChangeHP(int amount)
        {
            myHP += amount;
        }

        public void SetDirection(Vector2 destination)
        {
            myDirection = destination - myPosition;
            myDirection.Normalize();
        }

        public void StopMoving()
        {
            myDirection = Vector2.Zero;
        }

        public void KillMe()
        {
            myHP = 0;
        }
    }
}
