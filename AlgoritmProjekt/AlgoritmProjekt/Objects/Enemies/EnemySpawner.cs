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

        internal SpawnedEnemy(Texture2D texture, Vector2 position, float speed, int aggroRange, int size, int hp)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.startPos = position;
            this.speed = speed;
            this.aggroRange = aggroRange;
            this.size = size;
            this.hp = hp;
        }

        public override void Update(ref float time)
        {
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }

    class EnemySpawner : Enemy
    {
        float spawnTimer = 5;
        float timeLimit = 2;
        int waypointLimiter = 12;

        public EnemySpawner(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.startPos = position;
            this.size = size;
            this.hp = 3;
            this.startHp = hp;
            this.aggroRange = 400;
        }

        public void Update(ref List<Enemy> enemies, Vector2 player, float time, Point targetPoint, TileGrid grid)
        {
            if (myHP <= 0)
                alive = false;
            if (Vector2.Distance(player, myPosition) < aggroRange)
            {
                spawnTimer += time;
                if (spawnTimer >= timeLimit)
                {
                    FindPath(targetPoint, grid);
                    if (waypoints.Count < waypointLimiter && waypoints.Count != 0)
                    {
                        spawnTimer = 0;
                        SpawnEnemies(ref enemies);
                    }
                    else
                        waypoints.Clear();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Purple, 0, origin, 1, SpriteEffects.None, 1);
            float healthPercent = hp / startHp;
            Color color = new Color(1, 0, 0.5f * healthPercent);
            spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
        }

        protected void SpawnEnemies(ref List<Enemy> enemies)
        {
            enemies.Add(new SpawnedEnemy(texture, position, 150, aggroRange, size, startHp));
        }
    }
}
