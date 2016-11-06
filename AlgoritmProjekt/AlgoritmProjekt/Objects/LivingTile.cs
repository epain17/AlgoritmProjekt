using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects
{
    class LivingTile : Tile
    {
        protected bool alive = true;
        protected int hp;

        public int myHP
        {
            get { return hp; }
            set { hp = value; }
        }

        public bool iamAlive
        {
            get { return alive; }
        }

        public LivingTile(Texture2D texture, Vector2 position, int size) 
            :base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        public override void Update(ref float time)
        {
            position += time * (velocity + acceleration * time / 2);
            velocity += acceleration * time;
            base.Update(ref time);
        }
    }
}
