using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace AlgoritmProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        TileGrid grid;
        Camera camera;


        List<Enemy> enemies = new List<Enemy>();
        Vector2 cameraRecoil;
        Vector2 recoil;

        //float timer = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grid = new TileGrid(createRectangle(32, 32, GraphicsDevice), 32, 100, 50);
            player = new Player(createRectangle(32, 32, GraphicsDevice), new Vector2(300, 200), createRectangle(5, 5, GraphicsDevice), 32);

            enemies.Add(new Enemy(createRectangle(32, 32, GraphicsDevice), new Vector2(64, 64), 32));

            camera = new Camera(new Rectangle(0, 0, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), new Rectangle(0, 0, Window.ClientBounds.Width * 2, Window.ClientBounds.Height * 2));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            KeyMouseReader.Update();
            player.Update();
            foreach (Wall w in grid.GetWalls)
            {
                int n = player.Collision(w);
                if (n > 0)
                {
                    player.HandelCollision(w, n);
                }
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(player.PlayerPoint, grid);
            }

            HandleCamera();
            base.Update(gameTime);
        }

        void HandleCamera()
        {
            if (player.ShotsFired)
            {
                cameraRecoil = player.PlayerPos;
                recoil = player.PlayerPos - new Vector2(KeyMouseReader.mouseState.Position.X, KeyMouseReader.mouseState.Position.Y);
                recoil.Normalize();

                player.PlayerPos += recoil * player.RecoilPower;

                cameraRecoil += recoil * (player.RecoilPower * 3);
                camera.Update(cameraRecoil);
            }
            else
                camera.Update(player.PlayerPos);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
                spriteBatch.Draw(createRectangle(3, 3, GraphicsDevice), enemy.EnemyPos, Color.Red);
            }
            player.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                foreach (Vector2 v in enemy.Way)
                {
                    spriteBatch.Draw(createRectangle(3, 3, GraphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        Texture2D createRectangle(int width, int height, GraphicsDevice graphicsDevice)
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
    }
}
