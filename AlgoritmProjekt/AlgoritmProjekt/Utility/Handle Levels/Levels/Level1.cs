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
                ref spawners, ref player, ref items, ref solidSquare, ref goalCheckPoint,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);

            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }

            testEnemy = new PatrolEnemy(solidSquare, spawners[0].myPosition, tileSize, 2, 3, 5);

            spawners.RemoveAt(0);
        }

        public override void Update(float time)
        {
            if (!LoseCondition())
            {
                if (testEnemy != null)
                    testEnemy.Update(ref time, player, grid);
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (testEnemy != null)
                testEnemy.Draw(spriteBatch);
        }

        public override void ActivateGoal()
        {
            if (testEnemy == null)
                goalCheckPoint.IsActive = true;
        }

        protected override void Collisions()
        {
            if (testEnemy != null && testEnemy.CheckMyIntersect(player) && player.playerStates.status != PlayerStates.Status.Invulnerable)
            {
                player.playerStates.status = PlayerStates.Status.Invulnerable;
                //--player.myHP;
            }

            for (int i = 0; i < player.Projectiles.Count; i++)
            {
                if (testEnemy != null && player.Projectiles[i].CheckMyIntersect(testEnemy))
                    --testEnemy.myHP;
            }

            base.Collisions();
        }

        protected override void RemoveDeadObjects()
        {
            if (testEnemy != null && !testEnemy.iamAlive)
            {
                items.Add(new Item(hollowSquare, testEnemy.myPosition, tileSize));
                testEnemy = null;
            }

            base.RemoveDeadObjects();
        }
    }
}