using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.Environment;
using AlgoritmProjekt.Objects.GameObjects.StaticObjects.PickUps;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Utility.Handle_Levels.PCG;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.GameObjects.StaticObjects.Environment
{
    class Room
    {
        public enum PlacementType
        {
            Walls,
            Floor,
            Corridor,
        }
        public PlacementType myType;

        public int size,
            x,
            y,
            roomWidth,
            roomHeight;
        TileGrid grid;

        public bool isConnected = false;

        public Vector2 RoomCenterPosition()
        {
            Tile temp = grid.ReturnTile(x + (roomWidth / 2), y + (roomHeight / 2));
            return temp.MyCenter();
        }

        public Room(PlacementType myType, TileGrid grid, int x, int y, int roomWidth, int roomHeight)
        {
            this.myType = myType;
            size = roomWidth * roomHeight;
            this.x = x;
            this.y = y;
            this.roomWidth = roomWidth;
            this.roomHeight = roomHeight;
            this.grid = grid;

            switch (myType)
            {
                case PlacementType.Floor:
                    for (int i = 1; i < roomWidth - 1; i++)
                    {
                        for (int j = 1; j < roomHeight - 1; j++)
                        {
                            SetFloor(grid, x, y, i, j);
                        }
                    }
                    break;

                case PlacementType.Corridor:
                    for (int i = 0; i < roomWidth; i++)
                    {
                        for (int j = 0; j < roomHeight; j++)
                        {
                            SetCorridor(grid, x, y, i, j);
                        }
                    }
                    break;

                case PlacementType.Walls:
                    for (int i = 0; i < roomWidth; i++)
                    {
                        for (int j = 0; j < roomHeight; j++)
                        {
                            //SetWalls(grid, x, y, roomWidth, roomHeight, i, j);
                        }
                    }
                    break;
            }
        }

        private void SetCorridor(TileGrid grid, int x, int y, int i, int j)
        {
            while (grid.ReturnTile(x + i, y + j).myType != Tile.TileType.START || grid.ReturnTile(x + i, y + j).myType != Tile.TileType.FLOOR)
            {
                grid.ReturnTile(x + i, y + j).myType = Tile.TileType.START;
                if (i + 1 < roomWidth && j + 1 < roomHeight)
                    if (i > j)
                        i++;
                    else
                        j++;
                else
                    break;
            }
        }

        private void SetFloor(TileGrid grid, int x, int y, int i, int j)
        {
            if (myType == PlacementType.Floor)
                grid.ReturnTile(x + i, y + j).myType = Tile.TileType.FLOOR;
        }

        private void SetWalls(TileGrid grid, int x, int y, int roomWidth, int roomHeight, int i, int j)
        {
            if (i == 0 && grid.ReturnTile(x, y + j).myType == Tile.TileType.DEFAULT)
            {
                grid.ReturnTile(x, y + j).myType = Tile.TileType.WALL;
            }

            if (j == 0 && grid.ReturnTile(x + i, y).myType == Tile.TileType.DEFAULT)
            {
                grid.ReturnTile(x + i, y).myType = Tile.TileType.WALL;
            }

            if (i <= roomWidth && grid.ReturnTile(x + roomWidth, y + j).myType == Tile.TileType.DEFAULT)
            {
                grid.ReturnTile(x + roomWidth, y + j).myType = Tile.TileType.WALL;
            }

            if (j <= roomHeight && grid.ReturnTile(x + i, y + roomHeight).myType == Tile.TileType.DEFAULT)
            {
                grid.ReturnTile(x + i, y + roomHeight).myType = Tile.TileType.WALL;
            }
        }

        public void SetStartRoom()
        {
            myType = PlacementType.Walls;
            grid.ReturnTile(x + (roomWidth / 2), y + (roomHeight / 2)).myType = Tile.TileType.START;
        }


    }
}
