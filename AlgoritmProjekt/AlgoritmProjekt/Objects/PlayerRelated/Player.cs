using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects;
using AlgoritmProjekt.Objects.Companion;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Utility;
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
    class Player : MovingTile
    {
        #region Fields
        public PlayerStates playerStates;
        public InputAbilities abilities;
        private InputMovement movement;
        float colorAlpha = 1;
        float energyMeter = 50;

        public float EnergyMeter
        {
            get { return energyMeter; }
        }

        #endregion

        public Player(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            myHP = 3;
            speed = 160;

            int maxEnergy = 50;
            int invulnerableTimeLimit = 3;
            movement = new InputMovement(Keys.W, Keys.A, Keys.S, Keys.D, size);
            playerStates = new PlayerStates(PlayerStates.Status.Normal, maxEnergy, invulnerableTimeLimit, speed);
            abilities = new InputAbilities(Keys.LeftControl);
        }

        public void Update(ref float time, TileGrid grid)
        {
            if (myHP > 0)
            {
                playerStates.Update(ref time, ref energyMeter, ref colorAlpha, ref speed);
                abilities.Update(playerStates);

                movement.Update(grid, ref targetPos, ref position);
                SetDirection(targetPos);
                if (movement.ReachedDestination(targetPos, position))
                    StopMoving();
                base.Update(ref time);
            }
        }

        /// <summary>
        /// Used when changing level to reset the target position to the player's new position in the new grid.
        /// </summary>
        /// <param name="grid">Reference to a tile grid.</param>
        public void ResetMovement(TileGrid grid, Vector2 newPos)
        {
            position = newPos;
            targetPos = grid.ReturnTilePosition(position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (myHP > 0)
                spriteBatch.Draw(myTexture, position, null, Color.LimeGreen * colorAlpha, 0, origin, 1, SpriteEffects.None, 1);
        }

        public void HPUp(int increase)
        {
            myHP += increase;
        }
    }
}
