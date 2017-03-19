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
        enum Moving
        {
            still,
            up,
            left,
            down,
            right,
        }
        Moving moving = Moving.still;

        Keys downKey, leftKey, rightKey, upKey;
        int tileSize;
        Vector2 tempVector;

        private bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
                return true;
            return false;
        }

        public InputMovement(Keys upKey, Keys leftKey, Keys downKey, Keys rightKey, int tileSize)
        {
            this.upKey = upKey;
            this.leftKey = leftKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.tileSize = tileSize;
        }

        public void Update(TileGrid grid, ref Vector2 targetPos, ref Vector2 playerPos)
        {
            ChangeDirection(grid, ref targetPos, playerPos);
        }

        public bool ReachedDestination(Vector2 targetPos, Vector2 playerPos)
        {
            if (Vector2.Distance(playerPos, targetPos) <= 2)
            {
                moving = Moving.still;
                return true;
            }
            return false;
        }

        private void ChangeDirection(TileGrid grid, ref Vector2 targetPos, Vector2 playerPos)
        {
            if (moving == Moving.still)
            {
                if (isKeyDown(upKey))
                {
                    MovePlayerNorth(grid, ref targetPos, playerPos);
                    moving = Moving.up;
                }
                else if (isKeyDown(leftKey))
                {
                    MovePlayerWest(grid, ref targetPos, playerPos);
                    moving = Moving.left;
                }
                else if (isKeyDown(downKey))
                {
                    MovePlayerSouth(grid, ref targetPos, playerPos);
                    moving = Moving.down;
                }
                else if (isKeyDown(rightKey))
                {
                    MovePlayerEast(grid, ref targetPos, playerPos);
                    moving = Moving.right;
                }
            }
        }

        void MovePlayerNorth(TileGrid grid, ref Vector2 targetPos, Vector2 position)
        {
            tempVector = new Vector2(position.X, position.Y - (tileSize - 1));
            if (grid.WalkableFromVect(tempVector))
                targetPos = grid.ReturnTilePosition(tempVector);
        }

        void MovePlayerWest(TileGrid grid, ref Vector2 targetPos, Vector2 position)
        {
            tempVector = new Vector2(position.X - (tileSize - 1), position.Y);
            if (grid.WalkableFromVect(tempVector))
                targetPos = grid.ReturnTilePosition(tempVector);
        }

        void MovePlayerSouth(TileGrid grid, ref Vector2 targetPos, Vector2 position)
        {
            tempVector = new Vector2(position.X, position.Y + (tileSize - 1));
            if (grid.WalkableFromVect(tempVector))
                targetPos = grid.ReturnTilePosition(tempVector);
        }

        void MovePlayerEast(TileGrid grid, ref Vector2 targetPos, Vector2 position)
        {
            tempVector = new Vector2(position.X + (tileSize - 1), position.Y);
            if (grid.WalkableFromVect(tempVector))
                targetPos = grid.ReturnTilePosition(tempVector);
        }
    }
}
