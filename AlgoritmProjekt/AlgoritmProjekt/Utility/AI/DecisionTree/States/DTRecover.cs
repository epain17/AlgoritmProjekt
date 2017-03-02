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
    class DTRecover : DTState
    {
        float hpUpTimer;

        public DTRecover(DTEnemy agent)
            : base(agent)
        {
            this.agent = agent;
            hpUpTimer = 0;
        }

        public override void UpdatePerception(Player player, TileGrid grid, float time)
        {
            agent.ResetEnemyPath();
            hpUpTimer += time;
            if (hpUpTimer > 1)
            {
                ++agent.myHP;
                hpUpTimer = 0;
            }
            base.UpdatePerception(player, grid, time);
        }
    }
}
