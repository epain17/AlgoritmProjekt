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

        public float RecoilPower;
        float shotInterval = 0;
        bool shot = false;        

        public bool ShotsFired
        {
            get { return shot; }
            set { shot = value; }
        }

        bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public Player(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.myHP = 3;
        }

        public void Update(Vector2 target)
        {
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space);
            HandleWeaponStates();
            position += velocity;

            switch (playerState)
            {
                case PlayerState.Normal:
                    break;
                case PlayerState.Invulnerable:
                    InvulnerableState();
                    break;
                case PlayerState.Power:
                    break;
            }
        }

        void InvulnerableState()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.LimeGreen, 0, origin, 1, SpriteEffects.None, 1);
        }

        protected virtual void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey, Keys shotKey)
        {
            Moving(downKey, leftKey, rightKey, upKey);
            Shooting(shotKey);
        }

        private void Moving(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey)
        {
            velocity = Vector2.Zero;

            if (isKeyDown(leftKey))
            {
                velocity.X = -3;
            }

            else if (isKeyDown(rightKey))
            {
                velocity.X = 3;
            }

            else if (isKeyDown(downKey))
            {
                velocity.Y = 3;
            }

            else if (isKeyDown(upKey))
            {
                velocity.Y = -3;
            }
        }

        private void Shooting(Keys shotKey)
        {
            if (isKeyDown(shotKey))
            {
                if (shotInterval > 10)
                {
                    shot = true;
                    shotInterval = 0;
                }
            }
        }

        private void HandleWeaponStates()
        {
            switch (weaponState)
            {
                case WeaponType.Pistol:
                    shotInterval += 0.5f;
                    RecoilPower = 5f;
                    break;
                case WeaponType.ShotGun:
                    shotInterval += 0.25f;
                    RecoilPower = 20;
                    break;
                case WeaponType.MachineGun:
                    shotInterval += 1.5f;
                    RecoilPower = 10;
                    break;
            }
        }
    }
}
