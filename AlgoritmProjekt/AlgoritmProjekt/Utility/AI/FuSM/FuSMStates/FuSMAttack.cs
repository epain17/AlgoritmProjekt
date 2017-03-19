using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Utility;

namespace AlgoritmProjekt.Objects.Companion.FuSMStates
{
    class FuSMAttack : FuSMState
    {
        float shotRange;
        float distance;

        public FuSMAttack(AICompanion agent)
            : base(agent)
        {
            this.agent = agent;
            shotRange = Constants.tileSize * 5;
        }

        public override void Execute(Vector2 target)
        {
            distance = Vector2.Distance(agent.myPosition, target);

            if(agent.TimeToShoot() && shotRange > distance)
                agent.Shoot(target);

            base.Execute(target);
        }

    }
}
