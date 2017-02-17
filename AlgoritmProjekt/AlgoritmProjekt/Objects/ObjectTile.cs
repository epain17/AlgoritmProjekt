﻿using Microsoft.Xna.Framework;
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
        protected float scale;

        public ObjectTile(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.scale = 1;
        }

        public override void Update(ref float time)
        {
            base.Update(ref time);
        }
        
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, texColor, 0, origin, 1, SpriteEffects.None, 1);
        }
    }
}
