using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoritmProjekt.Grid;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmProjekt.Objects.GameObjects.StaticObjects.Environment
{
    class Wall : GameObject
    {

        public Wall(Vector2 position, int width, int height)
            : base(position, width, height)
        {
            myColor = Color.Gray;
        }

        public override void Update(float time, TileGrid grid)
        {
            base.Update(time, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
