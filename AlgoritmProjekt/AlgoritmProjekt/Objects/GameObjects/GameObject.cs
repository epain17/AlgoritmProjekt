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
        protected Vector2 origin;
        protected int mySize;
        protected Color myColor = Color.LimeGreen;
        public float colorAlpha = 1;

        public Rectangle myHitBox()
        {
            return new Rectangle((int)myPosition.X, (int)myPosition.Y, mySize, mySize);
        }

        public GameObject(Vector2 position, int size)
        {
            myPosition = position;
            mySize = size;
            origin = new Vector2(size / 2, size / 2);
        }

        public virtual void Update(float time)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, myHitBox(), null, myColor * colorAlpha, 0, origin, SpriteEffects.None, 0);
        }

        
    }
}
