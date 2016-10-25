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
    class Player : GameObject
    {
        public enum WeaponType
        {
            Pistol,
            ShotGun,
            MachineGun,
        }
        public WeaponType weaponState = WeaponType.MachineGun;

        public void HandleWeaponStates()
        {
            switch (weaponState)
            {
                case WeaponType.Pistol:
                    shotInterval += 0.5f;
                    break;
                case WeaponType.ShotGun:
                    shotInterval += 0.15f;
                    break;
                case WeaponType.MachineGun:
                    shotInterval += 2;
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

        Texture2D texture, projeTexture;

        public Vector2 pos;
        Vector2 velocity;
        Rectangle hitBox;
        Point point;
        int size;

        float shotInterval = 10;

        List<Projectile> projectiles = new List<Projectile>();

        public Point PlayerPoint
        {
            get { return point = new Point((int)pos.X / 32, (int)pos.Y / 32); }
        }

        bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public Rectangle HitBox
        {
            get { return hitBox = new Rectangle((int)pos.X, (int)pos.Y, size, size); }
        }

        public void HandelCollision(Wall w, int n)
        {
            //Top
            if (n == 1)
            {
                pos.Y = pos.Y + 4;
            }

            //Bottom             
            if (n == 2)
            {
                pos.Y = pos.Y - 4;
            }

            // Left            
            if (n == 3)
            {
                pos.X = pos.X - 4;
            }

            // Right            
            if (n == 4)
            {
                pos.X = pos.X + 4;
            }

        }

        public virtual int Collision(Wall w)
        {
            Rectangle top = w.HitBox;
            top.Height = 10;

            Rectangle bottom = w.HitBox;
            bottom.Height = 5;
            bottom.Y = bottom.Y + w.HitBox.Height - 5;

            Rectangle left = w.HitBox;
            left.Width = 2;
            left.Y = w.HitBox.Y + 10;
            left.Height = w.HitBox.Height - 20;

            Rectangle right = w.HitBox;
            right.X = right.X + right.Width - 2;
            right.Width = 2;
            right.Y = w.HitBox.Y + 10;
            right.Height = w.HitBox.Height - 20;



            if (HitBox.Intersects(left))
            {
                return 3;
            }

            else if (HitBox.Intersects(right))
            {
                return 4;
            }
            if (HitBox.Intersects(top))
            {
                return 1;
            }

            if (HitBox.Intersects(bottom))
            {
                return 2;
            }
            return 0;
        }

        public Player(Texture2D texture, Vector2 position, Texture2D projeTexture)
            : base(texture, position)
        {
            this.texture = texture;
            this.pos = position;
            this.projeTexture = projeTexture;
            size = 32;
        }

        public override void Update()
        {
            firingTime();
            HandleWeaponStates();
            HandlePlayerInteractions(Keys.S, Keys.A, Keys.D, Keys.W, Keys.Space);
            pos += velocity;
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
            spriteBatch.Draw(texture, pos, null, Color.Blue, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);
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
                        projectiles.Add(new Projectile(projeTexture, pos, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, pos, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, pos, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                        projectiles.Add(new Projectile(projeTexture, pos, new Vector2(KeyMouseReader.mouseState.Position.X + rand.Next(-35, 45), KeyMouseReader.mouseState.Position.Y + rand.Next(-15, 15))));
                
                    }
                    else
                        projectiles.Add(new Projectile(projeTexture, pos, new Vector2(KeyMouseReader.mouseState.Position.X, KeyMouseReader.mouseState.Position.Y)));
                }
            }

        }
    }
}
