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

        public void Update(TileGrid grid, ref CrossHair xhair, ref Vector2 targetPos, ref Vector2 playerPos, bool aiming)
        {
            ReachedDestination(targetPos, playerPos);
            ChangeDirection(grid, ref targetPos, playerPos, ref xhair, aiming);
            MoveXhair(playerPos, xhair);
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

        void CheckMoveState(Vector2 playerPos, CrossHair xhair)
        {
            switch (moving)
            {
                case Moving.up:
                    xhair.myPosition = new Vector2(playerPos.X, playerPos.Y - (tileSize * 3));
                    break;
                case Moving.left:
                    xhair.myPosition = new Vector2(playerPos.X - (tileSize * 3), playerPos.Y);
                    break;
                case Moving.down:
                    xhair.myPosition = new Vector2(playerPos.X, playerPos.Y + (tileSize * 3));
                    break;
                case Moving.right:
                    xhair.myPosition = new Vector2(playerPos.X + (tileSize * 3), playerPos.Y);
                    break;
            }
        }

        void MoveXhair(Vector2 playerPos, CrossHair xhair)
        {
            CheckMoveState(playerPos, xhair);
            if (isKeyDown(upKey))
            {
                xhair.myPosition = new Vector2(playerPos.X, playerPos.Y - (tileSize * 3));
            }
            if (isKeyDown(leftKey))
            {
                xhair.myPosition = new Vector2(playerPos.X - (tileSize * 3), playerPos.Y);
            }
            if (isKeyDown(downKey))
            {
                xhair.myPosition = new Vector2(playerPos.X, playerPos.Y + (tileSize * 3));
            }
            if (isKeyDown(rightKey))
            {
                xhair.myPosition = new Vector2(playerPos.X + (tileSize * 3), playerPos.Y);
            }
        }

        private void ChangeDirection(TileGrid grid, ref Vector2 targetPos, Vector2 playerPos, ref CrossHair xhair, bool aiming)
        {
            if (moving == Moving.still && !aiming)
            {
                if (isKeyDown(upKey))
                {
                    MovePlayerNorth(grid, ref targetPos, playerPos);
                    moving = Moving.up;
                }
                if (isKeyDown(leftKey))
                {
                    MovePlayerWest(grid, ref targetPos, playerPos);
                    moving = Moving.left;
                }
                if (isKeyDown(downKey))
                {
                    MovePlayerSouth(grid, ref targetPos, playerPos);
                    moving = Moving.down;
                }
                if (isKeyDown(rightKey))
                {
                    MovePlayerEast(grid, ref targetPos, playerPos);
                    moving = Moving.right;
                }
            }
            if (moving != Moving.still && aiming)
            {
                switch(moving)
                {
                    case Moving.up:
                        MovePlayerNorth(grid, ref targetPos, playerPos);
                        break;
                    case Moving.left:
                        MovePlayerWest(grid, ref targetPos, playerPos);
                        break;
                    case Moving.down:
                        MovePlayerSouth(grid, ref targetPos, playerPos);
                        break;
                    case Moving.right:
                        MovePlayerEast(grid, ref targetPos, playerPos);
                        break;
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
