using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.ParticleEngine.Emitters;
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
        Texture2D hollowSquare, smallHollowSquare, solidSquare;
        CrossHair xhair;
        TileGrid grid;
        Camera camera;

        Vector2 cameraRecoil;
        Vector2 recoil;
        int size = 32;
        string timeFont = "Time: ", scoreFont = "Score: ", lifeFont = "Life: ", energyFont = "Energy: ", gameOverFont = "Game Over", winFont = "Level Completed";

        List<EnemySpawner> spawners = new List<EnemySpawner>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Emitter> emitters = new List<Emitter>();
        List<Enemy> enemies = new List<Enemy>();
        List<Wall> walls = new List<Wall>();
        Player player;
        SpriteFont font;
        int score = 0;
        float TotalTime;

        bool playerEmit = true;
        int screenWidth, screenHeight;
        float powerMeter = 100;

        public GameManager(int screenWidth, int screenHeight, SpriteFont font, string filePath,
            Texture2D solidSquare, Texture2D hollowSquare, Texture2D smallHollowSquare)
        {
            camera = new Camera(new Rectangle(0, 0, screenWidth / 2, screenHeight / 2), new Rectangle(0, 0, screenWidth * 4, screenHeight * 4));
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.font = font;
            this.solidSquare = solidSquare;
            this.hollowSquare = hollowSquare;
            this.smallHollowSquare = smallHollowSquare;
            grid = new TileGrid(hollowSquare, size, 100, 50);
            xhair = new CrossHair(hollowSquare, smallHollowSquare, new Vector2(200, 200), size);

            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref walls,
                ref spawners, ref player, ref solidSquare, ref hollowSquare, ref smallHollowSquare, size);
            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Winner())
                TotalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            PlayerShoots();
            //tillfällig funktion för att kolla olika vapen - fungerar som att lvlaupp
            if (score >= 3500)
                player.weaponState = Player.WeaponType.MachineGun;
            else if (score >= 1500)
                player.weaponState = Player.WeaponType.ShotGun;

            //Ordningen är viktig - kameran tar emot på ett elastiskt sätt mot väggarna
            UpdateObjects((float)gameTime.ElapsedGameTime.TotalSeconds);
            xhair.Update(camera.CameraPos, player.myPosition);
            HandleCamera();
            //om de nedan kastas om tar fiender skada 2 ggr / skott
            RemoveDeadObjects();
            Collisions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TranslationMatrix);
            grid.Draw(spriteBatch);
            DrawAllObjects(spriteBatch);
            xhair.Draw(spriteBatch);
            DrawFonts(spriteBatch);
            spriteBatch.End();
        }

        public bool Winner()
        {
            if (spawners.Count() == 0)
                return true;
            return false;
        }

        public bool GameOver()
        {
            if (player.myHP == 0)
            {
                if (playerEmit)
                {
                    emitters.Add(new PlayerEmitter(solidSquare, player.myPosition));
                    playerEmit = false;
                }
                return true;
            }
            return false;
        }

        private void DrawFonts(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, scoreFont + score, new Vector2(-camera.CameraPos.X, -camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(font, timeFont + (int)TotalTime, new Vector2((screenWidth / 2) - (font.MeasureString(timeFont).X / 2) - camera.CameraPos.X, -camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(font, lifeFont + player.myHP, new Vector2(-camera.CameraPos.X, screenHeight - font.MeasureString(lifeFont).Y - camera.CameraPos.Y), Color.LimeGreen);
            spriteBatch.DrawString(font, energyFont + (int)powerMeter, new Vector2(screenWidth - (font.MeasureString(energyFont).X + (size * 3)) - camera.CameraPos.X, screenHeight - font.MeasureString(energyFont).Y - camera.CameraPos.Y), Color.LimeGreen);
            if (GameOver())
                spriteBatch.DrawString(font, gameOverFont, new Vector2(350 - camera.CameraPos.X, 200 - camera.CameraPos.Y), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            if (Winner())
                spriteBatch.DrawString(font, winFont, new Vector2(350 - camera.CameraPos.X, 200 - camera.CameraPos.Y), Color.Red, 0, new Vector2(40, 0), 1.5f, SpriteEffects.None, 0);

        }
        //not in use
        private void DrawWaypoints(SpriteBatch spriteBatch)
        {
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

            if (!GameOver())
                player.Draw(spriteBatch);
            //spriteBatch.Draw(createRectangle(3, 3, graphicsDevice), player.myPosition, Color.Red);
        }

        private void UpdateObjects(float time)
        {
            PlayerPowerUp(ref time);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(time, player.myPoint, grid);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Update(time);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(time);
            }

            if (!GameOver())
            {
                player.Update(time);

                for (int i = 0; i < spawners.Count; i++)
                {
                    spawners[i].Update(ref enemies, player.myPosition, time);
                }
            }
        }

        private void PlayerShoots()
        {
            if (player.ShotsFired)
            {
                Random rand = new Random();

                if (player.weaponState == Player.WeaponType.ShotGun)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        projectiles.Add(new Projectile(smallHollowSquare, player.myPosition, size, new Vector2(xhair.myPosition.X + rand.Next(-20, 20), xhair.myPosition.Y + rand.Next(-20, 20))));
                    }
                }
                else if (player.weaponState == Player.WeaponType.MachineGun)
                    projectiles.Add(new Projectile(smallHollowSquare, player.myPosition, size, new Vector2(xhair.myPosition.X + rand.Next(-10, 10), xhair.myPosition.Y + rand.Next(-10, 10))));
                else if (player.weaponState == Player.WeaponType.Pistol)
                    projectiles.Add(new Projectile(smallHollowSquare, player.myPosition, size, new Vector2(xhair.myPosition.X + rand.Next(-3, 3), xhair.myPosition.Y + rand.Next(-3, 3))));

                player.ShotsFired = false;
            }
        }

        private void PlayerPowerUp(ref float time)
        {
            if (player.playerState == Player.PlayerState.Power && powerMeter > 0)
            {
                powerMeter -= 0.15f;
                time *= 0.35f;
            }
            else if (powerMeter < 30)
                powerMeter += 0.15f;
        }

        //score måste ses över
        private void RemoveDeadObjects()
        {
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                if (!enemies[i].iamAlive)
                {
                    emitters.Add(new EnemyEmitter(hollowSquare, enemies[i].myPosition));
                    enemies.RemoveAt(i);
                    score += 25;
                }
            }

            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                if (!spawners[i].iamAlive)
                {
                    emitters.Add(new EnemyEmitter(hollowSquare, spawners[i].myPosition));
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
                if (enemy.CheckMyCollision(player) && player.playerState != Player.PlayerState.Invulnerable)
                {
                    player.playerState = Player.PlayerState.Invulnerable;
                    player.myHP--;
                }

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

    }
}
