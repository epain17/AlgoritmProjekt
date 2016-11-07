using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Weapons
{
    class Empty : Weapon
    {

        public Empty(Texture2D texture, SpriteFont font, Vector2 position, int size)
            : base(texture, font, position, size)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.size = size;
            this.text = "Empty";
            this.moveMe = true;
        }

        public override void Update(Vector2 camera, int screenWidth, int screenHeight)
        {
            base.Update(camera, screenWidth, screenHeight);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
