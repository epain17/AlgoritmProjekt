using AlgoritmProjekt.Input;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class HighScore
    {
        Vector2 position;
        Color fontColor, texColor;

        public HighScore(Vector2 position)
        {
            this.position = position;
            fontColor = Color.White;
            texColor = Color.DarkGreen;
        }

        public void Update()
        {
            if (KeyMouseReader.KeyPressed(Keys.Escape) ||
                KeyMouseReader.KeyPressed(Keys.Space) ||
                KeyMouseReader.KeyPressed(Keys.Enter))
                Game1.gameState = Game1.GameState.menu;
        }

        public void Draw(SpriteBatch spriteBatch, UserScore highscore)
        {
            spriteBatch.Draw(TextureManager.solidRect, position, texColor);

            for (int i = 0; i < highscore.HighScore.Count; i++)
            {

                string text = i + 1 + ".                " + highscore.HighScore[i].name + "                    " + highscore.HighScore[i].score;
                if (i < 10)
                    spriteBatch.DrawString(TextureManager.defaultFont, text, new Vector2(position.X + TextureManager.defaultFont.MeasureString("1").X, position.Y + i * TextureManager.defaultFont.MeasureString(text).Y), fontColor);
                else
                    break;
            }
            spriteBatch.DrawString(TextureManager.defaultFont, "High Score", new Vector2(position.X + (TextureManager.solidRect.Width / 2), position.Y - 50), Color.DarkSeaGreen, 0, new Vector2((TextureManager.defaultFont.MeasureString("HighScores").X / 2), 0), 1.5f, SpriteEffects.None, 0);
        }
    }
}
