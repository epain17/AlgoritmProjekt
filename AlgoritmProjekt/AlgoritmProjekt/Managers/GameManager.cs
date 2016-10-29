using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Projectiles;
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
        List<JsonObject> jsonTiles = new List<JsonObject>();
        GraphicsDevice graphicsDevice;
        Texture2D square, smallSquare;
        CrossHair xhair;
        TileGrid grid;
        Camera camera;

        Vector2 cameraRecoil;
        Vector2 recoil;
        int size = 32;

        List<EnemySpawner> spawners = new List<EnemySpawner>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Enemy> enemies = new List<Enemy>();
        List<Wall> walls = new List<Wall>();
        Player player;
        SpriteFont font;
        int score = 0;

        List<Emitter> emitters = new List<Emitter>();

        public GameManager(GameWindow Window, GraphicsDevice graphicsDevice, SpriteFont font)
        {
            camera = new Camera(new Rectangle(0, 0, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), new Rectangle(0, 0, Window.ClientBounds.Width * 4, Window.ClientBounds.Height * 4));
            this.graphicsDevice = graphicsDevice;
            this.font = font;
            square = createRectangle(size, size, graphicsDevice);
            smallSquare = createRectangle(3, 3, graphicsDevice);
            grid = new TileGrid(square, size, 100, 50);
            xhair = new CrossHair(square, smallSquare, new Vector2(200, 200), size);

            LoadLevel.LoadingLevel("SaveTest.json", ref jsonTiles, ref walls,
                ref spawners, ref player, ref square, ref smallSquare, size);
            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (score > 3500)
                player.weaponState = Player.WeaponType.MachineGun;
            else if (score > 1500)
                player.weaponState = Player.WeaponType.ShotGun;
            xhair.Update(camera.CameraPos, player.myPosition);
            WhenPlayerShoots();
            UpdateObjects((float)gameTime.ElapsedGameTime.TotalSeconds);
            RemoveDeadObjects();
            player.Update(xhair.myPosition);
            HandleCamera();
            Collisions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);
            DrawAllObjects(spriteBatch);
            xhair.Draw(spriteBatch);
            //spriteBatch.Draw(smallSquare, new Vector2(xhair.myPosition.X - 1.5f, xhair.myPosition.Y - 1.5f), Color.Red);
            //foreach (Enemy enemy in enemies)
            //{
            //    foreach (Vector2 v in enemy.Way)
            //    {
            //        spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
            //    }
            //}

            //foreach (Enemy enemy in enemies)
            //{
            //    foreach (Vector2 v in enemy.Way)
            //    {
            //        spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), new Vector2(v.X, v.Y), Color.Yellow);
            //    }
            //}
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(10 - camera.CameraPos.X, -camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.End();
        }

        private void DrawAllObjects(SpriteBatch spriteBatch)
        {
            foreach (Wall wall in walls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Draw(spriteBatch);
            }

            foreach (Projectile p in projectiles)
            {
                p.Draw(spriteBatch);
            }

            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
                //spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), enemy.myPosition, Color.Red);
            }
            player.Draw(spriteBatch);
            //spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), player.myPosition, Color.Red);
        }

        private void UpdateObjects(float time)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(player.myPoint, grid);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Update(time);
            }

            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].Update(ref enemies, player.myPosition, time);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update();
            }
        }

        private void WhenPlayerShoots()
        {
            if (player.ShotsFired)
            {
                Random rand = new Random();

                if (player.weaponState == Player.WeaponType.ShotGun)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        projectiles.Add(new Projectile(smallSquare, player.myPosition, 3, new Vector2(xhair.myPosition.X + rand.Next(-20, 20), xhair.myPosition.Y + rand.Next(-20, 20))));
                    }
                }
                else if (player.weaponState == Player.WeaponType.MachineGun)
                    projectiles.Add(new Projectile(smallSquare, player.myPosition, 3, new Vector2(xhair.myPosition.X + rand.Next(-10, 10), xhair.myPosition.Y + rand.Next(-10, 10))));
                else if (player.weaponState == Player.WeaponType.Pistol)
                    projectiles.Add(new Projectile(smallSquare, player.myPosition, 3, new Vector2(xhair.myPosition.X + rand.Next(-3, 3), xhair.myPosition.Y + rand.Next(-3, 3))));

                player.ShotsFired = false;
            }
        }

        private void RemoveDeadObjects()
        {
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                if (!enemies[i].iamAlive)
                {
                    emitters.Add(new Emitter(square, font, enemies[i].myPosition, 1));
                    enemies.RemoveAt(i);
                    score += 25;
                }
            }

            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                if (!spawners[i].iamAlive)
                {
                    spawners.RemoveAt(i);
                    score += 275;
                }
            }

            for (int i = projectiles.Count - 1; i >= 0; --i)
            {
                if (!projectiles[i].iamAlive)
                    projectiles.RemoveAt(i);
            }

            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                if (!emitters[i].IsAlive)
                    emitters.RemoveAt(i);
            }
        }

        private void HandleCamera()
        {
            if (player.ShotsFired)
            {
                cameraRecoil = player.myPosition;
                recoil = player.myPosition - new Vector2(xhair.myPosition.X, xhair.myPosition.Y);
                recoil.Normalize();

                //player.myPosition += recoil * player.RecoilPower;

                cameraRecoil += recoil * player.RecoilPower;
                camera.Update(cameraRecoil);
            }
            else
                camera.Update(player.myPosition);
        }

        private void Collisions()
        {
            foreach (Enemy enemy in enemies)
            {
                foreach (Projectile shot in projectiles)
                {
                    if (shot.CheckMyCollision(enemy))
                    {
                        enemy.myHP--;
                    }
                }
            }

            foreach (Wall wall in walls)
            {
                if (player.CheckMyCollision(wall))
                    player.SolveCollision(wall);

                foreach (Projectile p in projectiles)
                {
                    p.CheckMyCollision(wall);
                }
            }

            foreach (EnemySpawner spawner in spawners)
            {
                foreach (Projectile p in projectiles)
                {
                    if (p.CheckMyCollision(spawner))
                        spawner.myHP--;
                }
            }
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
