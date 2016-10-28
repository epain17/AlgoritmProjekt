using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
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
        Emitter emitter;
        GameWindow window;
        Random rand;
        public Menu(GameWindow window, SpriteFont font, Vector2 position, Texture2D texture)
        {
            this.window = window;
            this.font = font;
            this.position = position;
            this.color = Color.White;
            emitter = new Emitter(texture, Vector2.Zero, 2);
            buttons.Add("Play");
            buttons.Add("High Score");
            buttons.Add("Quit");
            rand = new Random();

        }

        public void Update(float time)
        {
            emitter.myPosition = new Vector2((float)rand.Next(0, window.ClientBounds.Width), 0);

            emitter.Update(time);
            
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
            emitter.Draw(spriteBatch);
            for (int i = 0; i < buttons.Count; i++)
            {
                color = (i == selected) ? Color.LimeGreen : Color.Blue;
                spriteBatch.DrawString(font, buttons[i], new Vector2(position.X - (font.MeasureString(buttons[i]).X / 2), position.Y + (i * 20)), color);
            }
        }
    }
}
