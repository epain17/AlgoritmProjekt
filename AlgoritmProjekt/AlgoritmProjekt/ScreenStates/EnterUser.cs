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
    class NewScore
    {
        public enum Navigate
        {
            ChangingLetter,
            NavigateButtons,
        }
        public Navigate navigate = Navigate.NavigateButtons;

        List<string> navigatedButtons = new List<string>();
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] letterSlots = new char[3];
        int selected, letterSlotIndex, alphabetIndex;
        string userName;

        Vector2 buttonPos;

        Vector2 fontOrigin, texOrigin, screenCenter;
        int screenWidth, screenHeight;

        string title;

        public NewScore(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            title = "Save Your Score";
            texOrigin = new Vector2(TextureManager.solidRect.Width / 2, TextureManager.solidRect.Height / 2);
            fontOrigin = new Vector2(TextureManager.defaultFont.MeasureString(title).X / 2, TextureManager.defaultFont.MeasureString(title).Y / 2);
            screenCenter = new Vector2(screenWidth / 2, screenHeight / 2);

            navigatedButtons.Add("Enter");
            navigatedButtons.Add("Exit");
            buttonPos = new Vector2(screenCenter.X - texOrigin.X / 2, screenCenter.Y + texOrigin.Y - TextureManager.defaultFont.MeasureString(navigatedButtons[0]).Y);
            InitialState();
        }

        public void Update()
        {
            UpdateInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.solidRect, screenCenter, null, Color.LimeGreen, 0, texOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.defaultFont, title, new Vector2(screenCenter.X, screenCenter.Y - TextureManager.solidRect.Height / 2 + TextureManager.defaultFont.MeasureString(title).Y), Color.LightGray, 0, fontOrigin, 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.defaultFont, Constants.totalScore.ToString(), new Vector2(screenCenter.X, screenCenter.Y - TextureManager.solidRect.Height / 2 + TextureManager.defaultFont.MeasureString(Constants.totalScore.ToString()).Y * 2), Color.LightGray, 0, new Vector2(TextureManager.defaultFont.MeasureString(Constants.totalScore.ToString()).X, 0), 2f, SpriteEffects.None, 0);

            Color color;
            for (int i = 0; i < letterSlots.Length; i++)
            {
                color = (i == letterSlotIndex) ? Color.White : Color.DarkSlateGray;
                spriteBatch.DrawString(TextureManager.defaultFont, letterSlots[i].ToString(), new Vector2(buttonPos.X + texOrigin.X / 4 + (i * texOrigin.X / 2), buttonPos.Y - TextureManager.solidRect.Height / 2), color, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < navigatedButtons.Count; i++)
            {
                color = (i == selected) ? Color.White : Color.DarkSlateGray;
                spriteBatch.DrawString(TextureManager.defaultFont, navigatedButtons[i], new Vector2(buttonPos.X + (i * texOrigin.X * 1.5f), buttonPos.Y), color, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }
        }

        void InitialState()
        {
            for (int i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i] = alphabet[0];
            }
            selected = 0;
            letterSlotIndex = -1;
            alphabetIndex = 0;
            userName = null;
            navigate = Navigate.NavigateButtons;
        }

        void UpdateInput()
        {
            Console.WriteLine(userName);

            switch (navigate)
            {
                case Navigate.NavigateButtons:
                    ExecuteSelectedButton();
                    SwitchButton();
                    break;
                case Navigate.ChangingLetter:
                    letterSlots[letterSlotIndex] = alphabet[alphabetIndex];
                    ChangeLetter(ref alphabetIndex, alphabet.Length - 1);
                    AddToString();
                    RemoveFromString();
                    break;
            }
        }

        private void ExecuteSelectedButton()
        {
            if (KeyMouseReader.KeyPressed(Keys.Enter) && navigatedButtons[selected] == "Enter")
            {
                letterSlotIndex = 0;
                userName = null;
                navigate = Navigate.ChangingLetter;
            }
            else if (KeyMouseReader.KeyPressed(Keys.Enter) && navigatedButtons[selected] == "Exit")
                Game1.gameState = Game1.GameState.menu;
        }

        private void SwitchButton()
        {
            if (KeyMouseReader.KeyPressed(Keys.D) && selected < 1)
            {
                selected++;
            }
            else if (KeyMouseReader.KeyPressed(Keys.A) && selected > 0)
            {
                selected--;
            }
        }

        private void AddToString()
        {
            if (KeyMouseReader.KeyPressed(Keys.Enter))
            {
                userName += alphabet[alphabetIndex].ToString();
                letterSlotIndex++;
                alphabetIndex = 0;
            }

            if (letterSlotIndex == 3)
            {
                Game1.WriteScores(Constants.scoreFilePath, userName, Constants.totalScore);
                Game1.ReadScores(Constants.scoreFilePath);
                Game1.gameState = Game1.GameState.menu;
                InitialState();
            }
        }

        private void RemoveFromString()
        {
            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                InitialState();
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
