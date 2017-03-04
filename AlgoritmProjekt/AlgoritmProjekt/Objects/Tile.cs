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
        protected Texture2D myTexture;
        protected Vector2 position, startPos, velocity;
        protected Color fontColor = Color.DarkSlateGray, texColor = Color.Blue;
        protected Rectangle hitBox;
        protected int size;
        protected bool occupied = false;
        protected Vector2 origin;

        public bool LetMeShoot
        {
            get;
            set;
        }

        public Vector2 myStartPos
        {
            get { return startPos; }
        }

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

        public virtual bool CheckMyIntersect(Tile target)
        {
            if (myHitBox.Intersects(target.myHitBox))
            {
                return true;
            }
            return false;
        }

        public virtual bool CheckMyVectorCollision(Vector2 target)
        {
            return myHitBox.Contains(target);
        }

        public Tile(Texture2D texture, Vector2 position, int size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.origin = new Vector2(size / 2, size / 2);
            this.velocity = Vector2.Zero;
            this.startPos = position;
        }

        public virtual void Update(ref float time)
        {
            
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, new Color(0f, 0.1f, 0f), 0, origin, 1, SpriteEffects.None, 1);
        }
    }
}
