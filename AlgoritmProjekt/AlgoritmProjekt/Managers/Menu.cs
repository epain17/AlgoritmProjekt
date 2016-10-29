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
        List<Emitter> emitters = new List<Emitter>();
        GameWindow window;
        Texture2D texture;
        Random rand;

        float timer;

        public Menu(GameWindow window, SpriteFont font, Vector2 position, Texture2D texture)
        {
            this.window = window;
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.color = Color.White;
            buttons.Add("Play");
            buttons.Add("High Score");
            buttons.Add("Quit");
            rand = new Random();

        }

        public void Update(float time)
        {
            timer += time;
            if (timer > 0.3f)
            {
                timer = 0;
                emitters.Add(new Emitter(texture, new Vector2((float)rand.Next(0, window.ClientBounds.Width), 0), 2));
                emitters.Add(new Emitter(texture, new Vector2((float)rand.Next(0, window.ClientBounds.Width), 0), 2));

            }
            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                emitters[i].Update(time);
                if (!emitters[i].IsAlive)
                    emitters.RemoveAt(i);
            }
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
            for (int i = 0; i < emitters.Count; i++)
            {
                emitters[i].Draw(spriteBatch);
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                color = (i == selected) ? Color.LightGray : Color.DarkSlateGray;
                spriteBatch.DrawString(font, buttons[i], new Vector2(position.X - (font.MeasureString(buttons[i]).X / 2), position.Y + (i * 20)), color);
            }
        }
    }
}
