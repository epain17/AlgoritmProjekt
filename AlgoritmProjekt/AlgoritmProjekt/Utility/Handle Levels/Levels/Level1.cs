using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Enemies;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.ParticleEngine.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.Levels
{
    class Level1 : Level
    {
        private List<EnemySpawner> spawners = new List<EnemySpawner>();
        private List<JsonObject> jsonTiles = new List<JsonObject>();
        private List<Emitter> emitters = new List<Emitter>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Wall> walls = new List<Wall>();
        private List<Item> items = new List<Item>();
        private Player player;
        Teleporter teleport;
        TileGrid grid;
        PatrolEnemy testEnemy;

        public Level1(string filePath, Player player, Texture2D solidSquare, Texture2D hollowSquare,
            Texture2D smallHollowSquare, Texture2D smoothTexture, int tileSize)
            : base(filePath, player, solidSquare, hollowSquare,
                  smallHollowSquare, smoothTexture, tileSize)
        {
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = smoothTexture;
            this.hollowSquare = hollowSquare;
            this.solidSquare = solidSquare;
            this.player = player;
            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref grid, ref walls,
                ref spawners, ref player, ref items, ref solidSquare, ref teleport,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);

            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }
            testEnemy = new PatrolEnemy(solidSquare, spawners[0].myPosition, tileSize, 2, 3, 5);
        }

        public override void Update(float time, Camera camera)
        {

            base.Update(time, camera);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (testEnemy != null)
                testEnemy.Draw(spriteBatch);
            companion.Draw(spriteBatch);

        }

        public override void ActivateTeleport()
        {
            if (spawners.Count == 0 && enemies.Count == 0 || testEnemy != null && testEnemy.myHP == 0)
                teleport.IsActive = true;
        }

        public override bool WinCondition()
        {
            if (Vector2.Distance(player.myPosition, teleport.myPosition) <= 1 && teleport.IsActive && KeyMouseReader.KeyPressed(Keys.Enter))
            {
                return true;
            }
            return false;
        }

        public override bool LoseCondition()
        {
            if (player.myHP <= 0)
            {
                if (playerEmit)
                {
                    emitters.Add(new PlayerDeathEmitter(solidSquare, player.myPosition));
                    playerEmit = false;
                }
                return true;
            }
            return false;
        }

        protected override void DrawObjects(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            foreach (Wall wall in walls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Draw(spriteBatch);
            }

            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            teleport.Draw(spriteBatch);
        }

        protected override void UpdateObjects(float time)
        {
            companion.Update(ref time);
            companion.ApproachTarget(player);
            if (!LoseCondition())
            {
                for (int i = 0; i < spawners.Count; i++)
                {
                    spawners[i].Update(ref enemies, player.myPosition, time, player.myPoint, grid);
                    if (companion.AttackTarget(spawners[i]))
                        player.Shoot(companion.myPosition,spawners[i].myPosition);

                }

                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(time, player.myPoint, grid);
                    if (companion.AttackTarget(enemy))
                        player.Shoot(companion.myPosition, enemy.myPosition);
                }
                if (testEnemy != null)
                {
                    testEnemy.Update(ref time, player, grid);
                    if (companion.AttackTarget(testEnemy))
                        player.Shoot(companion.myPosition, testEnemy.myPosition);
                }
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Update(ref time);
            }
            teleport.Update(ref time);
        }

        protected override void Collisions(Camera camera)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.CheckMyCollision(player) && player.playerStates.status != PlayerStates.Status.Invulnerable)
                {
                    player.playerStates.status = PlayerStates.Status.Invulnerable;
                    --player.myHP;
                }
            }

            if (testEnemy != null && testEnemy.CheckMyCollision(player) && player.playerStates.status != PlayerStates.Status.Invulnerable)
            {
                player.playerStates.status = PlayerStates.Status.Invulnerable;
                --player.myHP;
            }

            for (int i = 0; i < player.Projectiles.Count; i++)
            {
                if (testEnemy != null && player.Projectiles[i].CheckMyCollision(testEnemy))
                    --testEnemy.myHP;

                for (int j = 0; j < enemies.Count; j++)
                {
                    if (player.Projectiles[i].CheckMyCollision(enemies[j]))
                        --enemies[j].myHP;
                }

                for (int k = 0; k < walls.Count; k++)
                {
                    player.Projectiles[i].CheckMyCollision(walls[k]);
                }

                for (int l = 0; l < spawners.Count; l++)
                {
                    if (player.Projectiles[i].CheckMyCollision(spawners[l]))
                        --spawners[l].myHP;
                }
            }
        }

        protected override void RemoveDeadObjects()
        {
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                if (!enemies[i].iamAlive)
                {
                    emitters.Add(new EnemyEmitter(hollowSquare, enemies[i].myPosition));
                    enemies.RemoveAt(i);
                }
                else if (Vector2.Distance(player.myPosition, enemies[i].myPosition) > Constants.screenHeight)
                    enemies.RemoveAt(i);
            }
            if (testEnemy != null && !testEnemy.iamAlive)
                testEnemy = null;
            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                if (!spawners[i].iamAlive)
                {
                    emitters.Add(new EnemyEmitter(hollowSquare, spawners[i].myPosition));
                    spawners.RemoveAt(i);
                }
            }

            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                if (!emitters[i].IsAlive)
                    emitters.RemoveAt(i);
            }
        }
    }
}