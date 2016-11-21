using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Tiles
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
        protected string name;
        protected int size;
        protected bool occupied = false;
        protected Vector2 origin;
        protected SpriteFont font;
        protected Color color;

        public string myName
        {
            get { return name; }
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
            get { return new Rectangle((int)position.X - (size / 2), (int)position.Y - (size / 2), size, size); }
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

        public Tile(Texture2D texture, Vector2 position, int size, SpriteFont font)
        {
            this.size = size;
            this.texture = texture;
            this.position = new Vector2(position.X + (size / 2), position.Y + (size / 2));
            this.origin = new Vector2(size / 2, size / 2);
            this.name = " ";
            this.font = font;
            color = Color.DarkBlue;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            if (!iamOccupied)
                spritebatch.DrawString(font, name, position, Color.White, 0, new Vector2(font.MeasureString(name).X / 2, font.MeasureString(name).Y / 2), 1, SpriteEffects.None, 0);
        }

        public bool Hovering(Vector2 mouse)
        {
            if (myHitBox.Contains(mouse))
            {
                if (!iamOccupied)
                    color = Color.White;
                return true;
            }
            else
            {
                if (!iamOccupied)
                    color = Color.DarkBlue;
                return false;
            }
        }

        public bool Clicked()
        {
            if (KeyMouseReader.LeftClick() && !iamOccupied)
            {
                color = Color.Red;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
