using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Utility.AI.DecisionTree;
using AlgoritmProjekt.Utility.AI.DecisionTree.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    abstract class AbsDTNode
    {
        public abstract void Evaluate(ref DTState currentState);
    }

    internal class DTNode : AbsDTNode
    {
        public delegate bool LeafDelegate();
        protected LeafDelegate action;
        AbsDTNode Left;
        AbsDTNode Right;

        internal DTNode(LeafDelegate action, AbsDTNode Left, AbsDTNode Right)
        {
            this.action = action;
            this.Left = Left;
            this.Right = Right;
        }

        public override void Evaluate(ref DTState currentState)
        {
            if (action())
                Right.Evaluate(ref currentState);
            else
                Left.Evaluate(ref currentState);
        }
    }

    internal class DTLeaf : AbsDTNode
    {
        DTState agentState;

        internal DTLeaf(DTState agentState)
        {
            this.agentState = agentState;
        }

        public override void Evaluate(ref DTState currentState)
        {
            currentState = agentState;
        }
    }

    class DTree
    {
        DTEnemy agent;
        DTNode Root, Left, Right;
        DTLeaf AttackLeaf, ChaseLeaf, EscapeLeaf, RecoverLeaf;

        public DTree(DTEnemy agent)
        {
            this.agent = agent;
            RecoverLeaf = new DTLeaf(new DTRecover(agent));
            EscapeLeaf = new DTLeaf(new DTEscape(agent));
            ChaseLeaf = new DTLeaf(new DTChase(agent));
            AttackLeaf = new DTLeaf(new DTAttack(agent));
            //Left = new DTNode(agent.ChooseDefensiveStance, RecoverLeaf, EscapeLeaf);
            //Right = new DTNode(agent.ChooseOffensiveStance, ChaseLeaf, AttackLeaf);
            //Root = new DTNode(agent.ChooseCombatStance, Left, Right);
        }

        public void Execute()
        {
            Root.Evaluate(ref agent.CurrentState);
        }
    }
}
