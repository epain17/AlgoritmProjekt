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
    class EnterUser
    {
        enum Navigate
        {
            ChangingLetter,
            NavigateButtons,
        }
        Navigate navigate = Navigate.NavigateButtons;

        enum LetterSlot
        {
            first,
            second,
            third,
        }
        LetterSlot letterSlot = LetterSlot.first;

        List<string> navigatedButtons = new List<string>();

        Vector2 buttonPos;

        SpriteFont font;
        Texture2D texture;
        int screenWidth, screenHeight;

        Vector2 fontOrigin, texOrigin, screenCenter;
        string title;

        int selected = 0, letterSlotIndex = -1, firstLetterIndex, secondLetterIndex, thirdLetterIndex;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] letterSlots = new char[3];
        string userName;

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

            navigatedButtons.Add("Enter");
            navigatedButtons.Add("Exit");
            buttonPos = new Vector2(screenCenter.X - texOrigin.X / 2, screenCenter.Y + texOrigin.Y - font.MeasureString(navigatedButtons[0]).Y);
            for (int i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i] = alphabet[0];
            }
        }

        public void Update()
        {
            Console.WriteLine(userName);
            UpdateInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, screenCenter, null, Color.LimeGreen, 0, texOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, title, new Vector2(screenCenter.X, screenCenter.Y - texture.Height / 2 + font.MeasureString(title).Y), Color.LightGray, 0, fontOrigin, 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, Constants.totalScore.ToString(), new Vector2(screenCenter.X, screenCenter.Y - texture.Height / 2 + font.MeasureString(Constants.totalScore.ToString()).Y * 2), Color.LightGray, 0, new Vector2(font.MeasureString(Constants.totalScore.ToString()).X, 0), 2f, SpriteEffects.None, 0);

            Color color;
            for (int i = 0; i < letterSlots.Length; i++)
            {
                color = (i == letterSlotIndex) ? Color.White : Color.DarkSlateGray;
                spriteBatch.DrawString(font, letterSlots[i].ToString(), new Vector2(buttonPos.X + texOrigin.X / 4 + (i * texOrigin.X / 2), buttonPos.Y - texture.Height / 2), color, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < navigatedButtons.Count; i++)
            {
                color = (i == selected) ? Color.White : Color.DarkSlateGray;
                spriteBatch.DrawString(font, navigatedButtons[i], new Vector2(buttonPos.X + (i * texOrigin.X * 1.5f), buttonPos.Y), color, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }
        }

        void UpdateInput()
        {
            switch (navigate)
            {
                case Navigate.NavigateButtons:
                    if (KeyMouseReader.KeyPressed(Keys.Enter) && navigatedButtons[selected] == "Enter")
                    {
                        letterSlotIndex = 0;
                        userName = null;
                        navigate = Navigate.ChangingLetter;
                        letterSlot = LetterSlot.first;
                    }
                    else if (KeyMouseReader.KeyPressed(Keys.Enter) && navigatedButtons[selected] == "Exit")
                        Game1.gameState = Game1.GameState.menu;

                    if (KeyMouseReader.KeyPressed(Keys.D) && selected < 1)
                    {
                        selected++;
                    }
                    else if (KeyMouseReader.KeyPressed(Keys.A) && selected > 0)
                    {
                        selected--;
                    }
                    break;
                case Navigate.ChangingLetter:
                    switch (letterSlot)
                    {
                        case LetterSlot.first:
                            ChangeLetter(ref firstLetterIndex, alphabet.Length - 1);
                            letterSlots[letterSlotIndex] = alphabet[firstLetterIndex];

                            AddToString(firstLetterIndex);
                            break;
                        case LetterSlot.second:
                            ChangeLetter(ref secondLetterIndex, alphabet.Length - 1);
                            letterSlots[letterSlotIndex] = alphabet[secondLetterIndex];

                            AddToString(secondLetterIndex);
                            break;
                        case LetterSlot.third:
                            ChangeLetter(ref thirdLetterIndex, alphabet.Length - 1);
                            letterSlots[letterSlotIndex] = alphabet[thirdLetterIndex];

                            AddToString(thirdLetterIndex);

                            break;
                    }
                    break;
            }
        }

        private void AddToString(int alphabetInt)
        {
            if (KeyMouseReader.KeyPressed(Keys.Enter))
            {
                userName += alphabet[alphabetInt].ToString();
                letterSlotIndex++;
            }

            if (letterSlotIndex == 3)
            {
                Game1.WriteScores(Constants.filePath, userName, Constants.totalScore);
                Game1.gameState = Game1.GameState.menu;
            }
        }

        private void ChangeLetter(ref int alphabetInt, int alphabetLength)
        {
            if (KeyMouseReader.KeyPressed(Keys.W) && alphabetInt < alphabetLength)
            {
                alphabetInt++;
            }
            else if (KeyMouseReader.KeyPressed(Keys.W) && alphabetInt == alphabetLength)
                alphabetInt = 0;
            if (KeyMouseReader.KeyPressed(Keys.S) && alphabetInt > 0)
            {
                alphabetInt--;
            }
            else if (KeyMouseReader.KeyPressed(Keys.S) && alphabetInt == 0)
                alphabetInt = alphabetLength;
        }
    }
}
