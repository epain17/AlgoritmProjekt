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
        public static bool EXIT = false, RELOAD = false, LoadJsonLevel = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameManager;
        Menu menu;
        SpriteFont font;
        Texture2D smoothTex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            gameManager = new GameManager(Window, GraphicsDevice, font);
            smoothTex = Content.Load<Texture2D>("circle");
            menu = new Menu(Window, font, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), smoothTex);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (EXIT)
                Exit();
            if (RELOAD)
            {
                gameManager = new GameManager(Window, GraphicsDevice, font);
                RELOAD = false;
            }
            KeyMouseReader.Update();
            switch (gameState)
            {
                case GameState.menu:
                    menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.gamePlay:
                    gameManager.Update(gameTime);
                    if (KeyMouseReader.KeyPressed(Keys.Escape))
                        gameState = GameState.menu;
                    break;
            }
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
    }
}
