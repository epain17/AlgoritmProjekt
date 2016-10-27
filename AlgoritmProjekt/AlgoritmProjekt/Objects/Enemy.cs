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
        float speed;
        
        Pathfinder pathfinder;
        public Vector2 pathPos;
        Point startPoint, endPoint;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        List<Vector2> newPath = new List<Vector2>();
        List<Vector2> path = new List<Vector2>();

        protected int rangeLimit = 300;

        //kan raderas när pathfindingen fungerar bra 
        public Queue<Vector2> Way
        {
            get { return waypoints; }
        }

        float DistanceToWaypoint
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }
        
        public Enemy(Texture2D texture, Vector2 position, int size, int hp)
            : base(texture, position, size)
        {
            this.enemyTexture = texture;
            this.position = position;
            this.size = size;
            this.hp = hp;
            speed = 2.5f;
        }

        public void Update(Point targetPoint, TileGrid grid)
        {
            // if closest node reached
            FindPath(targetPoint, grid);
            UpdatePos();
            position += velocity;
            if (myHP <= 0)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, position, null, Color.Red, 0, origin, 1, SpriteEffects.None, 1);
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            if (Range(targetPoint) < rangeLimit && waypoints.Count() == 0)
            {

                waypoints.Clear();
                pathfinder = new Pathfinder(grid);
                startPoint = myPoint;
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

            }

        }

        private void UpdatePos()
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
