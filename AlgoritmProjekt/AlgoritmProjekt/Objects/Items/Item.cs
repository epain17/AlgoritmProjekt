using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Weapons
{
    class Item : ObjectTile
    {

        public Item(Texture2D texture, Vector2 position, int size) 
            :base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
        }

        public virtual void Update()
        {
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, texColor, 0, origin, 1, SpriteEffects.None, 1);
        }
    }
}
