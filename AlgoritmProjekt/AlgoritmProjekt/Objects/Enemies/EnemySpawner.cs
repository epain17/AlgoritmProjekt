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
    class EnemySpawner : Enemy
    {
        float spawnTimer = 5;
        float timeLimit = 3;

        public EnemySpawner(Texture2D texture, Vector2 position, Vector2 regroup, int size)
            : base(texture, position, regroup, size)
        {
            this.texture = texture;
            this.position = position;
            this.startPos = regroup;
            this.size = size;
            this.hp = 3;
        }

        public void Update(ref List<Enemy> enemies, Vector2 player, float time, Point targetPoint, TileGrid grid)
        {
            if (myHP <= 0)
                alive = false;
            if (Vector2.Distance(player, myPosition) < 400)
            {
                FindPath(targetPoint, grid);
                spawnTimer += time;
                if (spawnTimer >= timeLimit && waypoints.Count < 12 && waypoints.Count != 0)
                {
                    spawnTimer = 0;
                    SpawnEnemies(ref enemies);
                }
                else
                    waypoints.Clear();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Purple, 0, origin, 1, SpriteEffects.None, 1);
            float healthPercent = hp / startHp;
            Color color = new Color(1, 0, 0.5f * healthPercent);
            spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
        }

        void SpawnEnemies(ref List<Enemy> enemies)
        {
            enemies.Add(new Enemy(texture, position, startPos, size));
        }
    }
}
