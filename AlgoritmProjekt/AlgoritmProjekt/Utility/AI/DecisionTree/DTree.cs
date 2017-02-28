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
        public abstract bool Evaluate(ref DTState input);
    }

    internal class DTNode : AbsDTNode
    {
        AbsDTNode Left;
        AbsDTNode Right;

        internal DTNode(AbsDTNode Left, AbsDTNode Right)
        {
            this.Left = Left;
            this.Right = Right;
        }

        public override bool Evaluate(ref DTState input)
        {
            if (Left.Evaluate(ref input) || Right.Evaluate(ref input))
                return true;
            return false;
        }
    }

    internal class DTLeaf : AbsDTNode
    {
        public bool NodeState { get; private set; }
        public delegate bool LeafDelegate();
        private LeafDelegate action;

        DTState agentState;

        internal DTLeaf(LeafDelegate action, DTState agentState)
        {
            this.action = action;
            this.agentState = agentState;
            NodeState = false;
        }

        public override bool Evaluate(ref DTState input)
        {
            NodeState = action();
            if (NodeState)
                input = agentState;
            return NodeState;
        }
    }

    class DTree
    {
        DTNode Root, Left, Right;
        DTLeaf Attack, Chase, Escape, Recover;
        DTEnemy agent;

        public DTree(DTEnemy agent)
        {
            this.agent = agent;
            Recover = new DTLeaf(agent.RecoverHP, new DTRecover(agent));
            Escape = new DTLeaf(agent.EscapePlayer, new DTEscape(agent));
            Chase = new DTLeaf(agent.ChasePlayer, new DTChase(agent));
            Attack = new DTLeaf(agent.AttackPlayer, new DTAttack(agent));
            Left = new DTNode(Recover, Escape);
            Right = new DTNode(Chase, Attack);
            Root = new DTNode(Left, Right);
        }

        public void Execute()
        {
            Root.Evaluate(ref agent.CurrentState);
        }
    }
}
