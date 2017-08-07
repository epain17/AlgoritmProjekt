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
    class Enemy
    {
        public Queue<Vector2> waypoints = new Queue<Vector2>();
        public Point initialPathPoint, endPoint, previous;
        protected int aggroRange;
        protected int startHp;
        Pathfinder pathfinder;

        public Vector2 FutureWaypoint()
        {
            return waypoints.Count > 0 ? waypoints.Peek() : Vector2.Zero;
        }

        //protected virtual float HealthPercent()
        //{
        //    return (float)((float)hp / (float)startHp);
        //}

        //protected virtual Rectangle HealthBar()
        //{
        //    return new Rectangle((int)myPosition.X, (int)myPosition.Y - (int)(mySize * 0.75f), (int)(mySize * HealthPercent()), (int)(mySize * 0.25f));
        //}

        //public bool ReachedTileDestination(TileGrid grid)
        //{
        //    return Vector2.Distance(myPosition, grid.ReturnTilePosition(waypoints.Peek())) < 2;
        //}

        //public Enemy(Vector2 position, int size)
        //    : base(position, size)
        //{
        //    //this.myTexture = texture;
        //    this.myPosition = position;
        //    this.mySize = size;
        //    myStartPos = position;
        //    hp = 2;
        //    speed = 80;
        //    startHp = hp;
        //    aggroRange = size * 10;
        //}

        //public void Update(float time, Point targetPoint, TileGrid grid)
        //{
        //    FindPath(targetPoint, grid);
        //    UpdatePos(grid);

        //    if (myHP <= 0)
        //        alive = false;
        //    base.Update(ref time);
        //}

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
        //    spriteBatch.Draw(myTexture, myPosition, null, color, 0, origin, 1, SpriteEffects.None, 1);
        //    spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        //}

        //public void FindPath(Point targetPoint, TileGrid grid)
        //{
        //    if (waypoints.Count() == 0 && targetPoint != myPoint)
        //    {
        //        pathfinder = new Pathfinder(grid);
        //        waypoints.Clear();
        //        initialPathPoint = myPoint;
        //        endPoint = targetPoint;
        //        waypoints = pathfinder.FindPointPath(initialPathPoint, endPoint, previous);
        //    }
        //}

        //protected virtual void UpdatePos(TileGrid grid)
        //{
        //    if (waypoints.Count > 0)
        //    {
        //        if (ReachedTileDestination(grid))
        //        {
        //            previous = new Point((int)waypoints.Peek().X / mySize, (int)waypoints.Peek().Y / mySize);
        //            myPosition = grid.ReturnTilePosition(waypoints.Dequeue());
        //            waypoints.Clear();
        //        }
        //        else
        //            SetDirection(grid.ReturnTilePosition(waypoints.Peek()));
        //    }
        //    else
        //    {
        //        myPosition = grid.ReturnTilePosition(myPosition);
        //        StopMoving();
        //    }
        //}

        //public void ResetEnemyPath()
        //{
        //    StopMoving();
        //    waypoints.Clear();
        //}

        //public bool FindTarget(Vector2 target, float searchRange)
        //{
        //    return Vector2.Distance(myPosition, target) < searchRange;
        //}
    }
}
