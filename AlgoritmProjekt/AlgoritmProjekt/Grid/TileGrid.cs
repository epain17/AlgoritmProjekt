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
        public int gridWidth, gridHeight;
        public Tile[,] Grid;

        int tileWidth, tileHeight;

        public TileGrid(int width, int height, int columns, int rows)
        {
            this.tileWidth = width;
            this.tileHeight = height;
            this.gridWidth = columns;
            gridHeight = rows;

            CreateTileGrid();
        }

        private void CreateTileGrid()
        {
            Grid = new Tile[gridWidth, gridHeight];

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    Grid[i, j] = new Tile(new Vector2(i * tileWidth, j * tileWidth), tileWidth, tileHeight);
                }
            }

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (j - 1 >= 0)
                        Grid[i, j].SetNorthNeighbour(Grid[i, j - 1]);
                    if (i + 1 < gridWidth)
                        Grid[i, j].SetEastNeighbour(Grid[i + 1, j]);
                    if (j + 1 < gridHeight)
                        Grid[i, j].SetSouthNeighbour(Grid[i, j + 1]);
                    if (i - 1 >= 0)
                        Grid[i, j].SetWestNeighbour(Grid[i - 1, j]);
                }
            }
        }

        /// <summary>
        /// Use a world vector that gets normalized to a grid position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool isTileWalkable(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / tileWidth;
            tempY = (int)pos.Y / tileWidth;
            if (Grid[tempX, tempY].iamOccupied)
                return false;
            return true;
        }

        /// <summary>
        /// Use an already normalized grid position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool isTileWalkable(int x, int y)
        {
            if (Grid[x, y].iamOccupied)
                return false;
            return true;
        }

        public Vector2 ReturnTileCenter(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / tileWidth;
            tempY = (int)pos.Y / tileWidth;
            return Grid[tempX, tempY].MyCenter();
        }

        public Tile ReturnTile(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / tileWidth;
            tempY = (int)pos.Y / tileWidth;
            return Grid[tempX, tempY];
        }

        public void SetOccupiedGrid(Tile target)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (Grid[i, j].amIOccupied(target))
                    {
                        Grid[i, j].iamOccupied = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Grid != null)
            {
                for (int i = 0; i < gridWidth; i++)
                {
                    for (int j = 0; j < gridHeight; j++)
                    {
                        Grid[i, j].Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
