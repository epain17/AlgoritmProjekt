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
        Texture2D tileTex;
        Tile[,] tileGrid;
        int size;

        public TileGrid(Texture2D tileTex, int size, int columns, int rows)
        {
            this.tileTex = tileTex;
            this.size = size;
            width = columns;
            height = rows;

            CreateTileGrid();
        }

        public void CreateTileGrid()
        {
            tileGrid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j] = new Tile(tileTex, new Vector2((size / 2) + i * size, (size / 2) + j * size), size);
                }
            }
        }

        public bool WalkableFromVect(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tileGrid[tempX, tempY].iamOccupied)
                return false;
            return true;
        }

        public bool WalkableFromPoint(int x, int y)
        {
            if (tileGrid[x, y].iamOccupied)
                return false;
            return true;
        }

        public Vector2 ReturnTilePosition(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tileGrid[tempX, tempY] != null)
                return tileGrid[tempX, tempY].myPosition;
            return Vector2.Zero;
        }

        public void SetOccupiedGrid(Tile target)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tileGrid[i, j].amIOccupied(target))
                        tileGrid[i, j].iamOccupied = true;
                }
            }
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
        }
    }
}
