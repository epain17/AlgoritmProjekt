﻿using AlgoritmProjekt.Input;
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
        int size;
        float rotate;
    
        public Vector2 myPosition
        {
            get { return position; }
        }

        public CrossHair(Texture2D texture, Vector2 position, int size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            rotate = (float)Math.PI / 4;
        }

        public void Update(Vector2 camera)
        {
            position = new Vector2(KeyMouseReader.mouseState.X - camera.X, KeyMouseReader.mouseState.Y - camera.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, new Color(0.5f, 0.5f, 0.5f), rotate, new Vector2(size / 2, size / 2), 0.75f, SpriteEffects.None, 0);
        }

    }
}
