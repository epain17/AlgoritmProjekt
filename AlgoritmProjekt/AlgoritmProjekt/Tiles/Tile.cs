using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt
{
    class Tile
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle hitBox;
        protected int size;
        protected bool occupied = false;

        public Tile(Texture2D texture, Vector2 position, int size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        public Rectangle HitBox
        {
            get { return hitBox = new Rectangle((int)position.X, (int)position.Y, size, size); }
        }

        public virtual bool Occupied
        {
            get { return occupied; }
            set { occupied = value; }
        }

        public int PositionX
        {
            get { return (int)position.X; }
        }

        public int PositionY
        {
            get { return (int)position.Y; }
        }

        public virtual Point TilePoint
        {
            get { return new Point((int)position.X, (int)position.Y); }
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, new Color(0.1f, 0.1f, 0.1f, 0.1f), 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);
        }
    }
}
