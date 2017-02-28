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
            agent.FindPath(agent.myStartPoint, grid);

            if (agent.waypoints.Count > 0)
            {
                if (agent.DistanceToWaypoint(grid) < 1.5f)
                {
                    agent.myPosition = grid.ReturnTilePosition(agent.waypoints.Dequeue());
                }
                else
                {
                    agent.SetDirection(grid.ReturnTilePosition(agent.waypoints.Peek()));
                }
            }
            base.UpdatePerception(player, grid, time);
        }
    }
}
