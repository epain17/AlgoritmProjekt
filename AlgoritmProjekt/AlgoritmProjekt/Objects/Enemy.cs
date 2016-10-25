using AlgoritmProjekt.Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoritmProjekt.Characters
{
    class Enemy : GameObject
    {
        Vector2 pos, velocity;
        Texture2D enemyTexture;
        Point enemyPoint;
        float speed;

        Pathfinder pathfinder;
        public Vector2 pathPos;
        Point startPoint, endPoint;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        Queue<Vector2> waypoints2 = new Queue<Vector2>();
        List<Vector2> newPath = new List<Vector2>();
        List<Vector2> path = new List<Vector2>();

        public Enemy(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            this.enemyTexture = texture;
            this.pos = pos;
            speed = 0.8f;
        }

        public Point EnemyPoint
        {
            get { return enemyPoint = new Point((int)pos.X / 32, (int)pos.Y / 32); }
        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            waypoints2.Clear();

            foreach (var waypoint in waypoints)
            {
                this.waypoints2.Enqueue(waypoint);
            }
        }


        float DistanceToWaypoint
        {
            get { return Vector2.Distance(pos, waypoints.Peek()); }
        }

        public Point GetCurrentWaypoint
        {
            get { return new Point((int)pos.X / 32, (int)pos.Y / 32); }
        }

        public void Update(Point targetPoint, TileGrid grid)
        {
            FindPath(targetPoint, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, pos, null, Color.Red, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            pathfinder = new Pathfinder(grid);
            startPoint = EnemyPoint;
            endPoint = targetPoint;

            newPath.Clear();
            path = pathfinder.FindPath(startPoint, endPoint);
            if (path != null)
            {
                foreach (Vector2 point in path)
                {
                    foreach (Vector2 pathpos in path)
                    {
                        pathPos = new Vector2(pathpos.X, pathpos.Y);
                        newPath.Add(pathPos);
                        waypoints.Enqueue(pathPos);
                    }
                    SetWaypoints(waypoints);
                    break;
                }
            }

            if (waypoints2.Count > 0)
            {
                if (DistanceToWaypoint < 1f)
                {
                    pos = waypoints2.Peek();
                    waypoints2.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints2.Peek() - pos;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, speed);
                    pos += velocity;
                }
            }
        }
    }
}
