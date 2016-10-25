using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoritmProjekt.Characters
{
    class Enemy:GameObject
    {
        Queue<Vector2> waypoints = new Queue<Vector2>();
        Vector2 pos, velocity, dist;
        Texture2D enemyTexture;
        Point enemyPoint;
        float speed;

        public Enemy(Texture2D texture, Vector2 pos) : base(texture, pos)
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
            this.waypoints.Clear();

            foreach (var waypoint in waypoints)
            {
                this.waypoints.Enqueue(waypoint);
            }
        }

        Vector2 Dist
        {
            get { return dist = new Vector2(waypoints.Peek().X + 16, waypoints.Peek().Y + 16);}
        }

        float DistanceToWaypoint
        {
            get { return Vector2.Distance(pos, waypoints.Peek()); }
        }

        public Point GetCurrentWaypoint
        {
            get { return new Point((int)pos.X/ 32, (int)pos.Y/ 32); }
        }

        public override void Update()
        {

            if (waypoints.Count > 0)
            {
                if (DistanceToWaypoint < 1f)
                {
                    pos = waypoints.Peek();
                    // currentPlayerPosition = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - pos;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, speed);
                    pos += velocity;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, pos, null, Color.Red, 0, new Vector2(16, 16), 1, SpriteEffects.None, 1);
        }
    }
}
