using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Screens
{
    class Menu
    {
        List<string> buttons = new List<string>();
        SpriteFont font;
        Vector2 position;
        Color color;
        int selected = 0;

        public Menu(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
            this.color = Color.White;
            buttons.Add("Play");
            buttons.Add("High Score");
            buttons.Add("Quit");
        }

        public void Update()
        {
            if (KeyMouseReader.KeyPressed(Keys.Up) && selected > 0)
                selected--;
            else if (KeyMouseReader.KeyPressed(Keys.Down) && selected < buttons.Count - 1)
                selected++;

            if (KeyMouseReader.KeyPressed(Keys.Enter) || KeyMouseReader.KeyPressed(Keys.Space))
            {
                switch (selected)
                {
                    case 0:
                        Game1.RELOAD = true;
                        Game1.LoadJsonLevel = true;
                        Game1.gameState = Game1.GameState.gamePlay;
                        break;
                    case 1:

                        break;
                    case 2:
                        Game1.EXIT = true;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                color = (i == selected) ? Color.LimeGreen : Color.Blue;
                spriteBatch.DrawString(font, buttons[i], new Vector2(position.X - (font.MeasureString(buttons[i]).X / 2), position.Y + (i * 20)), color);
            }
        }
    }
}
