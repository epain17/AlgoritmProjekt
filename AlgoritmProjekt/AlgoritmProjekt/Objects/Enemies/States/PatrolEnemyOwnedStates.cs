using AlgoritmProjekt.Utility.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Enemies.States
{
    class IsPatrolling : State<PatrolEnemy>
    {
        public override void Enter(PatrolEnemy temp)
        {
            if (temp.behavior != PatrolEnemy.myBehaviors.Patrol)
                temp.behavior = PatrolEnemy.myBehaviors.Patrol;
        }

        public override void Execute(PatrolEnemy temp)
        {
            //temp.Patrolling();
        }

        public override void Exit(PatrolEnemy temp)
        {
            
        }
    }
}
