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
    class FuSMEvade : FuSMState
    {
        float evadeRange;
        float distance;


        public FuSMEvade(AICompanion agent) 
            : base(agent)
        {
            this.agent = agent;
            evadeRange = Constants.tileSize * 2;
        }

        public override void Execute(Vector2 target)
        {
            deltaPos = -(target - agent.myPosition);
            distance = Vector2.Distance(agent.myPosition, target);

            if (distance < evadeRange)
            {
                activationLevel = 1 - ((evadeRange) - distance) / (evadeRange);
                agent.Speed = agent.StartSpeed * activationLevel;
            }
            else
                activationLevel = 0;

            base.Execute(target);
        }

    }
}
