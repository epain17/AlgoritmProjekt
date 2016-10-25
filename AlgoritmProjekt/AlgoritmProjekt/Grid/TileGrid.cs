using AlgoritmProjekt.Characters;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Grid
{
    class TileGrid
    {
        public int width, height;
        Tile[,] tileGrid;
        Wall wall1, wall2, wall3, wall4;
        List<Wall> wallList = new List<Wall>();
        int tileSize;
        Texture2D tileTex;

        public TileGrid(Texture2D tileTex)
        {
            width = 60;
            height = 35;
            tileSize = 32;
            this.tileTex = tileTex;
            
            CreateTileGrid();
            CreateWalls();
        }

        public void CreateTileGrid()
        {
            tileGrid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j] = new Tile(tileTex, new Vector2(0 + i * tileSize, 0 + j * tileSize), tileSize);
                }
            }
        }

        public List<Wall> GetWalls
        {
            get { return wallList; }
        }

      public void CreateWalls()
        {
            wall1 = new Wall(tileTex, new Vector2(128, 160), 32);
            wall2 = new Wall(tileTex, new Vector2(128, 192), 32);
            wall3 = new Wall(tileTex, new Vector2(128, 224), 32);
            wall4 = new Wall(tileTex, new Vector2(128, 256), 32);
            wallList.Add(wall1);
            wallList.Add(wall2);
            wallList.Add(wall3);
            wallList.Add(wall4);
            foreach (Tile t in tileGrid)
            {
                foreach(Wall w in wallList)
                {
                    if(w.TilePoint == t.TilePoint)
                    {
                        t.Occupied = true;
                    }
                }
            }
        }
                
        public int CheckWalkable(int cellX, int cellY)
        {
            for (int i = cellX; i < width; i++)
            {
                for (int j = cellY; j < height; j++)
                {
                    if (tileGrid[i, j].Occupied == false && tileGrid[i, j] != null)
                    {
                        return 0;
                    }

                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tileGrid != null)
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        tileGrid[i, j].Draw(spriteBatch);
                    }
                }
            wall1.Draw(spriteBatch);
            wall2.Draw(spriteBatch);
            wall3.Draw(spriteBatch);
            wall4.Draw(spriteBatch);
        }
    }
}
