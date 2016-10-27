using AlgoritmProjekt.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects
{
    class EnemySpawner : Tile
    {
        int hp;
        bool alive = true;

        float spawnTimer = 500;

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public EnemySpawner(Texture2D texture, Vector2 position, int size, int hp) 
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.hp = hp;
        }

        public void Update(ref List<Enemy> enemies)
        {
            if (HP <= 0)
                alive = false;
            if(spawnTimer >= 500)
            {
                spawnTimer = 0;
                SpawnEnemies(ref enemies);
            }
            spawnTimer++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Purple, 0, origin, 1, SpriteEffects.None, 1);
        }

        void SpawnEnemies(ref List<Enemy> enemies)
        {
            enemies.Add(new Enemy(texture, position, size, 30));
        }
    }
}
