using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects.Environment;
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

namespace AlgoritmProjekt.Utility.Handle_Levels.PCG
{
    class Level
    {
        public List<Enemy>
            enemies = new List<Enemy>();
        public List<Item>
            items = new List<Item>();
        public List<Door>
            doors = new List<Door>();

        Player player;

        bool playerDied = false;

        public Vector2 StartPosition;

        public TileGrid worldMesh, navigationMesh;

        public TileGrid GetNavigationMesh()
        {
            return navigationMesh;
        }

        public Level(Player player)
        {
            this.player = player;
            worldMesh = new TileGrid(Constants.tileSize, Constants.tileSize, 50, 50);
            navigationMesh = new TileGrid(Constants.tileSize / 3, Constants.tileSize / 3, 150, 150);
        }

        public virtual void Update(float time)
        {
            UnlockDoor();
            UpdateObjects(time);
            Collisions();
            RemoveDeadObjects();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            worldMesh.Draw(spriteBatch);

            //navigationMesh.Draw(spriteBatch);
            DrawObjects(spriteBatch);
        }

        public virtual void UnlockDoor()
        {
            //if (items.Count <= 0)
            //    door.IsActive = true;
        }

        public virtual bool WinCondition()
        {
            //if (Vector2.Distance(player.myPosition, door.myPosition) <= 1 && door.IsActive && KeyMouseReader.KeyPressed(Keys.Enter))
            //    return true;

            return false;
        }

        public virtual bool LoseCondition()
        {
            //if (player.myHP <= 0)
            //{
            //    if (!playerDied)
            //    {
            //        playerDied = true;
            //    }
            //    return true;
            //}
            return false;
        }

        protected virtual void DrawObjects(SpriteBatch spriteBatch)
        {

        }

        protected virtual void UpdateObjects(float time)
        {            
            foreach (Door door in doors)
            {
                door.Update(ref time);
            }            
        }

        protected virtual void Collisions()
        {
            //for (int i = 0; i < companion.Projectiles.Count; i++)
            //{
            //    for (int k = 0; k < walls.Count; k++)
            //    {
            //        companion.Projectiles[i].CheckMyIntersect(walls[k]);
            //    }
            //}
        }

        protected virtual void RemoveDeadObjects()
        {            
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (items[i].CheckMyIntersect(player))
            //    {
            //        items.RemoveAt(i);
            //    }
            //}
        }

    }
}
