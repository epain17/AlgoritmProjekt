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
        DTree tree;
        DTState currentState, 
            attackState, chaseState, recoverState, escapeState;
        float safetyRange;
        Random rand;
        List<Projectile> projectiles = new List<Projectile>();
        Texture2D texBullet;

        public bool timeToShoot;

        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }

        public float AggroRange()
        {
            return size * 8;
        }

        public float SafetyRange()
        {
            return size * 10;
        }

        bool Aggressive()
        {
            if (hp < 7)
                return false;
            return true;
        }

        bool AttackPlayer(Player player)
        {
            if (Vector2.Distance(player.myPosition, position) < (aggroRange * 0.5f))
                return true;
            return false;
        }

        bool EscapePlayer(Player player)
        {
            if (Vector2.Distance(player.myPosition, position) < safetyRange)
                return true;
            return false;
        }

        public DTEnemy(Texture2D texture, Vector2 position, int size, Texture2D texBullet)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.texBullet = texBullet;
            rand = new Random();

            hp = 15;
            startHp = hp;
            speed = 80;
            aggroRange = size * 8;
            safetyRange = size * 10;
            currentState = new DTState(this);
            attackState = new DTAttack(this);
            chaseState = new DTChase(this);
            recoverState = new DTRecover(this);
            timeToShoot = false;
        }

        public void Update(float time, Player player, TileGrid grid)
        {
            SetState(player);
            currentState.UpdatePerception(player, grid, time);
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
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        }

        private void UpdateProjectiles(float time)
        {
            if (projectiles.Count > 0)
            {
                for (int i = projectiles.Count - 1; i >= 0; --i)
                {
                    if (!projectiles[i].iamAlive)
                        projectiles.RemoveAt(i);
                }
                foreach (Projectile proj in projectiles)
                {
                    proj.Update(ref time);
                }
            }
        }

        public void Shoot(Vector2 bulletStartPos, Vector2 target)
        {
            if (timeToShoot)
            {
                projectiles.Add(new FireBullet(texBullet, bulletStartPos, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3))));
                timeToShoot = false;
            }
        }

        void SetState(Player player)
        {
            if (Aggressive())
            {
                if (AttackPlayer(player))
                    currentState = attackState;
                else
                    currentState = chaseState;
            }
            else
            {
                if (EscapePlayer(player))
                    currentState = new DTEscape(this);
                else
                    currentState = recoverState;
            }
        }

    }
}

