﻿using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt
{
    class Tile
    {
        public string myID = "DEFAULT";
        public Color texColor;
        public bool iamOccupied = false;
        public Vector2 myPosition;
        public int myWidth, myHeight;
        protected float halfWidth, halfHeight;
        protected Texture2D myTexture;

        public Tile NorthNeighbour, EastNeighbour, SouthNeighbour, WestNeighbour;


        public void SetNorthNeighbour(Tile tile)
        {
            NorthNeighbour = tile;
        }

        public void SetEastNeighbour(Tile tile)
        {
            EastNeighbour = tile;
        }
        public void SetSouthNeighbour(Tile tile)
        {
            SouthNeighbour = tile;
        }
        public void SetWestNeighbour(Tile tile)
        {
            WestNeighbour = tile;
        }

        public Vector2 MyCenter()
        {
            return new Vector2(myPosition.X + halfWidth, myPosition.Y + halfHeight);
        }

        public Rectangle myHitBox
        {
            get { return new Rectangle((int)myPosition.X, (int)myPosition.Y, myWidth, myHeight); }
        }

        public bool amIOccupied(Tile target)
        {
            if (myHitBox.Contains(target.myPosition))
                return true;
            return false;
        }

        public Point myPoint
        {
            get { return new Point((int)myPosition.X / myWidth, (int)myPosition.Y / myHeight); }
        }

        public virtual bool CheckMyIntersect(Tile target)
        {
            if (myHitBox.Intersects(target.myHitBox))
            {
                return true;
            }
            return false;
        }

        public virtual bool CheckMyVectorCollision(Vector2 target)
        {
            return myHitBox.Contains(target);
        }

        public Tile(Vector2 position, int width, int height)
        {
            this.myTexture = TextureManager.solidRect;
            this.myPosition = position;
            this.myWidth = width;
            this.myHeight = height;
            halfWidth = width / 2;
            halfHeight = height / 2;
            texColor = new Color(0f, 0.1f, 0f);
            texColor = Color.Red;
        }

        public virtual void Update(ref float time)
        {

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, myHitBox, texColor);
        }
    }
}
