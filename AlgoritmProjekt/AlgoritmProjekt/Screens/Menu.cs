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
        SpriteFont font;
        string[] text = new string[2];
        Vector2 position;
        Color color;
        int selected = 0;

        public Menu(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
            this.color = Color.White;

            text[0] = "Start";
            text[1] = "Quit";
        }

        public void Update()
        {
            if (KeyMouseReader.KeyPressed(Keys.Up) && selected < 0)
                selected++;
            else if (KeyMouseReader.KeyPressed(Keys.Down) && selected > 1)
                selected--;
            HandleSelected();
        }

        private void HandleSelected()
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == selected)
                    color = Color.YellowGreen;
                else
                    color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 2; i++)
            {
                spriteBatch.DrawString(font, text[i], new Vector2(position.X, position.Y + (i * 20)), color);
            }
        }
    }
}
