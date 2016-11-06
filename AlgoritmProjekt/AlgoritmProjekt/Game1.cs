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
        }
        public static GameState gameState = GameState.menu;
        public static bool EXIT = false, RELOADGAMEPLAY = false, LoadJsonLevel = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameManager;
        Menu menu;
        SpriteFont font;
        Texture2D smoothTex;

        string LevelName = "SaveTest.json";
        int screenWidth = 800, screenHeight = 600;

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
            gameManager = new GameManager(screenWidth, screenHeight, font, LevelName, createSolidRectangle(32, 32, GraphicsDevice),
                createHollowRectangle(32, 32, GraphicsDevice), createHollowRectangle(3, 3, GraphicsDevice));
            smoothTex = Content.Load<Texture2D>("circle");
            menu = new Menu(Window, font, new Vector2(screenWidth / 2, screenHeight / 2), smoothTex);
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
                gameManager = new GameManager(screenWidth, screenHeight, font, LevelName, createSolidRectangle(32, 32, GraphicsDevice),
                                createHollowRectangle(32, 32, GraphicsDevice), createHollowRectangle(3, 3, GraphicsDevice));
                RELOADGAMEPLAY = false;
                gameState = GameState.gamePlay;
            }

            HandleScreens(gameTime);

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
            }

            base.Draw(gameTime);
        }

        private void HandleScreens(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.menu: //MENU
                    menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.gamePlay: //GAMEPLAY
                    gameManager.Update(gameTime);
                    if (gameManager.GameOver())
                        if (KeyMouseReader.KeyPressed(Keys.Space) ||
                            KeyMouseReader.KeyPressed(Keys.Enter) ||
                            KeyMouseReader.KeyPressed(Keys.Escape))
                        {
                            menu = new Menu(Window, font, new Vector2(screenWidth / 2, screenHeight / 2), smoothTex);
                            gameState = GameState.menu;
                        }
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        gameState = GameState.menu;
                    break;
                case GameState.highscore: //HIGHSCORES
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
