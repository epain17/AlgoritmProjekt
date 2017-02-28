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
    class Enemy : MovingTile
    {
        public Queue<Vector2> waypoints = new Queue<Vector2>();
        public Point startPoint, endPoint, previous;
        protected int aggroRange;
        protected int startHp;
        Pathfinder pathfinder;
        public Point pr;

        protected virtual float HealthPercent()
        {
            return (float)((float)hp / (float)startHp);
        }

        protected virtual Rectangle HealthBar()
        {
            return new Rectangle((int)position.X, (int)position.Y - (int)(size * 0.75f), (int)(size * HealthPercent()), (int)(size * 0.25f));
        }

        public float DistanceToWaypoint(TileGrid grid)
        {
            return Vector2.Distance(position, grid.ReturnTilePosition(waypoints.Peek()));
        }

        public Enemy(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.startPos = position;
            this.size = size;
            this.hp = 2;
            this.aggroRange = size * 10;

            startHp = hp;
            speed = 80;
            pr = new Point((int)startPos.X / mySize, (int)startPos.Y / mySize);
        }

        public void Update(float time, Point targetPoint, TileGrid grid)
        {
            if (FindTarget(targetPoint))
            {
                FindPath(targetPoint, grid);

            }
            else if (FindTarget(targetPoint))
            {
                FindPath(pr, grid);
            }

            UpdatePos(grid);

            if (myHP <= 0)
                alive = false;
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            if (waypoints.Count() == 0 && targetPoint != myPoint)
            {
                pathfinder = new Pathfinder(grid);
                waypoints.Clear();
                startPoint = myPoint;
                endPoint = targetPoint;
                waypoints = pathfinder.FindPointPath(startPoint, endPoint, previous);
            }
        }

        protected virtual void UpdatePos(TileGrid grid)
        {
            if (waypoints.Count > 0)
            {
                if (DistanceToWaypoint(grid) < 1.5f)
                {
                    previous = new Point((int)waypoints.Peek().X / mySize, (int)waypoints.Peek().Y / mySize);
                    position = grid.ReturnTilePosition(waypoints.Dequeue());
                    waypoints.Clear();
                }
                else
                {
                    SetDirection(grid.ReturnTilePosition(waypoints.Peek()));
                }
            }
        }

        public bool FindTarget(Point point)
        {
            Vector2 range = new Vector2(point.X * size, point.Y * size);
            return Vector2.Distance(this.position, range) < aggroRange;
        }
    }
}
