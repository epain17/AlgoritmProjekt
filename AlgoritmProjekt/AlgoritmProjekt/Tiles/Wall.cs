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
        }

        public override bool Occupied
        {            
            set { occupied = true; }
        }

        public override Point TilePoint
        {
            get { return new Point((int)position.X, (int)position.Y); }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, Color.DarkKhaki, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);

        }
    }
}
