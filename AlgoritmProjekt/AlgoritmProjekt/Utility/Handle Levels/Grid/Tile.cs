using AlgoritmProjekt.Objects.GameObjects;
using AlgoritmProjekt.Utility;
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
        public enum TileType
        {
            DEFAULT,
            START,
            FINISH,
            WALL,
            FLOOR,
            DOOR,
        }
        public TileType myType = TileType.DEFAULT;

        public bool iamOccupied = false;
        public Vector2 myPosition;
        public int myWidth, myHeight;
        protected float halfWidth, halfHeight;
        protected Texture2D myTexture;
        protected Color texColor, neutralColor;

        public Tile NorthNeighbour, EastNeighbour, SouthNeighbour, WestNeighbour;

        public Vector2 MyCenter()
        {
            return new Vector2(myPosition.X + halfWidth, myPosition.Y + halfHeight);
        }

        public Rectangle myHitBox
        {
            get { return new Rectangle((int)myPosition.X, (int)myPosition.Y, myWidth, myHeight); }
        }

        public bool amIOccupied(GameObject target)
        {
            if (myHitBox.Contains(target.myPosition))
                return true;
            return false;
        }

        public bool amIOccupied(Tile tile)
        {
            if (myHitBox.Contains(tile.MyCenter()))
                return true;
            return false;
        }

        public Point myPoint
        {
            get { return new Point((int)myPosition.X / myWidth, (int)myPosition.Y / myHeight); }
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

        public Tile(Vector2 position, int width, int height)
        {
            this.myTexture = TextureManager.solidRect;
            this.myPosition = position;
            this.myWidth = width;
            this.myHeight = height;
            halfWidth = width / 2;
            halfHeight = height / 2;
            neutralColor = new Color(0f, 0.2f, 0f);
            texColor = neutralColor;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            switch (myType)
            {
                case (TileType.DEFAULT):
                    spritebatch.Draw(myTexture, myHitBox, texColor);
                    break;
                case TileType.START:
                    spritebatch.Draw(myTexture, myHitBox, Color.Gold);
                    break;
                case TileType.WALL:
                    spritebatch.Draw(myTexture, myHitBox, Color.SaddleBrown);
                    break;
                case TileType.FLOOR:
                    spritebatch.Draw(myTexture, myHitBox, Color.Gray);
                    break;
                case TileType.FINISH:
                    spritebatch.Draw(myTexture, myHitBox, Color.Blue * 0.25f);
                    break;


            }
        }

        public virtual void BlockMe()
        {
            iamOccupied = true;
            texColor = Color.Red;
        }

        public virtual void UnblockMe()
        {
            iamOccupied = false;
            texColor = neutralColor;
        }

        public void SetNorthNeighbour(Tile tile)
        {
            NorthNeighbour = tile;
        }

        public void SetEastNeighbour(Tile tile)
        {
            EastNeighbour = tile;
        }

        public void SetSouthNeighbour(Tile tile)
        {
            SouthNeighbour = tile;
        }

        public void SetWestNeighbour(Tile tile)
        {
            WestNeighbour = tile;
        }

    }
}
