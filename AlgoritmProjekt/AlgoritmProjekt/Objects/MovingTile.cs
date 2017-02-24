using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects
{
    class MovingTile : Tile
    {
        public Vector2 direction, targetPos;
        public float speed;

        protected bool alive = true;
        protected int hp;

        public int myHP
        {
            get { return hp; }
            set { hp = value; }
        }

        public bool iamAlive
        {
            get { return alive; }
            set { alive = value; }
        }

        public MovingTile(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.speed = 100;
            
        }

        public override void Update(ref float time)
        {
            position += direction * speed * time;
        }

        public void StopMoving()
        {
            direction.X = 0;
            direction.Y = 0;
        }

        public void SetDirection(Vector2 target)
        {
            if (target != Vector2.Zero)
            {
                direction = target - myPosition;
                direction.Normalize();
            }
        }
    }
}
