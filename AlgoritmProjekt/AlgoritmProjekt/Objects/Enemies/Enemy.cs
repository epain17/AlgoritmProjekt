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
        Point startPoint, endPoint, previous;

        protected Queue<Vector2> waypoints = new Queue<Vector2>();

        private Point pr;


        protected int AggroRange = 200;

        float DistanceToWaypoint
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position, Vector2 regroup, int size)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.hp = 3;
            this.startPos = regroup;

            startHp = hp;
            speed = 110f;

            pr = new Point((int)regroup.X / mySize, (int)regroup.Y / mySize);
        }

        public void Update(float time, Point targetPoint, TileGrid grid)
        {
            if (FoundPlayer(targetPoint) == 1)
            {
                FindPath(targetPoint, grid);

            }

            else if (FoundPlayer(targetPoint) == 2)
            {
                FindPath(pr, grid);
            }

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

        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            if (waypoints != null)
            {
                if (waypoints.Count() == 0 && targetPoint != myPoint)
                {

                    pathfinder = new Pathfinder(grid);
                    waypoints.Clear();
                    startPoint = myPoint;
                    endPoint = targetPoint;
                    waypoints = pathfinder.FindPath(startPoint, endPoint, previous);
                }

            }

        }

        protected virtual void UpdatePos()
        {
            if (waypoints != null)
            {
                if (waypoints.Count > 0)
                {
                    if (DistanceToWaypoint < 1.5f)
                    {
                        position = waypoints.Peek();
                        previous = new Point((int)waypoints.Peek().X / mySize, (int)waypoints.Peek().Y / mySize);
                        waypoints.Dequeue();
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

        private int FoundPlayer(Point TP)
        {
            if (Range(TP) < AggroRange)
            {
                return 1;
            }

            else if (Range(TP) > AggroRange && myPoint != pr)
            {
                return 2;
            }

            return 0;
        }

        protected float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * size, point.Y * size);
            return Vector2.Distance(this.position, range);
        }
    }
}
