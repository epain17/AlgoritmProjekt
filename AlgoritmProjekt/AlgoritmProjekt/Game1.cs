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
        HighScoreScreen scoreScreen;
        EnterUser newUser;

        SpriteFont font;
        Texture2D smoothTex;
        int screenWidth = Constants.screenWidth, screenHeight = Constants.screenHeight;
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
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            smoothTex = Content.Load<Texture2D>("circle");
            gameManager = new GameManager(screenWidth, screenHeight, tileSize, font, createSolidRectangle(tileSize, tileSize, GraphicsDevice),
                createHollowRectangle(tileSize, tileSize, GraphicsDevice), createHollowRectangle(3, 3, GraphicsDevice), smoothTex);
            menu = new Menu(screenWidth, screenHeight, font, new Vector2(screenWidth / 2, screenHeight / 2), smoothTex);
            newUser = new EnterUser(font, createSolidRectangle((int)(screenWidth * 0.5f), (int)(screenHeight * 0.5f), GraphicsDevice), screenWidth, screenHeight);

            userHighScore = new UserScore();
            ReadScores(Constants.scoreFilePath);
            scoreScreen = new HighScoreScreen(createSolidRectangle((int)(screenWidth * 0.5f), (int)(screenHeight * 0.75f), GraphicsDevice), new Vector2(screenWidth * 0.25f, screenHeight * 0.1f), font);
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
                gameManager = new GameManager(screenWidth, screenHeight, tileSize, font, createSolidRectangle(tileSize, tileSize, GraphicsDevice),
                                createHollowRectangle(tileSize, tileSize, GraphicsDevice), createHollowRectangle(3, 3, GraphicsDevice), smoothTex);
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
                    scoreScreen.Draw(spriteBatch, userHighScore);
                    spriteBatch.End();
                    break;
                case GameState.enterUser:
                    spriteBatch.Begin();
                    newUser.Draw(spriteBatch);
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
                    gameManager.Update(gameTime);

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
                    scoreScreen.Update();
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        gameState = GameState.menu;
                    break;
                case GameState.enterUser: //ENTER USER
                    newUser.Update();
                    break;
            }
        }

        Texture2D createHollowRectangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];

            // Colour the entire texture transparent first.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }
            for (int i = 0; i < width; i++)
            {
                data[i] = Color.White;
            }
            for (int i = 0; i < width * height - width; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width - 1; i < width * height; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }

        Texture2D createSolidRectangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];

            // Colour the entire texture transparent first.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Color(0.2f, 0.2f, 0.2f, 0.25f);
            }
            for (int i = 0; i < width; i++)
            {
                data[i] = Color.White;
            }
            for (int i = 0; i < width * height - width; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width - 1; i < width * height; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
    }
}
