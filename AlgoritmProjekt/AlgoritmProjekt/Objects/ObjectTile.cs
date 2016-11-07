using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Weapons
{
    class ObjectTile : Tile
    {
        protected string text;
        protected SpriteFont font;
        protected Color fontColor = Color.DarkSlateGray, texColor = Color.Blue;


        public ObjectTile(Texture2D texture, SpriteFont font, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.font = font;
            this.text = "Object Tile";
        }

        public override void Update(ref float time)
        {
            base.Update(ref time);
        }
        
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, texColor, 0, origin, 1, SpriteEffects.None, 1);
            spritebatch.DrawString(font, text, position, fontColor, 0, new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2), 1, SpriteEffects.None, 0);
        }
    }
}
