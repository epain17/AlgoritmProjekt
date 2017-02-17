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
        public WeaponStates weaponStates;
        private InputAbilities abilities;
        private InputMovement movement;
        List<Projectile> projectiles = new List<Projectile>();
        Texture2D smallHollowSquare, hollowSquare;
        CrossHair xhair;

        //public AICompanion companion;

        float colorAlpha = 1;
        float shotInterval = 0;

        float energyMeter = 50;

        public float EnergyMeter
        {
            get { return energyMeter; }
        }

        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }
        #endregion

        public Player(Texture2D texture, Texture2D hollowSquare, Texture2D smallHollowSquare, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.hollowSquare = hollowSquare;
            this.position = position;
            this.size = size;
            this.smallHollowSquare = smallHollowSquare;
            this.myHP = 3;
            this.speed = 160;

            int maxEnergy = 50;
            int invulnerableTimeLimit = 3;
            movement = new InputMovement(Keys.W, Keys.A, Keys.S, Keys.D, size);
            playerStates = new PlayerStates(PlayerStates.Status.Normal, maxEnergy, invulnerableTimeLimit, speed);
            weaponStates = new WeaponStates(WeaponStates.WeaponType.None);
            abilities = new InputAbilities(Keys.Space, Keys.LeftControl, Keys.LeftShift);
            xhair = new CrossHair(hollowSquare, smallHollowSquare, position, size);

            //companion = new AICompanion(hollowSquare, position, size);
        }

        public void Update(ref float time, TileGrid grid)
        {
            //companion.Update(ref time, this);
            if (myHP > 0)
            {
                playerStates.Update(ref time, ref energyMeter, ref colorAlpha, ref speed);
                abilities.Update(playerStates, ref shotInterval);
                weaponStates.Update(time, ref shotInterval);

                if (abilities.ShotsFired)
                {
                    Shoot(position, xhair.myPosition);
                    abilities.ShotsFired = false;
                }

                UpdateProjectiles(time);
                movement.Update(grid, ref xhair, ref targetPos, ref position, abilities.Aim);
                SetDirection(targetPos);

                if (movement.ReachedDestination(targetPos, position))
                    StopMoving();
                base.Update(ref time);
            }
        }

        public void UpdateAI(Tile target)
        {
                //Shoot(companion.myPosition, target.myPosition);
                //companion.TimeToShoot = false;
        }

        /// <summary>
        /// Used when changing level to reset the target position to the player's new position in the new grid.
        /// </summary>
        /// <param name="grid">Reference to a tile grid.</param>
        public void ResetMovement(TileGrid grid)
        {
            targetPos = grid.ReturnTilePosition(position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spriteBatch);
            }
            if (abilities.Aim)
                xhair.Draw(spriteBatch);
            if (myHP > 0)
                spriteBatch.Draw(myTexture, position, null, Color.LimeGreen * colorAlpha, 0, origin, 1, SpriteEffects.None, 1);
            //companion.Draw(spriteBatch);
        }

        private void UpdateProjectiles(float time)
        {
            if (projectiles.Count > 0)
            {
                for (int i = projectiles.Count - 1; i >= 0; --i)
                {
                    if (!projectiles[i].iamAlive)
                        projectiles.RemoveAt(i);
                }
                foreach (Projectile proj in projectiles)
                {
                    proj.Update(ref time);
                }
            }
        }

        public void Shoot(Vector2 bulletStartPos, Vector2 target)
        {
            Random rand = new Random();
            switch (weaponStates.type)
            {
                case WeaponStates.WeaponType.Pistol:
                    projectiles.Add(new Projectile(smallHollowSquare, bulletStartPos, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3))));
                    break;
                case WeaponStates.WeaponType.ShotGun:
                    for (int i = 0; i < 4; i++)
                        projectiles.Add(new Projectile(smallHollowSquare, bulletStartPos, size, new Vector2(target.X + rand.Next(-20, 20), target.Y + rand.Next(-20, 20))));
                    break;
                case WeaponStates.WeaponType.MachineGun:
                    projectiles.Add(new Projectile(smallHollowSquare, bulletStartPos, size, new Vector2(target.X + rand.Next(-10, 10), target.Y + rand.Next(-10, 10))));
                    break;
            }
        }
    }
}
