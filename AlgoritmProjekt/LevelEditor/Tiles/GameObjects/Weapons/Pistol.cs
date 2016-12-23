using AlgoritmProjekt.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Tiles.GameObjects
{
    class Pistol : Tile
    {
        public Pistol(Texture2D texture, Vector2 position, int size, SpriteFont font)
            :base(texture, position, size, font)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.color = Color.Blue;
            this.name = "Pistol";
        }
    }
}
