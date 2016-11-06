using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects.Projectiles;
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
    class Player : Tile
    {
        public enum WeaponType
        {
            Pistol,
            ShotGun,
            MachineGun,
        }
        public WeaponType weaponState = WeaponType.Pistol;

        public enum PlayerState
        {
            Normal,
            Invulnerable,
            Power,
        }
        public PlayerState playerState = PlayerState.Normal;

        float invuleranbleTimer = 0;
        float colorAlpha = 1;
        bool fade = true;

        public float RecoilPower;
        float shotInterval = 0;
        bool shot = false;

        float energyMeter = 50, maxEnergy = 50;

        public bool ShotsFired
        {
            get { return shot; }
            set { shot = value; }
        }

        public float EnergyMeter
        {
            get { return energyMeter; }
        }

        public Player(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.myHP = 3;
        }

        public override void Update(ref float time)
        {
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space, Keys.LeftControl);
            HandleWeaponStates(time);
            HandlePlayerStates(ref time);
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.LimeGreen * colorAlpha, 0, origin, 1, SpriteEffects.None, 1);
        }

        private void HandlePlayerStates(ref float time)
        {
            switch (playerState)
            {
                case PlayerState.Normal:
                    if (energyMeter < maxEnergy)
                        energyMeter += 0.15f;
                    break;
                case PlayerState.Invulnerable:
                    InvulnerableState(time);
                    break;
                case PlayerState.Power:
                    PowerState(ref time);
                    break;
            }
        }

        protected virtual void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey, Keys shotKey, Keys powerKey)
        {
            Moving(downKey, leftKey, rightKey, upKey);
            Shooting(shotKey);
            Powering(powerKey);
        }

        private void InvulnerableState(float time)
        {
            invuleranbleTimer += time;
            if(fade)
            {
                colorAlpha -= 0.1f;
                if (colorAlpha < 0.1f)
                    fade = false;
            }
            else
            {
                colorAlpha += 0.1f;
                if (colorAlpha > 0.9f)
                    fade = true;
            }

            if(invuleranbleTimer > 3)
            {
                playerState = PlayerState.Normal;
                invuleranbleTimer = 0;
                colorAlpha = 1;
                fade = true;
            }
        }

        private void PowerState(ref float time)
        {
            if (playerState == PlayerState.Power && energyMeter > 0)
            {
                energyMeter -= 0.15f;
                time *= 0.35f;
            }
        }

        private void Moving(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey)
        {
            velocity = Vector2.Zero;

            if (isKeyDown(leftKey))
            {
                velocity.X = -160;
            }

            else if (isKeyDown(rightKey))
            {
                velocity.X = 160;
            }

            else if (isKeyDown(downKey))
            {
                velocity.Y = 160;
            }

            else if (isKeyDown(upKey))
            {
                velocity.Y = -160;
            }
        }

        private void Shooting(Keys shotKey)
        {
            if (isKeyDown(shotKey))
            {
                if (shotInterval > 0.5f)
                {
                    shot = true;
                    shotInterval = 0;
                }
            }
        }

        private void Powering(Keys powerKey)
        {
            if (isKeyDown(powerKey))
                playerState = PlayerState.Power;
            else if(playerState != PlayerState.Invulnerable)
                playerState = PlayerState.Normal;
        }

        private bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        private void HandleWeaponStates(float time)
        {
            switch (weaponState)
            {
                case WeaponType.Pistol:
                    shotInterval += time;
                    RecoilPower = 5f;
                    break;
                case WeaponType.ShotGun:
                    shotInterval += time * 0.5f;
                    RecoilPower = 12;
                    break;
                case WeaponType.MachineGun:
                    shotInterval += time * 2.5f;
                    RecoilPower = 8;
                    break;
            }
        }
    }
}
