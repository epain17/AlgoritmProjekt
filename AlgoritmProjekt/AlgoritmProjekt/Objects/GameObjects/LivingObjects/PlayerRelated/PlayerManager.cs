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
        public float Energy = 50;
        public int maxEnergy = 50;
        public float invulnerableTimeLimit = 3;
        public float attackInterval;

        private InputMovement movement;
        private Vector2 targetPos;
        private float
            speedMultiplier,
            energyLoss,
            weaponWeight;
        #endregion

        public Player(Vector2 position, int width, int height, int hp, float speed)
            : base(position, width, height, hp, speed)
        {
            playerStates = new PlayerStates(this);
            movement = new InputMovement(Keys.W, Keys.A, Keys.S, Keys.D, this);
            attack = new InputAbilities(OnAttack, AttackRequirement);
            speedMultiplier = 1.5f;

            // value can be used for heavier/lighter weapons to determine attack interval and energyloss
            weaponWeight = 0.1f;

            attackInterval = weaponWeight;
            energyLoss = maxEnergy * weaponWeight;
        }

        public override void Update(float time, TileGrid navigationGrid)
        {
            if (myHP > 0)
            {
                base.Update(time, navigationGrid);
                movement.ChangeDirection(navigationGrid, ref targetPos);

                //if (attack.Execute(playerStates))
                //    movement.MoveInFacedDirection(navigationGrid, ref targetPos);
                SetDirection(targetPos);

                if (movement.ReachedDestination(targetPos, this))
                {
                    myDirection = Vector2.Zero;
                    myPosition = targetPos;
                }

                playerStates.HandlePlayerStates(time);
            }
        }

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
            // Use attackinterval to determine attack rate

            Energy -= energyLoss;
            playerStates.CurrentStatus = PlayerStates.Status.Attack;
        }

        public bool AttackRequirement()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) &&
                KeyMouseReader.oldKeyState.IsKeyUp(Keys.Space) &&
                playerStates.CurrentStatus == PlayerStates.Status.Normal &&
                Energy > energyLoss)
                return true;
            return false;
        }

        public void OnIdle()
        {
            if (mySpeed != myStartSpeed)
                mySpeed = myStartSpeed;

            // Decimal can be used to determine the rate of energy gain
            if (Energy < maxEnergy)
                Energy += 0.3f;
        }

        public void WhenDamaged(int damageAmount)
        {
            HPChange(damageAmount);
            mySpeed *= speedMultiplier;
        }
    }
}
