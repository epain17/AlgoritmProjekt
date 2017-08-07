using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.GameObjects.LivingObjects;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Utility.Handle_Levels.PCG;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Characters
{
    class Player : LivingObject
    {
        #region Fields
        public PlayerStates playerStates;
        public InputAbilities attack;
        private InputMovement movement;
        public float energy = 50;
        public int maxEnergy = 50;
        public float invulnerableTimeLimit = 3;
        public float attackInterval = 0.3f;
        public float myStartSpeed;

        Vector2 targetPos;
        public Tile myCurrentTile, myPreviousTile;
        #endregion

        public Player(Vector2 position, int size, int hp, float speed)
            : base(position, size, hp, speed)
        {
            myPosition = position;
            mySize = size;
            myHP = hp;
            mySpeed = speed;
            myStartSpeed = speed;
            playerStates = new PlayerStates(this);
            movement = new InputMovement(Keys.W, Keys.A, Keys.S, Keys.D, this);
            attack = new InputAbilities(Keys.Space, OnAttack);
        }

        public void Update(float time, Level level)
        {
            if (myHP > 0)
            {
                if (myPreviousTile != null)
                {
                    myPreviousTile.texColor = Color.Red;
                    myPreviousTile.iamOccupied = false;
                }

                myCurrentTile = level.GetNavigationMesh().ReturnTile(myPosition);
                myCurrentTile.texColor = Color.Blue;
                myCurrentTile.iamOccupied = true;
                myPreviousTile = myCurrentTile;

                attack.Execute(playerStates);
                playerStates.HandlePlayerStates(time);

                movement.ChangeDirection(level.GetNavigationMesh(), ref targetPos, myPosition);
                SetDirection(targetPos);
                if (movement.ReachedDestination(targetPos, this))
                    StopMoving();
                base.Update(time);
            }
        }

        /// <summary>
        /// Used when changing level to reset the target position to the player's new position in the new grid.
        /// </summary>
        /// <param name="grid">Reference to a tile grid.</param>
        public void ResetMovement(TileGrid grid, Vector2 newPos)
        {
            myPosition = newPos;
            targetPos = grid.ReturnTileCenter(myPosition);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (myHP > 0)
                base.Draw(spriteBatch);
        }

        public void HPChange(int change)
        {
            myHP += change;
        }

        public void OnAttack()
        {
            // The decimal value can be used for heavier/lighter weapons
            energy -= maxEnergy * 0.1f;
            playerStates.CurrentStatus = PlayerStates.Status.Attack;
        }
    }
}
