using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Objects.GameObjects;
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
        public int 
            gridWidth,
            gridHeight,
            tileWidth, 
            tileHeight;

        public Tile[,] 
            Tiles;

        public TileGrid(int tileWidth, int tileHeight, int columns, int rows)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            gridWidth = columns;
            gridHeight = rows;

            Tiles = CreateTileGrid(gridWidth, gridHeight, tileWidth, tileHeight);
        }

        private Tile[,] CreateTileGrid(int columns, int rows, int width, int height)
        {
            Tile[,] temp = new Tile[columns, rows];

            // Create Tiles
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    temp[i, j] = new Tile(new Vector2(i * width, j * height), width, height);
                }
            }

            // Set Neighbouring Tiles
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (j - 1 >= 0)
                        temp[i, j].SetNorthNeighbour(temp[i, j - 1]);
                    if (i + 1 < gridWidth)
                        temp[i, j].SetEastNeighbour(temp[i + 1, j]);
                    if (j + 1 < gridHeight)
                        temp[i, j].SetSouthNeighbour(temp[i, j + 1]);
                    if (i - 1 >= 0)
                        temp[i, j].SetWestNeighbour(temp[i - 1, j]);
                }
            }

            return temp;
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
            if (Tiles[tempX, tempY].iamOccupied)
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
            if (Tiles[x, y].iamOccupied)
                return false;
            return true;
        }

        public Vector2 ReturnTileCenter(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / tileWidth;
            tempY = (int)pos.Y / tileWidth;
            return Tiles[tempX, tempY].MyCenter();
        }

        public Vector2 ReturnTileCenter(int x, int y)
        {
            return Tiles[x, y].MyCenter();
        }

        public Tile ReturnTile(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / tileWidth;
            tempY = (int)pos.Y / tileWidth;
            return Tiles[tempX, tempY];
        }

        public Tile ReturnTile(int x, int y)
        {
            return Tiles[x, y];
        }

        public void SetOccupiedGrid(GameObject target)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (Tiles[i, j].amIOccupied(target))
                    {
                        Tiles[i, j].iamOccupied = true;
                    }
                }
            }
        }


        public void SetOccupiedGrid(Tile target)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (target.amIOccupied(Tiles[i, j]))
                    {
                        Tiles[i, j].iamOccupied = true;
                    } 
                }
            }
        }

        public void SetOccupiedWalls()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (Tiles[i, j].myType == Tile.TileType.WALL)
                    {
                        Tiles[i, j].iamOccupied = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Tiles != null)
            {
                for (int i = 0; i < gridWidth; i++)
                {
                    for (int j = 0; j < gridHeight; j++)
                    {
                        Tiles[i, j].Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
