using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class HighScore
    {
        Texture2D solidTile;
        Vector2 position;
        SpriteFont font;
        Color fontColor, texColor;


        public HighScore(Texture2D solidTile, Vector2 position, SpriteFont font)
        {
            this.solidTile = solidTile;
            this.position = position;
            this.font = font;
            fontColor = Color.White;
            texColor = Color.LightGreen;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, List<string> names, List<int> scores)
        {
            spriteBatch.Draw(solidTile, position, texColor);
            for (int i = 0; i < scores.Count; i++)
            {
                string text = i + 1 + ".   " + names[i] + "      " + scores[i];
                if (i < 10)
                    spriteBatch.DrawString(font, text, new Vector2(position.X + font.MeasureString("1").X, position.Y + i*font.MeasureString(text).Y), fontColor);
                else
                    break;
            }
            spriteBatch.DrawString(font, "", new Vector2(position.X, position.Y), fontColor);
        }
    }
}
