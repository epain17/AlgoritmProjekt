using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        string[] text = new string[3];
        Vector2 position;
        Color color;
        int selected = 0;

        public Menu(SpriteFont font)
        {
            this.font = font;
            this.color = Color.White;
        }

        public void Update()
        {
            HandleSelected();
        }

        private void HandleSelected()
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (i == selected)
                    color = Color.YellowGreen;
                else
                    color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < text.Length; i++)
            {
                spriteBatch.DrawString(font, text[i], new Vector2(position.X, position.Y + (i * 20)), color);
            }
        }
    }
}
