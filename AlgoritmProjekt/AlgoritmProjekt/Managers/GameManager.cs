using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers
{
    class GameManager
    {
        Player player;
        TileGrid grid;
        Camera camera;
        CrossHair xhair;

        List<Enemy> enemies = new List<Enemy>();
        Vector2 cameraRecoil;
        Vector2 recoil;
        GraphicsDevice graphicsDevice;

        public GameManager(GameWindow Window, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            grid = new TileGrid(createRectangle(32, 32, graphicsDevice), 32, 100, 50);
            player = new Player(createRectangle(32, 32, graphicsDevice), new Vector2(300, 200), createRectangle(5, 5, graphicsDevice), 32);

            enemies.Add(new Enemy(createRectangle(32, 32, graphicsDevice), new Vector2(64, 64), 32, 10));

            camera = new Camera(new Rectangle(0, 0, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), new Rectangle(0, 0, Window.ClientBounds.Width * 2, Window.ClientBounds.Height * 2));
            xhair = new CrossHair(createRectangle(32, 32, graphicsDevice), new Vector2(200, 200), 32);
        }


        public void Update(GameTime gameTime)
        {
            player.Update(xhair.myPosition);
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
                enemy.Update(player.myPoint, grid);
                player.CheckHit(enemy);
            }
            xhair.Update(camera.CameraPos);
            HandleCamera();
        }

        void HandleCamera()
        {
            if (player.ShotsFired)
            {
                cameraRecoil = player.myPosition;
                recoil = player.myPosition - new Vector2(xhair.myPosition.X, xhair.myPosition.Y);
                recoil.Normalize();

                player.myPosition += recoil * player.RecoilPower;

                cameraRecoil += recoil * (player.RecoilPower * 3);
                camera.Update(cameraRecoil);
            }
            else
                camera.Update(player.myPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
                spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), enemy.myPosition, Color.Red);
            }
            player.Draw(spriteBatch);
            spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), player.myPosition, Color.Red);

            foreach (Enemy enemy in enemies)
            {
                foreach (Vector2 v in enemy.Way)
                {
                    spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
                }
            }
            xhair.Draw(spriteBatch);
            spriteBatch.End();
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
