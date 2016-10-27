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

        List<Projectile> projectiles = new List<Projectile>();
        public float RecoilPower;
        Texture2D projeTexture;
        float shotInterval = 0;
        bool shot = false;


        Vector2 velocity;


        public bool CheckHit(Tile enemy)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if(Vector2.Distance(projectiles[i].Position, enemy.myPosition) < (size))
                {
                    projectiles[i].InstaKillMe();
                    return true;
                }
            }

            return false;
        }

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

        public Player(Texture2D texture, Vector2 position, Texture2D projeTexture, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.projeTexture = projeTexture;
            this.size = size;
        }

        public void Update(Vector2 target)
        {
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space, target);
            HandleWeaponStates();
            firingTimeFrame();
            HandleProjectiles();
            position += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Blue, 0, origin, 1, SpriteEffects.None, 1);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        private void firingTimeFrame()
        {
            float timer = 0;
            while (ShotsFired)
            {
                timer += 0.1f;
                if (timer > 100)
                    ShotsFired = false;
            }
        }

        private void HandleProjectiles()
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

            for (int i = projectiles.Count - 1; i >= 0; --i)
            {
                if (projectiles[i].DeadShot)
                    projectiles.RemoveAt(i);
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
                    Random rand = new Random();

                    if (weaponState == WeaponType.ShotGun)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            projectiles.Add(new Projectile(projeTexture, position, new Vector2(target.X + rand.Next(-25, 25), target.Y + rand.Next(-25, 25))));
                        }
                    }
                    else
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(target.X + rand.Next(-15, 15), target.Y + rand.Next(-15, 15))));
                }
            }
        }

        private void HandleWeaponStates()
        {
            switch (weaponState)
            {
                case WeaponType.Pistol:
                    shotInterval += 0.5f;
                    RecoilPower = 2.5f;
                    break;
                case WeaponType.ShotGun:
                    shotInterval += 0.25f;
                    RecoilPower = 10;
                    break;
                case WeaponType.MachineGun:
                    shotInterval += 2;
                    RecoilPower = 5;
                    break;
            }
        }
    }
}
