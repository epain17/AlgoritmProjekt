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
        public WeaponType weaponState = WeaponType.ShotGun;
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
        Point point;

        float shotInterval = 10;

        List<Projectile> projectiles = new List<Projectile>();

        public Point PlayerPoint
        {
            get { return point = new Point((int)position.X / 32, (int)position.Y / 32); }
        }

        bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public Vector2 PlayerPos
        {
            get { return position; }
            set { position = value; }
        }

        public Player(Texture2D texture, Vector2 position, Texture2D projeTexture, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.projeTexture = projeTexture;
            this.size = size;
        }

        public void Update()
        {
            firingTime();
            HandleWeaponStates();
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space);
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

        void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey, Keys shotKey)
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
                    if (weaponState == WeaponType.ShotGun)
                    {
                        Random rand = new Random();
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                
                    }
                    else
                        projectiles.Add(new Projectile(projeTexture, position, new Vector2(KeyMouseReader.mouseState.Position.X, KeyMouseReader.mouseState.Position.Y)));
                }
            }

        }
    }
}
