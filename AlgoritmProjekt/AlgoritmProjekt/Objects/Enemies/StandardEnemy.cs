using AlgoritmProjekt.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Enemies
{
    class StandardEnemy : Enemy
    {

        public StandardEnemy(Texture2D texture, Vector2 position, float speed, int aggroRange, int size, int hp)
            :base(texture, position, size)
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
}
