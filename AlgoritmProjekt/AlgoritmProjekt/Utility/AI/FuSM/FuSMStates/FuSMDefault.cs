using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Companion.FuSMStates
{
    class FuSMDefault : FuSMState
    {
        Random rand;
        float minDistance;
        float activationWeight;
        float distance;

        public FuSMDefault(AICompanion agent)
            : base(agent)
        {
            this.agent = agent;
            rand = new Random();
            minDistance = Constants.tileSize * 1.5f;
            activationWeight = Constants.tileSize * 100;
        }

        public override void Execute(Vector2 target, Player player)
        {
            distance = Vector2.Distance(agent.myPosition, target);
            deltaPos = new Vector2((target.X - agent.myPosition.X) + rand.Next(-Constants.tileSize, Constants.tileSize), (target.Y - agent.myPosition.Y) + rand.Next(-Constants.tileSize, Constants.tileSize));

            if (distance < minDistance)
            {
                activationLevel = 0.0001f;
                agent.Speed = 60;
            }
            else
            {
                activationLevel = 1 - ((activationWeight - distance) / activationWeight);
                agent.Speed = agent.StartSpeed * (activationLevel * 30);
            }

            //Console.WriteLine("Default: " + activationLevel);
            base.Execute(target, player);
        }

    }
}
