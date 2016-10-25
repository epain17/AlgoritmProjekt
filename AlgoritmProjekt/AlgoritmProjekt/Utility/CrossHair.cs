using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility
{
    class CrossHair
    {
        Texture2D texture;
        Vector2 position;


        public CrossHair(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

    }
}
