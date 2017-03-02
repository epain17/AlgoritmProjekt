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
    class PatrolEnemy : Enemy
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

        private Vector2 NorthSensor()
        {
            return new Vector2(position.X, position.Y - size);
        }

        private Vector2 WestSensor()
        {
            return new Vector2(position.X - size, position.Y);
        }

        private Vector2 SouthSensor()
        {
            return new Vector2(position.X, position.Y + size);
        }

        private Vector2 EastSensor()
        {
            return new Vector2(position.X + size, position.Y);
        }

        public PatrolEnemy(Texture2D texture, Vector2 position, int size, int startCheckPoint, int patrolWidth, int patrolHeight)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.patrolWidth = patrolWidth;
            this.patrolHeight = patrolHeight;
            this.hp = 5;
            startHp = hp;
            speed = 80;
            this.aggroRange = 200;
            checkPointIndex = startCheckPoint + 1;
            initializeCheckpoints(startCheckPoint);
        }

        void HandleStates(Player player, TileGrid grid, float time)
        {
            switch (behavior)
            {
                case myBehaviors.Patrol:
                    Patrolling(player, grid);
                    break;
                case myBehaviors.ChaseTarget:
                    Chasing(player, grid);
                    break;
                case myBehaviors.Escape:
                    Escaping(player, grid, time);
                    break;
            }
        }

        public void Patrolling(Player player, TileGrid grid)
        {
            if (!FindTarget(player.myPosition, size * 6))
            {
                FindPath(new Point((int)checkpoints[checkPointIndex].X / size, (int)checkpoints[checkPointIndex].Y / size), grid);
                if (Vector2.Distance(position, checkpoints[checkPointIndex]) < 1 && checkPointIndex < checkpoints.Length)
                {
                    waypoints.Clear();
                    ++checkPointIndex;
                    if (checkPointIndex > checkpoints.Length - 1)
                        checkPointIndex = 0;
                }
            }
            else
            {
                if (hp <= 2)
                {
                    behavior = myBehaviors.Escape;
                }
                else
                {
                    behavior = myBehaviors.ChaseTarget;
                }
            }
        }

        public void Chasing(Player player, TileGrid grid)
        {
            if (FindTarget(player.myPosition, size * 8))
                FindPath(player.myPoint, grid);
            else
            {
                behavior = myBehaviors.Patrol;
            }

            if (hp <= 2)
            {
                behavior = myBehaviors.Escape;
            }
        }

        public void Escaping(Player player, TileGrid grid, float time)
        {
            if (FindTarget(player.myPosition, size * 10))
            {
                if (player.myPosition.X > position.X && grid.WalkableFromVect(WestSensor()))
                    FindPath(new Point((int)(WestSensor().X / size), myPoint.Y), grid);
                else if (player.myPosition.X < position.X && grid.WalkableFromVect(EastSensor()))
                    FindPath(new Point((int)(EastSensor().X / size), myPoint.Y), grid);

                if (player.myPosition.Y < position.Y && grid.WalkableFromVect(SouthSensor()))
                    FindPath(new Point(myPoint.X, (int)(SouthSensor().Y / size)), grid);
                else if (player.myPosition.Y > position.Y && grid.WalkableFromVect(NorthSensor()))
                    FindPath(new Point(myPoint.X, (int)(NorthSensor().Y / size)), grid);
            }
            else
            {
                timer += time;

                if (timer > timelimit)
                {
                    timer = 0;
                    behavior = myBehaviors.Patrol;
                }
            }
        }

        void initializeCheckpoints(int index)
        {
            checkpoints = new Vector2[4];
            checkpoints[0] = new Vector2(startPos.X + (size * patrolWidth), startPos.Y - (size * patrolHeight));
            checkpoints[1] = new Vector2(startPos.X + (size * patrolWidth), startPos.Y + (size * patrolHeight));
            checkpoints[2] = new Vector2(startPos.X - (size * patrolWidth), startPos.Y + (size * patrolHeight));
            checkpoints[3] = new Vector2(startPos.X - (size * patrolWidth), startPos.Y - (size * patrolHeight));
            position = checkpoints[index];
        }

        public void Update(ref float time, Player player, TileGrid grid)
        {
            if (myHP <= 0)
                alive = false;
            UpdatePos(grid);
            HandleStates(player, grid, time);

            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 way in waypoints)
            {
                spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 0);
            }
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);

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
        }
    }
}
