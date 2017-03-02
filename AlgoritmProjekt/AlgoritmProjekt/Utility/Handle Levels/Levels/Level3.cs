using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.Utility.AI.DecisionTree;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.Levels
{
    class Level3 : Level
    {
        List<Enemy> dtEnemies;

        public Level3(string filePath, Player player, Texture2D solidSquare, Texture2D hollowSquare,
            Texture2D smallHollowSquare, Texture2D smoothTexture, int tileSize)
            : base(filePath, player, solidSquare, hollowSquare,
                  smallHollowSquare, smoothTexture, tileSize)
        {
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = smoothTexture;
            this.hollowSquare = hollowSquare;
            this.solidSquare = solidSquare;
            this.player = player;

            dtEnemies = new List<Enemy>();

            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref grid, ref walls,
                ref spawners, ref player, ref items, ref solidSquare, ref teleport,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);

            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }

            //for (int i = 0; i < spawners.Count; i++)
            //{
            //    dtEnemies.Add(new DTEnemy(solidSquare, spawners[i].myPosition, tileSize, smoothTexture, player));
            //}
            dtEnemies.Add(new DTEnemy(solidSquare, spawners[0].myPosition, tileSize, smoothTexture, player));

            for (int i = spawners.Count - 1; i >= 0; --i)
            {
                spawners.RemoveAt(i);
            }
        }

        public override void Update(float time)
        {
            if (!LoseCondition())
            {
                foreach (DTEnemy enemy in dtEnemies)
                {
                    enemy.Update(time, player, grid);
                }
                //companion.Perception(time, player, items, dtEnemies, spawners);
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (DTEnemy enemy in dtEnemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public override void ActivateTeleport()
        {
            if (dtEnemies.Count == 0)
                teleport.IsActive = true;
        }

        protected override void Collisions()
        {
            for (int i = 0; i < player.Projectiles.Count; i++)
            {
                foreach (DTEnemy enemy in dtEnemies)
                {
                    if (player.Projectiles[i].CheckMyIntersect(enemy))
                        --enemy.myHP;
                }
            }

            base.Collisions();
        }

        protected override void RemoveDeadObjects()
        {
            for (int i = dtEnemies.Count - 1; i >= 0; --i)
            {
                if (!dtEnemies[i].iamAlive)
                {
                    dtEnemies.RemoveAt(i);
                }
            }

            base.RemoveDeadObjects();
        }
    }
}
