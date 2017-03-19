using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Interfaces
{
    class IShoot
    {
        public List<Projectile> projectiles = new List<Projectile>();
        WeaponStates weaponStates;
        Texture2D texBullet;
        Random rand;
        float shotInterval = 0;
        Tile agent;
        int size;

        public IShoot(Tile agent, Texture2D texBullet)
        {
            this.agent = agent;
            this.texBullet = texBullet;
            rand = new Random();
            size = Constants.tileSize;
            weaponStates = new WeaponStates();
        }

        public void Update(float time, Vector2 target)
        {
            weaponStates.Update(time, ref shotInterval);

            foreach (Projectile proj in projectiles)
                proj.Update(ref time);
            for (int i = projectiles.Count - 1; i >= 0; --i)
            {
                if (!projectiles[i].iamAlive)
                    projectiles.RemoveAt(i);
            }
            Shoot(target);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spriteBatch);
            }
        }

        public void Shoot(Vector2 target)
        {
            if (agent.LetMeShoot)
            {
                switch (weaponStates.type)
                {
                    case WeaponStates.WeaponType.Pistol:
                        projectiles.Add(new FireBullet(texBullet, agent.myPosition, size, new Vector2(target.X + rand.Next(-10, 10), target.Y + rand.Next(-10, 10)), 170, 6));
                        break;
                    case WeaponStates.WeaponType.MachineGun:
                        projectiles.Add(new FireBullet(texBullet, agent.myPosition, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3)), 180, 7));
                        break;
                    case WeaponStates.WeaponType.ShotGun:
                        for (int i = 0; i < 4; i++)
                            projectiles.Add(new FireBullet(texBullet, agent.myPosition, size, new Vector2(target.X + rand.Next(-20, 20), target.Y + rand.Next(-20, 20)), 150, 5));
                        break;
                }
                agent.LetMeShoot = false;
            }
        }
    }
}
