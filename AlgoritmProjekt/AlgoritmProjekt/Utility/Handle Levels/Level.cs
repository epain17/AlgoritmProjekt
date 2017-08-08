using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects.Environment;
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
        //public List<Item>
        //    items = new List<Item>();
        public readonly TileGrid 
            WorldMesh, 
            NavigationMesh;
        public Vector2 
            StartPosition;

        Player player;
        
        public Level(Player player, TileGrid worldMesh)
        {
            this.player = player;
            this.WorldMesh = worldMesh;
            NavigationMesh = new TileGrid(worldMesh.tileWidth / 3, worldMesh.tileHeight / 3, worldMesh.gridWidth * 3, worldMesh.gridHeight * 3);
        }

        public virtual void Update(float time)
        {
            UpdateObjects(time);
            Collisions();
            RemoveDeadObjects();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            WorldMesh.Draw(spriteBatch);
            DrawObjects(spriteBatch);
        }

        public virtual bool WinCondition()
        {
            //if (Vector2.Distance(player.myPosition, door.myPosition) <= 1 && door.IsActive && KeyMouseReader.KeyPressed(Keys.Enter))
            //    return true;

            return false;
        }

        public virtual bool LoseCondition()
        {
            if (!player.Alive())
                return true;
            return false;
        }

        protected virtual void DrawObjects(SpriteBatch spriteBatch)
        {

        }

        protected virtual void UpdateObjects(float time)
        {            

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
