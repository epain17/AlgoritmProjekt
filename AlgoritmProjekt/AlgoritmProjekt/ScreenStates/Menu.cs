using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Managers.ParticleEngine.Emitters;
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
        public enum RunTime
        {
            FirstLoad,
            Continued,
        }
        public RunTime run = RunTime.FirstLoad;
        List<string> buttons = new List<string>();
        SpriteFont font;
        Vector2 position;
        Color color;
        int selected = 1, screenWidth, screenHeight;
        List<Emitter> emitters = new List<Emitter>();
        Texture2D texture;
        Random rand;

        float timer;
        Rectangle frame;

        public Menu(int screenWidth, int screenHeight, SpriteFont font, Vector2 position, Texture2D texture)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.color = Color.White;
            buttons.Add("Continue");
            buttons.Add("New Game");
            buttons.Add("High Score");
            buttons.Add("Quit");
            rand = new Random();
            frame = new Rectangle((int)position.X - 100, (int)position.Y - 50, 200, 200);
        }

        public void Update(float time)
        {
            HandleEmitters(time);
            if (Game1.gameState == Game1.GameState.menu)
            {
                HandleSelect();
                ExecuteSelectedButton();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Emitter emitter in emitters)
            {
                emitter.Draw(spriteBatch);
            }

            if (Game1.gameState == Game1.GameState.menu)
            {
                spriteBatch.Draw(texture, frame, Color.Black);
                spriteBatch.Draw(texture, frame, Color.Black);

                switch (run)
                {
                    case RunTime.FirstLoad:
                        for (int i = 1; i < buttons.Count; i++)
                        {
                            color = (i == selected) ? Color.White : Color.DarkSlateGray;
                            spriteBatch.DrawString(font, buttons[i], new Vector2(position.X - (font.MeasureString(buttons[i]).X / 2), position.Y + (i * 20)), color);
                        }
                        break;
                    case RunTime.Continued:
                        for (int i = 0; i < buttons.Count; i++)
                        {
                            color = (i == selected) ? Color.White : Color.DarkSlateGray;
                            spriteBatch.DrawString(font, buttons[i], new Vector2(position.X - (font.MeasureString(buttons[i]).X / 2), position.Y + (i * 20)), color);
                        }
                        break;
                }
                spriteBatch.Draw(texture, new Rectangle(screenWidth / 2 - (int)font.MeasureString("Enter The Cubes").X * 2, (int)(screenHeight * 0.15f) + (int)font.MeasureString("Enter The Cubes").Y / 4, (int)font.MeasureString("Enter The Cubes").X * 4, (int)font.MeasureString("Enter The Cubes").Y * 4), Color.Black);
                spriteBatch.DrawString(font, "Enter The Cubes", new Vector2(screenWidth / 2, screenHeight * 0.15f), Color.LimeGreen, 0, new Vector2(font.MeasureString("Enter The Cubes").X / 2, 0), 2.5f, SpriteEffects.None, 0);
            }
        }

        private void HandleSelect()
        {
            switch (run)
            {
                case RunTime.FirstLoad:
                    if(selected == 0)
                        selected = 1;
                    if (KeyMouseReader.KeyPressed(Keys.W) && selected > 1)
                        selected--;
                    else if (KeyMouseReader.KeyPressed(Keys.W) && selected == 1)
                        selected = buttons.Count - 1;
                    if (KeyMouseReader.KeyPressed(Keys.S) && selected < buttons.Count - 1)
                        selected++;
                    else if (KeyMouseReader.KeyPressed(Keys.S) && selected == buttons.Count - 1)
                        selected = 1;
                    break;
                case RunTime.Continued:
                    if (KeyMouseReader.KeyPressed(Keys.W) && selected > 0)
                        selected--;
                    else if (KeyMouseReader.KeyPressed(Keys.W) && selected == 0)
                        selected = buttons.Count - 1;
                    if (KeyMouseReader.KeyPressed(Keys.S) && selected < buttons.Count - 1)
                        selected++;
                    else if (KeyMouseReader.KeyPressed(Keys.S) && selected == buttons.Count - 1)
                        selected = 0;
                    break;
            }
        }

        private void ExecuteSelectedButton()
        {
            if (KeyMouseReader.KeyPressed(Keys.Enter) || KeyMouseReader.KeyPressed(Keys.Space))
            {
                switch (selected)
                {
                    case 0:
                        //continue - no changes
                        Game1.gameState = Game1.GameState.gamePlay;
                        break;
                    case 1:
                        //new game
                        Game1.RELOADGAMEPLAY = true;
                        Game1.LoadJsonLevel = true;
                        selected = 0;
                        break;
                    case 2:
                        //highscore
                        Game1.gameState = Game1.GameState.highscore;
                        if (run == RunTime.FirstLoad)
                            selected = 1;
                        else
                            selected = 0;

                        break;
                    case 3:
                        Game1.EXIT = true;
                        break;
                }
            }
        }

        private void HandleEmitters(float time)
        {
            timer += time;
            if (timer > 0.1f)
            {
                timer = 0;
                emitters.Add(new MatrixEmitter(font, new Vector2((float)rand.Next(0, screenWidth), 0)));
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Update(ref time);
            }

            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                if (!emitters[i].IsAlive)
                    emitters.RemoveAt(i);
            }
        }
    }
}
