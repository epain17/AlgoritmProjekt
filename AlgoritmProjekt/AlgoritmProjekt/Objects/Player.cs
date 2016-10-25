using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Characters
{
    class Player : GameObject
    {
        Texture2D texture;

        public Vector2 pos;
        Vector2 velocity;
        Rectangle hitBox; 
        Point point;
        int size;

        public Point PlayerPoint
        {
            get { return point = new Point((int)pos.X/32, (int)pos.Y/32);}
        }

        bool movePlayerRight(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        bool movePlayerLeft(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        bool movePlayerUp(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        bool movePlayerDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public Rectangle HitBox
        {
            get { return hitBox = new Rectangle((int)pos.X, (int)pos.Y, size, size); }
        }

        public void HandelCollision(Wall w, int n)
        {
            //Top
            if (n == 1)
            {
                pos.Y = pos.Y + 4;
            }

            //Bottom             
            if (n == 2)
            {
                pos.Y = pos.Y - 4;
            }

            // Left            
            if (n == 3)
            {
                pos.X = pos.X - 4;
            }

            // Right            
            if (n == 4)
            {
                pos.X = pos.X + 4;
            }

        }

        public virtual int Collision(Wall w)
        {
            Rectangle top = w.HitBox;
            top.Height = 10;

            Rectangle bottom = w.HitBox;
            bottom.Height = 5;
            bottom.Y = bottom.Y + w.HitBox.Height - 5;

            Rectangle left = w.HitBox;
            left.Width = 2;
            left.Y = w.HitBox.Y + 10;
            left.Height = w.HitBox.Height - 20;

            Rectangle right = w.HitBox;
            right.X = right.X + right.Width - 2;
            right.Width = 2;
            right.Y = w.HitBox.Y + 10;
            right.Height = w.HitBox.Height - 20;



            if (HitBox.Intersects(left))
            {
                return 3;
            }

            else if (HitBox.Intersects(right))
            {
                return 4;
            }
            if (HitBox.Intersects(top))
            {
                return 1;
            }

            if (HitBox.Intersects(bottom))
            {
                return 2;
            }
            return 0;
        }

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            this.texture = texture;
            this.pos = position;
            size = 32;
        }

        public override void Update()
        {
            HandlePlayerInteractions(Keys.Down, Keys.Left, Keys.Right, Keys.Up);
            pos += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, null, Color.Brown, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);
        }

        void HandlePlayerInteractions(Keys downKey, Keys leftKey, Keys rightKey, Keys upKey)
        {
            velocity = Vector2.Zero;

            if (movePlayerLeft(leftKey))
            {
                velocity.X = -5;
            }

            if (movePlayerRight(rightKey))
            {
                velocity.X = 5;
            }

            if (movePlayerDown(downKey))
            {
                velocity.Y = 5;
            }

            if (movePlayerUp(upKey))
            {
                velocity.Y = -5;
            }                                   
        }
    }
}
