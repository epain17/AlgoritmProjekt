using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt
{
    public class JsonObject
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
    class Tile
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle hitBox;
        protected int size;
        protected bool occupied = false;
        protected Vector2 origin;
        protected int hp;
        protected bool alive = true;

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle myHitBox
        {
            get { return hitBox = new Rectangle((int)position.X, (int)position.Y, size, size); }
        }

        public virtual bool iamOccupied
        {
            get { return occupied; }
            set { occupied = value; }
        }

        public Point myPoint
        {
            get { return new Point((int)position.X / size, (int)position.Y / size); }
        }

        public int myHP
        {
            get { return hp; }
            set { hp = value; }
        }

        public bool iamAlive
        {
            get { return alive; }
        }

        public virtual bool CheckMyCollision(Tile target)
        {
            if (Vector2.Distance(myPosition, target.myPosition) < size)
            {
                return true;
            }
            return false;
        }

        public Tile(Texture2D texture, Vector2 position, int size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.origin = new Vector2(size / 2, size / 2);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, new Color(0f, 0.1f, 0f), 0, origin, 1, SpriteEffects.None, 1);
        }

        public void HandelCollision(Wall w, int n)
        {
            //Top
            if (n == 1)
            {
                position.Y = position.Y + 4;
            }

            //Bottom             
            if (n == 2)
            {
                position.Y = position.Y - 4;
            }

            // Left            
            if (n == 3)
            {
                position.X = position.X - 4;
            }

            // Right            
            if (n == 4)
            {
                position.X = position.X + 4;
            }

        }

        public virtual int Collision(Wall w)
        {
            Rectangle top = w.myHitBox;
            top.Height = 10;

            Rectangle bottom = w.myHitBox;
            bottom.Height = 5;
            bottom.Y = bottom.Y + w.myHitBox.Height - 5;

            Rectangle left = w.myHitBox;
            left.Width = 2;
            left.Y = w.myHitBox.Y + 10;
            left.Height = w.myHitBox.Height - 20;

            Rectangle right = w.myHitBox;
            right.X = right.X + right.Width - 2;
            right.Width = 2;
            right.Y = w.myHitBox.Y + 10;
            right.Height = w.myHitBox.Height - 20;



            if (myHitBox.Intersects(left))
            {
                return 3;
            }

            else if (myHitBox.Intersects(right))
            {
                return 4;
            }
            if (myHitBox.Intersects(top))
            {
                return 1;
            }

            if (myHitBox.Intersects(bottom))
            {
                return 2;
            }
            return 0;
        }
    }
}
