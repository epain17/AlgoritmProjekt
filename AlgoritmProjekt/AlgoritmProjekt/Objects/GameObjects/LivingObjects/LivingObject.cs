using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Utility.Handle_Levels.PCG;

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
        public float mySpeed,
            myWeightRatio;
        public int myHP;
        public Vector2 myDirection;
        public float myStartSpeed;

        public LivingObject(Vector2 position, int width, int height, int hp, float speed)
            : base(position, width, height)
        {
            myStartSpeed = speed;
            mySpeed = speed;
            myHP = hp;
            isBlockable = true;
            myWeightRatio = 1;
        }

        public override void Update(float time, TileGrid grid)
        {
            base.Update(time, grid);
            myPosition += myDirection * (mySpeed * myWeightRatio) * time;
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
