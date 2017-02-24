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
    class Level2 : Level
    {


        public Level2(string filePath, Player player, Texture2D solidSquare, Texture2D hollowSquare,
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
        }

        public override void Update(float time)
        {
            if (!LoseCondition())
            {
                for (int i = 0; i < spawners.Count; i++)
                {
                    spawners[i].Update(ref enemies, player.myPosition, time, player.myPoint, grid);
                }

                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(time, player.myPoint, grid);
                }
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public override void ActivateTeleport()
        {
            if (spawners.Count == 0 && enemies.Count == 0)
                teleport.IsActive = true;
        }

        protected override void Collisions()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.CheckMyCollision(player) && player.playerStates.status != PlayerStates.Status.Invulnerable)
                {
                    player.playerStates.status = PlayerStates.Status.Invulnerable;
                    --player.myHP;
                }
            }

            for (int i = 0; i < player.Projectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (player.Projectiles[i].CheckMyCollision(enemies[j]))
                        --enemies[j].myHP;
                }

                for (int l = 0; l < spawners.Count; l++)
                {
                    if (player.Projectiles[i].CheckMyCollision(spawners[l]))
                        --spawners[l].myHP;
                }
            }
            base.Collisions();
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

            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                if (!spawners[i].iamAlive)
                {
                    emitters.Add(new EnemyEmitter(hollowSquare, spawners[i].myPosition));
                    spawners.RemoveAt(i);
                }
            }

            base.RemoveDeadObjects();
        }
    }
}