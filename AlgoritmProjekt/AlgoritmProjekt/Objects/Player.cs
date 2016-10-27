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
        public WeaponType weaponState = WeaponType.MachineGun;
        public float RecoilPower;
        public void HandleWeaponStates()
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
        bool shot = false;

        public bool ShotsFired
        {
            get { return shot; }
            set { shot = value; }
        }

        void firingTime()
        {
            float timer = 0;
            while(ShotsFired)
            {
                timer += 0.1f;
                if (timer > 100)
                    ShotsFired = false;
            }
        }

        Texture2D projeTexture;

        Vector2 velocity;

        float shotInterval = 10;

        List<Projectile> projectiles = new List<Projectile>();

        public bool CheckHit(Enemy enemy)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if(Vector2.Distance(projectiles[i].Position, enemy.myPosition) < 32)
                {
                    projectiles[i].InstaKillMe();
                    enemy.HP--;
                    return true;
                }
            }

            return false;
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
            
            firingTime();
            HandleWeaponStates();
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space, target);
            position += velocity;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Blue, 0, origin, 1, SpriteEffects.None, 1);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey, Keys shotKey, Vector2 target)
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

            if (isKeyDown(shotKey))
            {
                if (shotInterval > 10)
                {
                    shot = true;
                    shotInterval = 0;
                    Random rand = new Random();

                    if (weaponState == WeaponType.ShotGun)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            projectiles.Add(new Projectile(projeTexture, position, new Vector2(target.X + rand.Next(-35, 45), target.Y + rand.Next(-15, 15))));
                        }
                    }
                    else
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(target.X + rand.Next(-15, 15), target.Y + rand.Next(-15, 15))));
                }
            }

        }
    }
}
