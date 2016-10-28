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
        Texture2D enemyTexture;
        float speed;
        float startHp;
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
        
        public Enemy(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.enemyTexture = texture;
            this.position = position;
            this.size = size;
            this.hp = 4;
            startHp = hp;
            speed = 3.2f;
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
            float healthPercent = hp / startHp;
            Color color = new Color(0.25f / healthPercent, 1 * healthPercent, 1f * healthPercent);
            spriteBatch.Draw(enemyTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
        }

        protected void FindPath(Point targetPoint, TileGrid grid)
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
                        { //inte säker på om detta med size i pathpos är rätt - gjorde det för offsets
                            pathPos = new Vector2(pathpos.X - (size / 2), pathpos.Y - (size / 2));
                            newPath.Add(pathPos);
                            waypoints.Enqueue(pathPos);
                        }
                        break;
                    }
                }

            }

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
