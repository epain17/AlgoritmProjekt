﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt
{
    class Camera
    {
        Rectangle ScreenBounds, LevelBounds;
        Matrix translationMatrix;
        public Vector2 CameraPos;

        public Matrix TranslationMatrix
        {
            get { return translationMatrix; }
        }

        public Camera(Rectangle ScreenBounds, Rectangle LevelBounds)
        {
            this.ScreenBounds = ScreenBounds;
            this.LevelBounds = LevelBounds;
        }

        public void Update(Vector2 pos)
        {
            float translationX = -MathHelper.Clamp(pos.X - ScreenBounds.Width, -ScreenBounds.Width, LevelBounds.Width - ScreenBounds.Width);
            float translationY = -MathHelper.Clamp(pos.Y - ScreenBounds.Height, -ScreenBounds.Height, LevelBounds.Height - ScreenBounds.Height);
            translationMatrix = Matrix.CreateTranslation(translationX, translationY, 0);
            CameraPos.X = translationX;
            CameraPos.Y = translationY;
        }
    }
}
