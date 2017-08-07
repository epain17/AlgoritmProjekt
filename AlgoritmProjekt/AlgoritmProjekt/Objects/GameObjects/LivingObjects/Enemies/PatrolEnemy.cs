using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Utility.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Enemies
{
    class PatrolEnemy
    {
        public enum myBehaviors
        {
            Patrol,
            ChaseTarget,
            Escape,
        }
        public myBehaviors behavior = myBehaviors.Patrol;
        int patrolWidth, patrolHeight;
        Vector2[] checkpoints;
        int checkPointIndex;
        float timer, timelimit = 3;

        //private Vector2 NorthSensor()
        //{
        //    return new Vector2(myPosition.X, myPosition.Y - mySize);
        //}

        //private Vector2 WestSensor()
        //{
        //    return new Vector2(myPosition.X - mySize, myPosition.Y);
        //}

        //private Vector2 SouthSensor()
        //{
        //    return new Vector2(myPosition.X, myPosition.Y + mySize);
        //}

        //private Vector2 EastSensor()
        //{
        //    return new Vector2(myPosition.X + mySize, myPosition.Y);
        //}

        //public PatrolEnemy(Vector2 position, int size, int startCheckPoint, int patrolWidth, int patrolHeight)
        //    : base(position, size)
        //{
        //    //this.myTexture = texture;
        //    this.patrolWidth = patrolWidth;
        //    this.patrolHeight = patrolHeight;
        //    this.hp = 5;
        //    startHp = hp;
        //    speed = 80;
        //    this.aggroRange = 200;
        //    checkPointIndex = startCheckPoint + 1;
        //    initializeCheckpoints(startCheckPoint);
        //}

        //void HandleStates(Player player, TileGrid grid, float time)
        //{
        //    switch (behavior)
        //    {
        //        case myBehaviors.Patrol:
        //            Patrolling(player, grid);
        //            break;
        //        case myBehaviors.ChaseTarget:
        //            Chasing(player, grid);
        //            break;
        //        case myBehaviors.Escape:
        //            Escaping(player, grid, time);
        //            break;
        //    }
        //}

        //public void Patrolling(Player player, TileGrid grid)
        //{
        //    if (!FindTarget(player.myPosition, mySize * 6))
        //    {
        //        FindPath(new Point((int)checkpoints[checkPointIndex].X / mySize, (int)checkpoints[checkPointIndex].Y / mySize), grid);
        //        if (Vector2.Distance(myPosition, checkpoints[checkPointIndex]) < 1 && checkPointIndex < checkpoints.Length)
        //        {
        //            waypoints.Clear();
        //            ++checkPointIndex;
        //            if (checkPointIndex > checkpoints.Length - 1)
        //                checkPointIndex = 0;
        //        }
        //    }
        //    else
        //    {
        //        if (hp <= 2)
        //        {
        //            behavior = myBehaviors.Escape;
        //        }
        //        else
        //        {
        //            behavior = myBehaviors.ChaseTarget;
        //        }
        //    }
        //}

        //public void Chasing(Player player, TileGrid grid)
        //{
        //    if (FindTarget(player.myPosition, mySize * 8))
        //        FindPath(player.myPoint, grid);
        //    else
        //    {
        //        behavior = myBehaviors.Patrol;
        //    }

        //    if (hp <= 2)
        //    {
        //        behavior = myBehaviors.Escape;
        //    }
        //}

        //public void Escaping(Player player, TileGrid grid, float time)
        //{
        //    if (FindTarget(player.myPosition, mySize * 10))
        //    {
        //        if (player.myPosition.X > myPosition.X && grid.isTileWalkable(WestSensor()))
        //            FindPath(new Point((int)(WestSensor().X / mySize), myPoint.Y), grid);
        //        else if (player.myPosition.X < myPosition.X && grid.isTileWalkable(EastSensor()))
        //            FindPath(new Point((int)(EastSensor().X / mySize), myPoint.Y), grid);

        //        if (player.myPosition.Y < myPosition.Y && grid.isTileWalkable(SouthSensor()))
        //            FindPath(new Point(myPoint.X, (int)(SouthSensor().Y / mySize)), grid);
        //        else if (player.myPosition.Y > myPosition.Y && grid.isTileWalkable(NorthSensor()))
        //            FindPath(new Point(myPoint.X, (int)(NorthSensor().Y / mySize)), grid);
        //    }
        //    else
        //    {
        //        timer += time;

        //        if (timer > timelimit)
        //        {
        //            timer = 0;
        //            behavior = myBehaviors.Patrol;
        //        }
        //    }
        //}

        //void initializeCheckpoints(int index)
        //{
        //    checkpoints = new Vector2[4];
        //    checkpoints[0] = new Vector2(myStartPos.X + (mySize * patrolWidth), myStartPos.Y - (mySize * patrolHeight));
        //    checkpoints[1] = new Vector2(myStartPos.X + (mySize * patrolWidth), myStartPos.Y + (mySize * patrolHeight));
        //    checkpoints[2] = new Vector2(myStartPos.X - (mySize * patrolWidth), myStartPos.Y + (mySize * patrolHeight));
        //    checkpoints[3] = new Vector2(myStartPos.X - (mySize * patrolWidth), myStartPos.Y - (mySize * patrolHeight));
        //    myPosition = checkpoints[index];
        //}

        //public void Update(ref float time, Player player, TileGrid grid)
        //{
        //    if (myHP <= 0)
        //        alive = false;
        //    UpdatePos(grid);
        //    HandleStates(player, grid, time);

        //    base.Update(ref time);
        //}

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    //foreach (Vector2 way in waypoints)
        //    //{
        //    //    spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 0);
        //    //}
        //    Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
        //    spriteBatch.Draw(myTexture, myPosition, null, color, 0, origin, 1, SpriteEffects.None, 1);
        //    spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);

            //// Draw CheckPoints
            //spriteBatch.Draw(myTexture, checkpoints[0], null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, checkpoints[1], null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, checkpoints[2], null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, checkpoints[3], null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);

            ////Draw Sensors
            //spriteBatch.Draw(myTexture, NorthSensor(), null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, SouthSensor(), null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, WestSensor(), null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
            //spriteBatch.Draw(myTexture, EastSensor(), null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 1);
        //}
    }
}
