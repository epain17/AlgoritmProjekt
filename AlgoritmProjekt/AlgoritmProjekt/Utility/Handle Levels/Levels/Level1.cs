using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.Enemies;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.ParticleEngine.Emitters;
using AlgoritmProjekt.Utility.AI.DecisionTree;
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
        List<PatrolEnemy> testEnemy = new List<PatrolEnemy>();

        public Level1(string filePath, Player player, AICompanion companion, Texture2D solidSquare, Texture2D hollowSquare,
            Texture2D smallHollowSquare, Texture2D smoothTexture, int tileSize)
            : base(filePath, player, companion, solidSquare, hollowSquare,
                  smallHollowSquare, smoothTexture, tileSize)
        {
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = smoothTexture;
            this.hollowSquare = hollowSquare;
            this.solidSquare = solidSquare;
            this.player = player;
            this.companion = companion;
            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref grid, ref walls,
                ref spawners, ref player, ref items, ref solidSquare, ref goalCheckPoint,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);

            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }

            testEnemy.Add(new PatrolEnemy(solidSquare, spawners[0].myPosition, tileSize, 2, 3, 5));

            spawners.RemoveAt(0);
        }

        public override void Update(float time)
        {
            if (!LoseCondition())
            {
                foreach (PatrolEnemy enemy in testEnemy)
                {
                    enemy.Update(ref time, player, grid);
                }
                companion.Perception(time, player, testEnemy);
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (PatrolEnemy enemy in testEnemy)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public override void ActivateGoal()
        {
            if (testEnemy.Count <= 0)
                goalCheckPoint.IsActive = true;
        }

        protected override void Collisions()
        {
            foreach (PatrolEnemy enemy in testEnemy)
            {
                if (enemy != null && enemy.CheckMyIntersect(player) && player.playerStates.status != PlayerStates.Status.Invulnerable)
                {
                    player.playerStates.status = PlayerStates.Status.Invulnerable;
                    //--player.myHP;
                }
            }

            for (int i = 0; i < companion.Projectiles.Count; i++)
            {
                foreach (PatrolEnemy enemy in testEnemy)
                {
                    if (companion.Projectiles[i].CheckMyIntersect(enemy))
                        --enemy.myHP;
                }
            }

            base.Collisions();
        }

        protected override void RemoveDeadObjects()
        {
            for (int i = testEnemy.Count - 1; i >= 0; i--)
            {
                if (!testEnemy[i].iamAlive)
                {
                    items.Add(new Item(hollowSquare, testEnemy[i].myPosition, tileSize));
                    testEnemy.RemoveAt(i);
                }
            }

            base.RemoveDeadObjects();
        }
    }
}