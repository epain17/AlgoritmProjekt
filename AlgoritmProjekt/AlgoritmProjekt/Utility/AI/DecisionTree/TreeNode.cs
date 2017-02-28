using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    public interface ITreeNode
    {
        bool NodeState { get; }
        bool Evaluate();
    }

    public class Selector : ITreeNode
    {
        List<ITreeNode> children;
        public bool NodeState { get; private set; } = false;

        public Selector(List<ITreeNode> children)
        {
            this.children = children;
        }

        public bool Evaluate()
        {
            foreach (ITreeNode node in children)
            {
                if (node.Evaluate())
                {
                    NodeState = true;
                    return true;
                }
            }
            NodeState = false;
            return false;
        }
    }

    public class Sequence : ITreeNode
    {
        List<ITreeNode> children;
        public bool NodeState { get; private set; } = false;

        public Sequence(List<ITreeNode> children)
        {
            this.children = children;
        }

        public bool Evaluate()
        {
            foreach (ITreeNode node in children)
            {
                if (!node.Evaluate())
                {
                    NodeState = false;
                    return false;
                }
            }

            NodeState = true;
            return true;
        }

        public class Inverter : ITreeNode
        {
            ITreeNode nodeToInvert;
            public bool NodeState { get; private set; } = false;
            
            public Inverter(ITreeNode nodeToInvert)
            {
                this.nodeToInvert = nodeToInvert;
            } 

            public bool Evaluate()
            {
                NodeState = !nodeToInvert.Evaluate();
                return NodeState;
            }
        }

        public class ActionNode : ITreeNode
        {
            public delegate bool ActionNodeDelegate();
            private ActionNodeDelegate action;
            public bool NodeState { get; private set; } = false;

            public ActionNode(ActionNodeDelegate action)
            {
                this.action = action;
            }

            public bool Evaluate()
            {
                NodeState = action();
                return NodeState;
            }
        }

        public class AITree
        {
            private float playerDistanceFromEnemy;
            private int playerPower;

            private ActionNode IsInAttackRange;
            private ActionNode IsVisible;
            private ActionNode EstimatePlayerPower;
            private Sequence Attack;
            private Inverter Patrol;
            private Sequence Escape;
            private Selector Root;

            bool IsPlayerInAttackRange() => playerDistanceFromEnemy < 5;
            bool IsPlayerVisible() => playerDistanceFromEnemy < 8;
            bool IsPlayerTooPowerful() => playerPower > 3;

            public AITree()
            {
                IsInAttackRange = new ActionNode(IsPlayerInAttackRange);
                IsVisible = new ActionNode(IsPlayerVisible);
                EstimatePlayerPower = new ActionNode(IsPlayerTooPowerful);
                Attack = new Sequence(new List<ITreeNode> { IsInAttackRange, IsVisible });
                Patrol = new Inverter(Attack);
                Escape = new Sequence(new List<ITreeNode> { IsVisible, EstimatePlayerPower });
                Root = new Selector(new List<ITreeNode> { Escape, Patrol, Attack });

                Root.Evaluate();
            }
        }

        public static class DecisionTree
        {
            public static void Test()
            {
                AITree tree = new AITree();
            }
        }
    }
}
