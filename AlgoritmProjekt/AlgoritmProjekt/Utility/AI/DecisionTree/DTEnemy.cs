using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Utility.AI.DecisionTree.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    class DTEnemy : Enemy
    {
        List<Projectile> projectiles = new List<Projectile>();
        float safetyRange, attackRange;
        Texture2D texBullet;
        Player player;
        Random rand;
        DTree tree;

        Point initialPoint;

        public Point myStartPoint
        {
            get { return initialPoint; }
        }

        public DTState CurrentState;

        public bool timeToShoot;

        public DTEnemy(Texture2D texture, Vector2 position, int size, Texture2D texBullet, Player player)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.texBullet = texBullet;
            this.player = player;
            this.startPoint = new Point((int)position.X / size, (int)position.Y / size);
            initialPoint = startPoint;
            hp = 15;
            rand = new Random();
            startHp = hp;
            speed = 80;
            aggroRange = size * 8;
            attackRange = aggroRange * 0.5f;
            safetyRange = size * 10;
            timeToShoot = false;

            CurrentState = new DTState(this);
            tree = new DTree(this);
        }

        public void Update(float time, Player player, TileGrid grid)
        {
            tree.Execute();
            CurrentState.UpdatePerception(player, grid, time);
            Shoot(position, player.myPosition);
            UpdateProjectiles(time);
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spriteBatch);
            }
            foreach (Vector2 way in waypoints)
            {
                spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 0);
            }
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        }

        private void UpdateProjectiles(float time)
        {
            if (projectiles.Count > 0)
            {
                foreach (Projectile proj in projectiles)
                    proj.Update(ref time);
                for (int i = projectiles.Count - 1; i >= 0; --i)
                {
                    if (!projectiles[i].iamAlive)
                        projectiles.RemoveAt(i);
                }
            }
        }

        public void Shoot(Vector2 bulletStartPos, Vector2 target)
        {
            if (timeToShoot)
            {
                projectiles.Add(new FireBullet(texBullet, bulletStartPos, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3)), 125, 6));
                timeToShoot = false;
            }
        }

        public bool ChooseOffensiveStance()
        {
            if (Vector2.Distance(player.myPosition, position) < (attackRange))
            {
                //Console.WriteLine("Attacking");
                return true;
            }
            //Console.WriteLine("Chasing");
            return false;
        }

        public bool ChooseDefensiveStance()
        {
            if (hp < 10 && Vector2.Distance(player.myPosition, position) < safetyRange && myPoint != initialPoint)
            {
                //Console.WriteLine("Escaping");
                speed = 120;
                return true;
            }
            //Console.WriteLine("Recovering");
            return false;
        }

        public bool ChooseCombatStance()
        {
            return hp > (startHp / 2) ? true : false;
        }
    }
}

