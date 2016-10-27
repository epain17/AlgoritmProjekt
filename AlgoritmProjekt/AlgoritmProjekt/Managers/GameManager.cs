using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects;
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
        TileGrid grid;
        Camera camera;
        CrossHair xhair;

        Vector2 cameraRecoil;
        Vector2 recoil;
        GraphicsDevice graphicsDevice;
        Texture2D square, smallSquare;

        int size = 32;
        int spawnHP = 50;
        List<JsonObject> jsonTiles = new List<JsonObject>();
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        List<Enemy> enemies = new List<Enemy>();
        List<Wall> walls = new List<Wall>();
        Player player;
        Enemy enemy;

        public GameManager(GameWindow Window, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            square = createRectangle(size, size, graphicsDevice);
            smallSquare = createRectangle(5, 5, graphicsDevice);
            grid = new TileGrid(square, size, 100, 50);
            player = new Player(square, new Vector2(64, 64), smallSquare, size);
            enemy = new Enemy(square, new Vector2(126, 128), size, 10);


            enemies.Add(enemy);



           // LoadLevel.LoadingLevel("SaveTest.json", ref jsonTiles, ref walls, ref spawners, ref player, ref square, ref smallSquare, size, spawnHP);

            camera = new Camera(new Rectangle(0, 0, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), new Rectangle(0, 0, Window.ClientBounds.Width * 4, Window.ClientBounds.Height * 4));
            xhair = new CrossHair(square, new Vector2(200, 200), size);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(xhair.myPosition);
            //foreach (Wall w in grid.GetWalls)
            //{
            //    int n = player.Collision(w);
            //    if (n > 0)
            //    {
            //        player.HandelCollision(w, n);
            //    }
            //}
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(player.myPoint, grid);
                player.CheckHit(enemy);
            }
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                if (!enemies[i].Alive)
                    enemies.RemoveAt(i);
            }
            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                if (!spawners[i].Alive)
                    spawners.RemoveAt(i);
            }
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Update(ref enemies);
                player.CheckHit(spawner);
            }
            HandleCamera();
            xhair.Update(camera.CameraPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
                //spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), enemy.myPosition, Color.Red);
            }
            player.Draw(spriteBatch);
            //spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), player.myPosition, Color.Red);

            foreach (Wall wall in walls)
            {
                wall.Draw(spriteBatch);
            }


            foreach (Enemy enemy in enemies)
            {
                foreach (Vector2 v in enemy.Way)
                {
                    spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
                }
            }

            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Draw(spriteBatch);
            }

            //foreach (Enemy enemy in enemies)
            //{
            //    foreach (Vector2 v in enemy.Way)
            //    {
            //        spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
            //    }
            //}

            xhair.Draw(spriteBatch);
            spriteBatch.Draw(smallSquare, new Vector2(xhair.myPosition.X - 2.5f, xhair.myPosition.Y - 2.5f), Color.Red);
            spriteBatch.End();
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
