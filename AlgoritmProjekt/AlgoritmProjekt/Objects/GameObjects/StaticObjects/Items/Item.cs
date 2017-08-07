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
    class Item
    {
        public enum MyType
        {
            Health,
            Money,
            Key,
        }
        public MyType myType;

        //public Item(Vector2 position, int size) 
        //{
        //    this.myPosition = position;
        //    this.mySize = size;
        //}

        //public virtual void Update()
        //{
            
        //}

        //public override void Draw(SpriteBatch spritebatch)
        //{
        //    spritebatch.Draw(myTexture, myPosition, null, texColor, 0, origin, 1, SpriteEffects.None, 1);
        //}
    }
}
