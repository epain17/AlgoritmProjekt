using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using AlgoritmProjekt.Utility;
using AlgoritmProjekt.Characters;

namespace AlgoritmProjekt.Objects.Companion.FuSMStates
{
    class FuSMApproach : FuSMState
    {
        float weight;
        float approachRange;
        float distanceToTarget;
        float distanceFromTargetToPlayer;

        public FuSMApproach(AICompanion agent) 
            : base(agent)
        {
            this.agent = agent;
            weight = Constants.tileSize * 4;
            approachRange = Constants.tileSize * 8;
        }

        public override void Execute(Vector2 target, Player player)
        {
            deltaPos = target - agent.myPosition;
            distanceToTarget = Vector2.Distance(agent.myPosition, target);
            distanceFromTargetToPlayer = Vector2.Distance(player.myPosition, target);

            if (target == null || distanceFromTargetToPlayer > approachRange)
                activationLevel = 0;
            else if (distanceToTarget < Constants.tileSize * 1.3f)
                activationLevel = 1;
            else
            {
                activationLevel = 1 - ((weight - distanceToTarget) / weight);
                agent.Speed = agent.StartSpeed * activationLevel;
            }
            
            //Console.WriteLine("Approach: " + activationLevel);
            base.Execute(target, player);
        }
    }
}
