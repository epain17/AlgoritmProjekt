using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoritmProjekt.Characters
{
    class Enemy : LivingTile
    {
        protected float speed;
        protected float startHp;

        Pathfinder pathfinder;
        Point startPoint, endPoint;

        protected Queue<Vector2> waypoints = new Queue<Vector2>();

        private Queue<Vector2> mainQue;

        protected int AggroRange = 400;

        float DistanceToWaypoint
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.hp = 3;
            startHp = hp;
            speed = 100f;
        }

        public void Update(float time/*, Point targetPoint, TileGrid grid*/)
        {
            //FindPath(targetPoint, grid);
            UpdatePos();

            if (myHP <= 0)
                alive = false;
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPercent = hp / startHp;
            Color color = new Color(0.25f / healthPercent, 1 * healthPercent, 1f * healthPercent);
            spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            //if(waypoints.Count() != 0)
            //spriteBatch.Draw(texture, waypoints.Peek(), null, color, 0, origin, 1, SpriteEffects.None, 1);
        }

        public int FindPath(Point targetPoint, TileGrid grid)
        {
            if (Range(targetPoint) < AggroRange && waypoints.Count() == 0)
            {
                waypoints.Clear();
                pathfinder = new Pathfinder(grid);
                startPoint = myPoint;
                endPoint = targetPoint;
                waypoints = pathfinder.FindPath(startPoint, endPoint);
                return 0;
            }
            else if (Range(targetPoint) < AggroRange)
            {
                return 1;
            }
            return 2;
        }

        protected virtual void UpdatePos()
        {
            if (mainQue != null)
            {
                if (mainQue.Count > 0)
                {
                    if (DistanceToWaypoint < 1.5f)
                    {
                        position = mainQue.Peek();
                        mainQue.Dequeue();
                    }
                    else
                    {
                        Vector2 direction = waypoints.Peek() - position;
                        direction.Normalize();
                        velocity = Vector2.Multiply(direction, speed);
                    }
                }
                else
                    velocity = Vector2.Zero;
            }
        }

        public Queue<Vector2> SetQueue(Queue<Vector2> queue, Point targetPoint, TileGrid grid)
        {
            if (targetPoint != null && FindPath(targetPoint, grid) == 0)
            {
                return mainQue = waypoints;
            }

            //else if(FindPath(targetPoint, grid) == 1)
            //{
            //    return mainQue = queue;
            //}

            return null;
        }

        protected float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * size, point.Y * size);
            return Vector2.Distance(this.position, range);
        }
    }
}
