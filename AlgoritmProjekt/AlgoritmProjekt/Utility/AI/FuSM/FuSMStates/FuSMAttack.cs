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

        public FuSMAttack(AICompanion agent)
            : base(agent)
        {
            this.agent = agent;
            shotRange = Constants.tileSize * 5;
        }

        public override void Execute(Vector2 target)
        {
            //agent.LetMeAttack = true;
            agent.m_vTarget = target;

            base.Execute(target);
        }

    }
}
