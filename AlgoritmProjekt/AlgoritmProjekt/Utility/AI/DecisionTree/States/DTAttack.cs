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
    class DTAttack : DTState
    {
        float shootTimer;

        public DTAttack(DTEnemy agent) 
            : base(agent)
        {
            this.agent = agent;
            shootTimer = 0;
        }

        public override void UpdatePerception(Player player, TileGrid grid, float time)
        {
            shootTimer += time;
            //agent.direction = Vector2.Zero;

            if(shootTimer > 1)
            {
                //agent.ResetEnemyPath();
                shootTimer = 0;
                //agent.LetMeAttack = true;
            }

            base.UpdatePerception(player, grid, time);
        }
    }
}
