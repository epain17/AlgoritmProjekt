using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree.States
{
    class DTChase : DTState
    {

        public DTChase(DTEnemy agent)
            : base(agent)
        {
            this.agent = agent;
        }

        public override void UpdatePerception(Player player, TileGrid grid, float time)
        {
            agent.FindPath(player.myPoint, grid);

            if (agent.waypoints.Count > 0)
            {
                if (agent.ReachedTileDestination(grid))
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
