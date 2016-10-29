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
        protected Vector2 position, velocity;
        protected Rectangle hitBox;
        protected int size;
        protected bool occupied = false;
        protected Vector2 origin;
        protected int hp;
        protected bool alive = true;

        public int mySize
        {
            get { return size; }
        }

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle myHitBox
        {
            get { return hitBox = new Rectangle((int)position.X - (size / 2), (int)position.Y - (size / 2), size, size); }
        }

        public virtual bool iamOccupied
        {
            get { return occupied; }
            set { occupied = value; }
        }

        public bool amIOccupied(Tile target)
        {
            if (myHitBox.Contains(target.myPosition))
                return true;
            return false;
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
            if (myHitBox.Intersects(target.myHitBox))
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
            this.velocity = Vector2.Zero;
        }

        public virtual void Update()
        {
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, new Color(0f, 0.1f, 0f), 0, origin, 1, SpriteEffects.None, 1);
        }

        public virtual void SolveCollision(Tile target)
        {
            if (velocity.X > 0)
                position.X = target.myHitBox.Left - (size / 2);
            else if (velocity.X < 0)
                position.X = target.myHitBox.Right + (size / 2);

            else if (velocity.Y > 0)
                position.Y = target.myHitBox.Top - (size / 2);
            else if (velocity.Y < 0)
                position.Y = target.myHitBox.Bottom + (size / 2);

            velocity = Vector2.Zero;
        }


    }
}
