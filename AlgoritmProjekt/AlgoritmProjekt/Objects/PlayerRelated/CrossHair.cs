using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility
{
    class CrossHair : Tile
    {
        Texture2D smallTex;
        float rotate;
    
        public CrossHair(Texture2D texture, Texture2D smallTex, Vector2 position, int size)
            :base(texture, position, size)
        {
            this.myTexture = texture;
            this.smallTex = smallTex;
            this.position = position;
            this.size = size;
            rotate = (float)Math.PI / 4;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, position, null, new Color(0.5f, 0.5f, 0.5f), rotate, new Vector2(size / 2, size / 2), 0.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(smallTex, position, null, Color.Red, rotate, new Vector2(smallTex.Width / 2, smallTex.Height / 2), 1, SpriteEffects.None, 0);
        }

    }
}
