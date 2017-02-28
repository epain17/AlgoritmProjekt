using AlgoritmProjekt.Characters;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Companion.FuSMStates
{
    class FuSMState
    {
        protected AICompanion agent;
        protected float activationLevel;
        protected Vector2 deltaPos;

        public FuSMState(AICompanion agent)
        {
            this.agent = agent;
            this.activationLevel = 0.0001f;
        }

        protected virtual void BindActivationLevel(ref float activationLevel)
        {
            if (activationLevel < 0)
                activationLevel = 0;
            else if (activationLevel > 1)
                activationLevel = 1;
        }

        public virtual void Execute(Vector2 target, Player player)
        {
            BindActivationLevel(ref activationLevel);
            agent.AccumulateDirection(deltaPos * activationLevel);
        }
    }
}
