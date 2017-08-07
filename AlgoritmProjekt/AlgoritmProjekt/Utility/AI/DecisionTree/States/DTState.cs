using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    class DTState
    {
        protected DTEnemy agent;

        public DTState(DTEnemy agent)
        {
            this.agent = agent;
        }

        public virtual void UpdatePerception(Player player, TileGrid grid, float time)
        {
            //if (agent.myHP <= 0)
            //    agent.iamAlive = false;

        }
    }
}
