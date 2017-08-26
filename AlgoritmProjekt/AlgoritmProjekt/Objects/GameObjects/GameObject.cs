using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.GameObjects
{
    class GameObject
    {
        public Texture2D myTexture = TextureManager.solidRect;
        public Vector2 myPosition;
        public float colorAlpha = 1;
        public Tile myCurrentTile, myPreviousTile;

        protected Vector2 origin;
        protected int
            myWidth,
            myHeight;
        protected Color myColor = Color.LimeGreen;
        protected bool isBlockable = false;

        public Rectangle myHitBox()
        {
            return new Rectangle((int)myPosition.X, (int)myPosition.Y, myWidth, myHeight);
        }

        public Point myWorldPoint
        {
            get { return new Point((int)myPosition.X / myWidth, (int)myPosition.Y / myHeight); }
        }

        public GameObject(Vector2 position, int width, int height)
        {
            myPosition = position;
            myHeight = height;
            myWidth = width;
            origin = new Vector2(width / 2, width / 2);
        }

        public virtual void Update(float time, TileGrid grid)
        {
            // NOT EFFICIENT 
            if (isBlockable)
            {
                if (myCurrentTile != null)
                {
                    myPreviousTile = myCurrentTile;
                    myPreviousTile.UnblockMe();
                }

                myCurrentTile = grid.ReturnTile(myPosition);
                myCurrentTile.BlockMe();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, myHitBox(), null, myColor * colorAlpha, 0, origin, SpriteEffects.None, 0);
            if (isBlockable)
                myCurrentTile.Draw(spriteBatch);
        }


    }
}
