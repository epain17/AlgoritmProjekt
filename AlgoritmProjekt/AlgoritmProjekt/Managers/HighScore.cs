using AlgoritmProjekt.Input;
using AlgoritmProjekt.Utility.Algorithms;
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
        Texture2D solidTile;
        Vector2 position;
        SpriteFont font;
        Color fontColor, texColor;
        HashTable hashTable;

        public HighScore(Texture2D solidTile, Vector2 position, SpriteFont font, HashTable hashTable)
        {
            this.solidTile = solidTile;
            this.position = position;
            this.font = font;
            this.hashTable = hashTable;
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

        public void Draw(SpriteBatch spriteBatch, List<string> key)
        {
            spriteBatch.Draw(solidTile, position, texColor);
            
            for (int i = 0; i < key.Count; i++)
            {

                string text = i + 1 + ".                " + key[i] + "                    " + hashTable.GetValue(key[i]);
                if (i < 10)
                    spriteBatch.DrawString(font, text, new Vector2(position.X + font.MeasureString("1").X, position.Y + i * font.MeasureString(text).Y), fontColor);
                else
                    break;
            }
            spriteBatch.DrawString(font, "High Score", new Vector2(position.X + (solidTile.Width / 2), position.Y - 50), Color.DarkSeaGreen, 0, new Vector2((font.MeasureString("HighScores").X / 2), 0), 1.5f, SpriteEffects.None, 0);
        }
    }
}
