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
        private List<EnemySpawner> spawners = new List<EnemySpawner>();
        private List<JsonObject> jsonTiles = new List<JsonObject>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Item> items = new List<Item>();
        private List<Wall> walls = new List<Wall>();
        TileGrid grid;
        protected Texture2D hollowSquare, smallHollowSquare, solidSquare, smoothTexture;
        protected List<Emitter> emitters = new List<Emitter>();
        protected bool playerEmit = true;
        protected AICompanion companion;
        protected Teleporter teleport;
        protected Player player;
        

        public TileGrid GetGrid()
        {
            return this.grid;
        }

        public Level(string filePath, Player player, Texture2D solidSquare,
            Texture2D hollowSquare, Texture2D smallHollowSquare, Texture2D smoothTexture, int tileSize)
        {
            this.smallHollowSquare = smallHollowSquare;
            this.smoothTexture = smoothTexture;
            this.hollowSquare = hollowSquare;
            this.solidSquare = solidSquare;
            this.player = player;
            LoadLevel.LoadingLevel(filePath, ref jsonTiles, ref grid, ref walls,
                ref spawners, ref player, ref items, ref solidSquare, ref teleport,
                ref hollowSquare, ref smallHollowSquare, ref smoothTexture, tileSize);
            companion = new AICompanion(hollowSquare, player.myPosition, tileSize);
            foreach (Wall wall in walls)
            {
                grid.SetOccupiedGrid(wall);
            }
        }

        public virtual void Update(float time, Camera camera)
        {
            //Console.WriteLine("Companion: " + companion.myPosition);
            //Console.WriteLine("Player: " + player.myPosition);

            companion.DefaultState(player);
            foreach (Item item in items)
                companion.ApproachState(item, player);

            ActivateTeleport();
            UpdateObjects(time);
            Collisions(camera);
            RemoveDeadObjects();

            companion.Update(ref time);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawObjects(spriteBatch);
            companion.Draw(spriteBatch);
        }

        public virtual void ActivateTeleport()
        {
            if (player.weaponStates.type == WeaponStates.WeaponType.Pistol)
                teleport.IsActive = true;
        }

        public virtual bool WinCondition()
        {
            if (Vector2.Distance(player.myPosition, teleport.myPosition) <= 1 && teleport.IsActive && KeyMouseReader.KeyPressed(Keys.Enter))
            {
                return true;
            }
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

            if (teleport != null)
                teleport.Draw(spriteBatch);
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

            if (teleport != null)
                teleport.Update(ref time);
        }

        protected virtual void Collisions(Camera camera)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
                if (items[i].CheckMyCollision(player) || items[i].CheckMyCollision(companion))
                {
                    if (items[i] is Pistol)
                    {
                        player.weaponStates.type = WeaponStates.WeaponType.Pistol;
                        items.RemoveAt(i);
                    }
                }
            }
            // Collide walls
            for (int i = 0; i < player.Projectiles.Count; i++)
            {
                for (int k = 0; k < walls.Count; k++)
                {
                    player.Projectiles[i].CheckMyCollision(walls[k]);
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
        }
    }
}
