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
        List<Projectile> projectiles = new List<Projectile>();
        Texture2D texBullet;
        Random rand;
        Tile agent;
        int size;

        public IShoot(Tile agent, Texture2D texBullet)
        {
            this.agent = agent;
            this.texBullet = texBullet;
            rand = new Random();
            size = Constants.tileSize;
        }

        public void Update(float time, Vector2 target)
        {
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
                projectiles.Add(new FireBullet(texBullet, agent.myPosition, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3)), 125, 6));
                agent.LetMeShoot = false;
            }
        }
    }
}
