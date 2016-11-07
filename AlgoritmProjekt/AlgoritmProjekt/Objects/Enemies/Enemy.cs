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
        protected Vector2 temp;

        protected Queue<Vector2> waypoints = new Queue<Vector2>();

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
            speed = 180f;
        }

        public void Update(float time, Point targetPoint, TileGrid grid)
        {
            FindPath(targetPoint, grid);
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
            if (waypoints.Count() != 0)
            {
                foreach (Vector2 waypoint in waypoints)
                {
                    spriteBatch.Draw(texture, waypoint, null, color, 0, origin, 0.4f, SpriteEffects.None, 1);
                }
                spriteBatch.Draw(texture, temp, null, color, 0, origin, 1, SpriteEffects.None, 1);

            }
        }

        protected bool FindPath(Point targetPoint, TileGrid grid)
        {
            if (Range(targetPoint) < AggroRange && waypoints.Count() == 0)
            {
                waypoints.Clear();
                pathfinder = new Pathfinder(grid);
                startPoint = myPoint;
                endPoint = targetPoint;
                temp = new Vector2(endPoint.X * mySize, endPoint.Y * mySize);
                waypoints = pathfinder.FindPath(startPoint, endPoint);
                return true;
            }
            return false;
        }

        protected virtual void UpdatePos()
        {
            if (waypoints.Count > 0)
            {
                if (DistanceToWaypoint < 1.5f)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();
                    velocity = Vector2.Multiply(direction, speed);
                }
            }
        }

        protected void AddPlayerWaypointToQueue(Point targetPoint)
        {

        }

        protected float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * size, point.Y * size);
            return Vector2.Distance(this.position, range);
        }
    }
}
