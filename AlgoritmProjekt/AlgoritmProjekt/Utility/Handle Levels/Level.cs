using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.GameObjects.StaticObjects.Environment;
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
        public readonly TileGrid
            WorldMesh,
            NavigationMesh;

        public Vector2 myStartPosition;

        public Level(TileGrid worldMesh)
        {
            WorldMesh = worldMesh;
            NavigationMesh = new TileGrid(worldMesh.tileWidth / 3, worldMesh.tileHeight / 3, worldMesh.gridWidth * 3, worldMesh.gridHeight * 3);

            for (int i = 0; i < WorldMesh.gridWidth; i++)
            {
                for (int j = 0; j < WorldMesh.gridHeight; j++)
                {
                    if (WorldMesh.ReturnTile(i, j).myType == Tile.TileType.WALL)
                        NavigationMesh.SetOccupiedGrid(WorldMesh.ReturnTile(i, j));
                }
            }

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

            return false;
        }

        public virtual bool LoseCondition(Player player)
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

        }

        protected virtual void RemoveDeadObjects()
        {

        }

    }
}
