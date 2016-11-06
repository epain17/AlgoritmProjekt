using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Weapons
{
    class Weapon : ObjectTile
    {
        public bool moveMe = false;
        bool changeColor = true;
        Color fontColor = Color.DarkSlateGray, texColor = Color.DarkViolet;

        public Weapon(Texture2D texture, SpriteFont font, Vector2 position, int size) 
            :base(texture, font, position, size)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.size = size;
            this.text = "Weapon";
        }

        public virtual void Update(Vector2 camera, int screenWidth, int screenHeight)
        {
            if (moveMe)
            {
                myPosition = new Vector2(screenWidth - (mySize / 2) - camera.X, (mySize / 2) - camera.Y);
                if (changeColor)
                {
                    changeColor = false;
                    texColor = Color.Violet;
                    fontColor = Color.Gray;
                }
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, texColor, 0, origin, 1, SpriteEffects.None, 1);
            spritebatch.DrawString(font, text, position, fontColor, 0, new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2), 0.6f, SpriteEffects.None, 0);
        }
    }
}
