using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects
{
    internal class SpawnedEnemy : Enemy
    {
        protected override Rectangle HealthBar()
        {
            return base.HealthBar();
        }

        protected override float HealthPercent()
        {
            return base.HealthPercent();
        }

        internal SpawnedEnemy(Texture2D texture, Vector2 position, float speed, int aggroRange, int size, int hp)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.startPos = position;
            this.speed = speed;
            this.aggroRange = aggroRange;
            this.size = size;
            this.hp = hp;
            this.startHp = hp;
        }

        public override void Update(ref float time)
        {
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //foreach (Vector2 way in waypoints)
            //{
            //    spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.35f, SpriteEffects.None, 0);
            //}
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        }
    }

    class EnemySpawner : Enemy
    {
        float spawnTimer = 5;
        float timeLimit = 1.5f;
        int waypointLimiter = 10;

        protected override Rectangle HealthBar()
        {
            return base.HealthBar();
        }

        protected override float HealthPercent()
        {
            return base.HealthPercent();
        }

        public EnemySpawner(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.startPos = position;
            this.size = size;
            this.hp = 15;
            this.startHp = hp;
            this.aggroRange = size * 10;
        }

        public void Update(ref List<Enemy> enemies, Vector2 player, float time, Point targetPoint, TileGrid grid)
        {
            if (myHP <= 0)
                alive = false;
            if (Vector2.Distance(player, myPosition) <= aggroRange)
            {
                FindPath(targetPoint, grid);
                if (waypoints.Count <= waypointLimiter && waypoints.Count > 0)
                {
                    spawnTimer += time;
                    if (spawnTimer >= timeLimit)
                    {
                        spawnTimer = 0;
                        SpawnEnemies(ref enemies);
                    }
                }
                else
                    waypoints.Clear();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = new Color(1, 0, 0.9f * HealthPercent());
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
        }

        protected void SpawnEnemies(ref List<Enemy> enemies)
        {
            enemies.Add(new SpawnedEnemy(myTexture, position, 80, aggroRange, size, 8));
        }
    }
}
