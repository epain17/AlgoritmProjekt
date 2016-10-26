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
    class Enemy : Tile
    {
        Vector2 velocity;
        Texture2D enemyTexture;
        Point enemyPoint;
        float speed;

        Pathfinder pathfinder;
        public Vector2 pathPos;
        Point startPoint, endPoint;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        List<Vector2> newPath = new List<Vector2>();
        List<Vector2> path = new List<Vector2>();

        public Enemy(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.enemyTexture = texture;
            this.position = position;
            this.size = size;
            speed = 0.8f;
        }

        public Point GetCurrentPoint
        {
            get { return enemyPoint = new Point((int)position.X / 32, (int)position.Y / 32); }
        }

        //kan raderas när pathfindingen fungerar bra 
        public Queue<Vector2> Way
        {
            get { return waypoints; }
        }

        public Point EnemyPoint
        {
            get { return enemyPoint = new Point((int)position.X / 32, (int)position.Y / 32); }
        }

        float DistanceToWaypoint
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public void Update(Point targetPoint, TileGrid grid)
        {
            FindPath(targetPoint, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, position, null, Color.Red, 0, origin, 1, SpriteEffects.None, 1);           
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            waypoints.Clear();
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
                    break;
                }
            }

            if (waypoints.Count > 0)
            {
                if (DistanceToWaypoint < 1f)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, speed);
                    position += velocity;
                }
            }
        }
    }
}
