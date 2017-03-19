using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Projectiles;
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

namespace AlgoritmProjekt.Utility.Handle_Levels
{
    class Level
    {
        public List<EnemySpawner> spawners = new List<EnemySpawner>();
        public List<JsonObject> jsonTiles = new List<JsonObject>();
        public List<Emitter> emitters = new List<Emitter>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Wall> walls = new List<Wall>();
        public List<Item> items = new List<Item>();

        protected Texture2D hollowSquare, smallHollowSquare, solidSquare, smoothTexture;
        protected AICompanion companion;
        protected Goal goalCheckPoint;
        protected TileGrid grid;
        protected Player player;

        protected int tileSize;
        protected bool playerEmit = true;


        public TileGrid GetGrid()
        {
            return this.grid;
        }

        public Level(string filePath, Player player, AICompanion companion, Texture2D solidSquare,
            Texture2D hollowSquare, Texture2D smallHollowSquare, Texture2D smoothTexture, int tileSize)
        {
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = smoothTexture;
            this.hollowSquare = hollowSquare;
            this.solidSquare = solidSquare;
            this.player = player;
            this.companion = companion;
            this.tileSize = tileSize;
            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref grid, ref walls,
                ref spawners, ref player, ref items, ref solidSquare, ref goalCheckPoint,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);
            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }

        }

        public virtual void Update(float time)
        {
            ActivateGoal();
            UpdateObjects(time);
            Collisions();
            RemoveDeadObjects();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawObjects(spriteBatch);
        }

        public virtual void ActivateGoal()
        {
            if (items.Count <= 0)
                goalCheckPoint.IsActive = true;
        }

        public virtual bool WinCondition()
        {
            if (Vector2.Distance(player.myPosition, goalCheckPoint.myPosition) <= 1 && goalCheckPoint.IsActive && KeyMouseReader.KeyPressed(Keys.Enter))
                return true;

            return false;
        }

        public virtual bool LoseCondition()
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

        protected virtual void DrawObjects(SpriteBatch spriteBatch)
        {
            if (grid != null)
                grid.Draw(spriteBatch);
            foreach (Wall wall in walls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Draw(spriteBatch);
            }

            foreach (Item item in items)
            {
                item.Draw(spriteBatch);
            }

            if (goalCheckPoint != null)
                goalCheckPoint.Draw(spriteBatch);
        }

        protected virtual void UpdateObjects(float time)
        {
            foreach (Item item in items)
            {
                item.Update(ref time);
            }

            foreach (Emitter emitter in emitters)
            {
                emitter.Update(ref time);
            }

            if (goalCheckPoint != null)
                goalCheckPoint.Update(ref time);
        }

        protected virtual void Collisions()
        {
            for (int i = 0; i < companion.Projectiles.Count; i++)
            {
                for (int k = 0; k < walls.Count; k++)
                {
                    companion.Projectiles[i].CheckMyIntersect(walls[k]);
                }
            }
        }

        protected virtual void RemoveDeadObjects()
        {
            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                if (!emitters[i].IsAlive)
                    emitters.RemoveAt(i);
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].CheckMyIntersect(player) || items[i].CheckMyVectorCollision(companion.myPosition))
                {
                    companion.PowerUp();
                    items.RemoveAt(i);
                }
            }
        }
    }
}
