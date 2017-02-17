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
        public StateMachine<PatrolEnemy> FSM;
        int patrolWidth, patrolHeight;
        Vector2[] checkpoints;
        int checkPointIndex;
        float timer, timelimit = 3;

        private Vector2 NorthSensor()
        {
            return new Vector2(position.X, position.Y - (size - 1));
        }

        private Vector2 WestSensor()
        {
            return new Vector2(position.X - (size - 1), position.Y);
        }

        private Vector2 SouthSensor()
        {
            return new Vector2(position.X, position.Y + (size - 1));
        }

        private Vector2 EastSensor()
        {
            return new Vector2(position.X + (size - 1), position.Y);
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
            FSM = new StateMachine<PatrolEnemy>(this);
            initializeCheckpoints(startCheckPoint);
        }

        void HandleStates(Player player, TileGrid grid, float time)
        {
            switch (behavior)
            {
                case myBehaviors.Patrol:
                    Patrolling(player);
                    break;
                case myBehaviors.ChaseTarget:
                    Chasing(player, grid);
                    break;
                case myBehaviors.Escape:
                    Escaping(player, grid, time);
                    break;
            }
        }

        public void Patrolling(Player player)
        {
            if (FindTarget(player.myPoint))
                behavior = myBehaviors.ChaseTarget;

            if (Vector2.Distance(position, checkpoints[checkPointIndex]) < 1 && checkPointIndex <= checkpoints.Length - 1)
            {
                ++checkPointIndex;
                if (checkPointIndex > checkpoints.Length - 1)
                    checkPointIndex = 0;
            }
            SetDirection(checkpoints[checkPointIndex]);

            if (hp <= 2 && FindTarget(player.myPoint))
                behavior = myBehaviors.Escape;
            if (myHP <= 0)
                alive = false;
        }

        public void Chasing(Player player, TileGrid grid)
        {
            if (FindTarget(player.myPoint))
                FindPath(player.myPoint, grid);
            else
            {
                SetDirection(checkpoints[checkPointIndex]);
                behavior = myBehaviors.Patrol;
            }

            if (hp <= 2)
                behavior = myBehaviors.Escape;
        }

        public void Escaping(Player player, TileGrid grid, float time)
        {
            if (FindTarget(player.myPoint))
            {
                if (player.myPosition.X > position.X && grid.WalkableFromVect(WestSensor()))
                {
                    FindPath(new Point((int)(WestSensor().X / size), myPoint.Y), grid);
                }
                else if (player.myPosition.X < position.X && grid.WalkableFromVect(EastSensor()))
                {
                    FindPath(new Point((int)(EastSensor().X / size), myPoint.Y), grid);
                }

                if (player.myPosition.Y < position.Y && grid.WalkableFromVect(SouthSensor()))
                    FindPath(new Point(myPoint.X, (int)(SouthSensor().Y / size)), grid);
                else if (player.myPosition.Y > position.Y && grid.WalkableFromVect(NorthSensor()))
                    FindPath(new Point(myPoint.X, (int)(NorthSensor().Y / size)), grid);

            }
            else
            {
                timer += time;
                StopMoving();
                if (timer > timelimit)
                {
                    timer = 0;
                    SetDirection(checkpoints[checkPointIndex]);
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
            HandleStates(player, grid, time);
            FSM.Update();
            UpdatePos(grid);
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPercent = hp / startHp;
            Color color = new Color(0.25f / healthPercent, 1 * healthPercent, 1f * healthPercent);
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
        }
    }
}
