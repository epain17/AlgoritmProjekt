using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.PlayerRelated.Actions
{
    class InputMovement
    {
        public enum Moving
        {
            UP,
            LEFT,
            DOWN,
            RIGHT,
        }
        public Moving moving = Moving.RIGHT;

        Keys downKey, leftKey, rightKey, upKey;
        Player player;

        public InputMovement(Keys upKey, Keys leftKey, Keys downKey, Keys rightKey, Player player)
        {
            this.upKey = upKey;
            this.leftKey = leftKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.player = player;
        }

        public bool ReachedDestination(Vector2 targetPos, Player player)
        {
            if (Vector2.Distance(player.myPosition, targetPos) <= 3)
                return true;
            return false;
        }

        public void ChangeDirection(TileGrid grid, ref Vector2 targetPos)
        {
            if (KeyMouseReader.keyState.IsKeyDown(upKey))
            {
                MovePlayerNorth(grid, ref targetPos);
                moving = Moving.UP;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(leftKey))
            {
                MovePlayerWest(grid, ref targetPos);
                moving = Moving.LEFT;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(downKey))
            {
                MovePlayerSouth(grid, ref targetPos);
                moving = Moving.DOWN;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(rightKey))
            {
                MovePlayerEast(grid, ref targetPos);
                moving = Moving.RIGHT;
            }
        }

        public void MoveInFacedDirection(TileGrid grid, ref Vector2 targetPos)
        {
            switch (moving)
            {
                case Moving.UP:
                    MovePlayerNorth(grid, ref targetPos);
                    break;
                case Moving.LEFT:
                    MovePlayerWest(grid, ref targetPos);
                    break;
                case Moving.DOWN:
                    MovePlayerSouth(grid, ref targetPos);
                    break;
                case Moving.RIGHT:
                    MovePlayerEast(grid, ref targetPos);
                    break;
            }
        }

        void MovePlayerNorth(TileGrid grid, ref Vector2 targetPos)
        {
            if (player.myCurrentTile.NorthNeighbour != null && grid.isTileWalkable(player.myCurrentTile.NorthNeighbour.myPosition))
                targetPos = grid.ReturnTileCenter(player.myCurrentTile.NorthNeighbour.myPosition);
        }

        void MovePlayerEast(TileGrid grid, ref Vector2 targetPos)
        {
            if (player.myCurrentTile.EastNeighbour != null && grid.isTileWalkable(player.myCurrentTile.EastNeighbour.myPosition))
                targetPos = grid.ReturnTileCenter(player.myCurrentTile.EastNeighbour.myPosition);
        }

        void MovePlayerSouth(TileGrid grid, ref Vector2 targetPos)
        {
            if (player.myCurrentTile.SouthNeighbour != null && grid.isTileWalkable(player.myCurrentTile.SouthNeighbour.myPosition))
                targetPos = grid.ReturnTileCenter(player.myCurrentTile.SouthNeighbour.myPosition);
        }

        void MovePlayerWest(TileGrid grid, ref Vector2 targetPos)
        {
            if (player.myCurrentTile.WestNeighbour != null && grid.isTileWalkable(player.myCurrentTile.WestNeighbour.myPosition))
                targetPos = grid.ReturnTileCenter(player.myCurrentTile.WestNeighbour.myPosition);
        }
    }
}
