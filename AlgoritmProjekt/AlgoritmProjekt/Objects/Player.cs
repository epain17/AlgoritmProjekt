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

        public float RecoilPower;
        float shotInterval = 0;
        bool shot = false;
        Vector2 velocity;

        //public override bool CheckMyCollision(Tile enemy)
        //{
        //    for (int i = 0; i < projectiles.Count; i++)
        //    {
        //        if (Vector2.Distance(projectiles[i].Position, enemy.myPosition) < (size))
        //        {
        //            projectiles[i].InstaKillMe();
        //            return true;
        //        }
        //    }

        //    return false;
        //}

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
        }

        public void Update(Vector2 target)
        {
            firingTimeFrame();
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space, target);
            HandleWeaponStates();
            position += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Blue, 0, origin, 1, SpriteEffects.None, 1);
        }

        private void firingTimeFrame()
        {
            float timer = 0;
            while (shot)
            {
                timer += 0.1f;
                if (timer > 100)
                    shot = false;
            }
        }

        private void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey, Keys shotKey, Vector2 target)
        {
            Moving(downKey, leftKey, rightKey, upKey);
            Shooting(shotKey, target);
        }

        private void Moving(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey)
        {
            velocity = Vector2.Zero;

            if (isKeyDown(leftKey))
            {
                velocity.X = -3;
            }

            if (isKeyDown(rightKey))
            {
                velocity.X = 3;
            }

            if (isKeyDown(downKey))
            {
                velocity.Y = 3;
            }

            if (isKeyDown(upKey))
            {
                velocity.Y = -3;
            }
        }

        private void Shooting(Keys shotKey, Vector2 target)
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
                    shotInterval += 2;
                    RecoilPower = 10;
                    break;
            }
        }
    }
}
