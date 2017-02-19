using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.Fusm
{
    class FuSMMachine : FuSMState
    {
        protected List<FuSMState> activeStates = new List<FuSMState>();
        protected List<FuSMState> states = new List<FuSMState>();

        public FuSMMachine(FuSMAIControl parent)
            : base(parent)
        {
            m_parent = parent;
        }

        public virtual void UpdateMachine(float dt)
        {
            if (states.Count > 0)
            {
                //check for activations, and then update
                activeStates.Clear();
                List<FuSMState> nonActiveStates = new List<FuSMState>();
                for (int i = 0; i < states.Count; i++)
                {
                    if (states[i].CalculateActivation() > 0)
                        activeStates.Add(states[i]);
                    else
                        nonActiveStates.Add(states[i]);
                }

                //Exit all non active states for cleanup
                if (nonActiveStates.Count != 0)
                {
                    for (int i = 0; i < nonActiveStates.Count; i++)
                        nonActiveStates[i].Exit();
                }

                //Update all activated states
                if (activeStates.Count != 0)
                {
                    for (int i = 0; i < activeStates.Count; i++)
                        activeStates[i].Update(dt);
                }
            }
        }
        public virtual void AddState(FuSMState state)
        {
            states.Add(state);
        }
        public virtual bool IsActive(FuSMState state)
        {
            return false;
        }
        public virtual void Reset()
        {
            for (int i = 0; i < states.Count; i++)
            {
                states[i].Exit();
                states[i].Init();
            }
        }


    }
}
