using AlgoritmProjekt.Objects.Companion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.Fusm
{
    class FuSMAIControl
    {
        FuSMMachine machine;
        Tile m_target;
        Tile m_nearestTarget;
        Tile m_nearestPowerup;
        float m_nearestTargetDist;
        float m_nearestPowerupDist;

        bool m_willCollide;
        float m_safetyRadius;
        float m_maxSpeed;

        public FuSMAIControl(AICompanion companion)
        {

        }

        public void Init()
        {
            m_willCollide = false;
            m_nearestTarget = null;
            m_nearestPowerup = null;

            if (m_target == null)
            {
                //m_target = new Target;
                //m_target->m_size = 1;
                //Game.PostGameObj(m_target);
            }
        }

        public void Update(float dt)
        {
            UpdatePerceptions(dt);
            machine.UpdateMachine(dt);
        }

        public void UpdatePerceptions(float dt)
        {

        }
    }
}
