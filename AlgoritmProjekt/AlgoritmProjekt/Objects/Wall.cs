using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt
{
    class Wall:Tile
    {

        public Wall(Texture2D texture, Vector2 position, int size) 
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.occupied = true;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, Color.Gray, 0, origin, 1, SpriteEffects.None, 1);

        }
    }
}
