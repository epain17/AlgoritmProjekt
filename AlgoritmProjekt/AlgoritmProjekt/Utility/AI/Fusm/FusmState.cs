using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.Fusm
{
    class FuSMState
    {
        protected FuSMAIControl m_parent;
        protected float m_activationLevel;

        public FuSMState(FuSMAIControl parent)
        {
            m_parent = parent;
            m_activationLevel = 0.0f;
        }

        public virtual void Init()
        {
            m_activationLevel = 0.0f;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update(float dt)
        {

        }


        public virtual void Exit()
        {

        }
        public virtual float CalculateActivation()
        {
            return m_activationLevel;
        }

        protected virtual void CheckLowerBound(float lbound = 0.0f) { if (m_activationLevel < lbound) m_activationLevel = lbound; }
        protected virtual void CheckUpperBound(float ubound = 1.0f) { if (m_activationLevel > ubound) m_activationLevel = ubound; }
        protected virtual void CheckBounds(float lb = 0.0f, float ub = 1.0f) { CheckLowerBound(lb); CheckUpperBound(ub); }

    }
}
