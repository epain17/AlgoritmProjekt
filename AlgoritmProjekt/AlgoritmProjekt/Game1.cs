using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Managers;
using AlgoritmProjekt.Screens;
using System.IO;

namespace AlgoritmProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum GameState
        {
            menu,
            gamePlay,
            highscore,
            enterUser,
        }
        public static GameState gameState = GameState.menu;
        public static bool EXIT = false, RELOADGAMEPLAY = false, LoadJsonLevel = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Screens
        GameManager gameManager;
        Menu menu;
        HighScore highScore;
        NewScore newScore;

        TextureManager textureManager;

        int screenWidth = Constants.screenWidth, 
            screenHeight = Constants.screenHeight;
        int tileSize = Constants.tileSize;

        static UserScore userHighScore;

        float switchScreenTimer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureManager = new TextureManager(GraphicsDevice, Content);
            
            menu = new Menu(screenWidth, screenHeight, new Vector2(screenWidth / 2, screenHeight / 2));
            newScore = new NewScore(screenWidth, screenHeight);

            userHighScore = new UserScore();
            ReadScores(Constants.scoreFilePath);
            highScore = new HighScore(new Vector2(screenWidth * 0.25f, screenHeight * 0.1f));
        }

        public static void ReadScores(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string readLine = sr.ReadLine();
            string[] strings = readLine.Split(',');
            userHighScore.AddUserScore(Int32.Parse(strings[1]), strings[0]);

            while (!sr.EndOfStream)
            {
                readLine = sr.ReadLine();
                if (readLine != "")
                {
                    strings = readLine.Split(',');
                    userHighScore.AddUserScore(Int32.Parse(strings[1]), strings[0]);
                }
            }
            userHighScore.SortScores();
            sr.Close();
        }

        static string ToString(string userName, int score)
        {
            string result;
            result = userName;
            result += ",";
            result += score.ToString() + '\n';
            return result;
        }

        public static void WriteScores(string filePath, string key, int value)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(ToString(key, value));
            writer.Close();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            if (EXIT)
                Exit();
            if (RELOADGAMEPLAY)
            {
                gameManager = new GameManager(screenWidth, screenHeight, tileSize);
                RELOADGAMEPLAY = false;
                menu.run = Menu.RunTime.Continued;
                gameState = GameState.gamePlay;
            }

            UpdateScreens(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (gameState)
            {
                case GameState.menu:
                    spriteBatch.Begin();
                    menu.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.gamePlay:
                    gameManager.Draw(spriteBatch);
                    break;
                case GameState.highscore:
                    spriteBatch.Begin();
                    menu.Draw(spriteBatch);
                    highScore.Draw(spriteBatch, userHighScore);
                    spriteBatch.End();
                    break;
                case GameState.enterUser:
                    spriteBatch.Begin();
                    newScore.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }

        private void UpdateScreens(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.menu: //MENU
                    menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.gamePlay: //GAMEPLAY
                    gameManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                    if (gameManager.GameOver() || gameManager.Winner())
                    {
                        switchScreenTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (switchScreenTimer > 4 ||
                            KeyMouseReader.KeyPressed(Keys.Escape) ||
                            KeyMouseReader.KeyPressed(Keys.Space))
                        {
                            switchScreenTimer = 0;
                            gameState = GameState.enterUser;
                            menu.run = Menu.RunTime.FirstLoad;
                        }
                    }
                    else
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        gameState = GameState.menu;

                    break;
                case GameState.highscore: //HIGHSCORES
                    menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    highScore.Update();
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        gameState = GameState.menu;
                    break;
                case GameState.enterUser: //ENTER USER
                    newScore.Update();
                    break;
            }
        }
    }
}
