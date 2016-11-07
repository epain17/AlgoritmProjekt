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
        float spawnTimer = 0;
        float timeLimit = 5;

        public EnemySpawner(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.hp = 3;
        }

        public void Update(ref List<Enemy> enemies, Vector2 player, float time, Point targetPoint, TileGrid grid)
        {
            if (myHP <= 0)
                alive = false;
            if (Vector2.Distance(player, myPosition) < 400)
            {
                //FindPath(targetPoint, grid);

                Random rand = new Random();
                spawnTimer += time;
                if (spawnTimer >= timeLimit + rand.Next(-2, 5))
                {
                    spawnTimer = 0;
                    SpawnEnemies(ref enemies);
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

        void SpawnEnemies(ref List<Enemy> enemies)
        {
            enemies.Add(new Enemy(texture, position, size));
        }
    }
}
