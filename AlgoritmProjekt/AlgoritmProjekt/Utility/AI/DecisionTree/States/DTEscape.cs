using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree.States
{
    class DTEscape:DTState
    {
        public DTEscape(DTEnemy agent)
            : base(agent)
        {
            this.agent = agent;
        }

        public override void UpdatePerception(Player player, TileGrid grid, float time)
        {
            base.UpdatePerception(player, grid, time);
        }
    }
}
