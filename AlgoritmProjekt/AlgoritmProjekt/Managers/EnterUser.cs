using AlgoritmProjekt.Input;
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
    class EnterUser
    {
        List<string> buttons = new List<string>();
        Vector2 buttonPos;

        SpriteFont font;
        Texture2D texture;
        int screenWidth, screenHeight;

        Vector2 fontOrigin, texOrigin, screenCenter;
        Rectangle inputFrame;
        string title;

        string input;

        public EnterUser(SpriteFont font, Texture2D texture, int screenWidth, int screenHeight)
        {
            this.font = font;
            this.texture = texture;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            title = "Save Your Score";
            texOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            fontOrigin = new Vector2(font.MeasureString(title).X / 2, font.MeasureString(title).Y / 2);
            screenCenter = new Vector2(screenWidth / 2, screenHeight / 2);
            buttons.Add("Enter");
            buttons.Add("Exit");
            buttonPos = new Vector2(screenCenter.X - texOrigin.X / 2, screenCenter.Y + texOrigin.Y - font.MeasureString(buttons[0]).Y);
        }

        public void Update()
        {
            UpdateInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, screenCenter, null, Color.LimeGreen, 0, texOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, title, screenCenter, Color.LightGray, 0, fontOrigin, 1, SpriteEffects.None, 0);
            if (input != null)
                spriteBatch.DrawString(font, input, screenCenter, Color.DarkSlateGray);

            for (int i = 0; i < buttons.Count; i++)
            {
                spriteBatch.DrawString(font, buttons[i], new Vector2(buttonPos.X + (i * texOrigin.X * 1.5f), buttonPos.Y), Color.White, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }
        }

        void UpdateInput()
        {
            Keys[] pressedKeys;
            pressedKeys = KeyMouseReader.keyState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (KeyMouseReader.oldKeyState.IsKeyUp(key))
                {
                    if (key == Keys.Back)
                    {
                        input = input.Remove(input.Length - 1, 1);
                    }
                    else if (key == Keys.Space)
                    {
                        input = input.Insert(input.Length, " ");
                    }
                    else
                        input += key.ToString();
                }
            }
        }
    }
}
