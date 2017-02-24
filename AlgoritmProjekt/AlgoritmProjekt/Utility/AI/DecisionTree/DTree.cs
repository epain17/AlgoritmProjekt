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
    class DTree
    {
        Node root;
        DTState currentState,
    attackState, chaseState, recoverState, escapeState;
        DTEnemy agent;

        bool Aggressive()
        {
            if (agent.myHP < 7)
                return false;
            return true;
        }

        bool AttackPlayer(Player player)
        {
            if (Vector2.Distance(player.myPosition, agent.myPosition) < (agent.AggroRange() * 0.5f))
                return true;
            return false;
        }

        bool EscapePlayer(Player player)
        {
            if (Vector2.Distance(player.myPosition, agent.myPosition) < agent.SafetyRange())
                return true;
            return false;
        }

        public Node Root
        {
            get { return root; }
            set { root = value; }
        }

        public DTree(bool question, DTEnemy agent)
        {
            this.agent = agent;
            this.root = new Node(question, null);
        }

        public void Insert(bool question, DTState state)
        {
            Node current = new Node(question, state);

            if (root == null)
                root = current;
            else
            {
                AddRecursively(current, root);
            }
        }

        void AddRecursively(Node nodeToAdd, Node parent)
        {
            if (parent.Question)
            {
                if (parent.Right == null)
                    parent.Right = nodeToAdd;
                else
                    AddRecursively(nodeToAdd, parent.Right);
            }
            else if (!parent.Question)
            {
                if (parent.Left == null)
                    parent.Left = nodeToAdd;
                else
                    AddRecursively(nodeToAdd, parent.Left);
            }
        }

        void SetState(Player player)
        {
            if (Aggressive())
            {
                if (AttackPlayer(player))
                    currentState = attackState;
                else
                    currentState = chaseState;
            }
            else
            {
                if (EscapePlayer(player))
                    currentState = escapeState;
                else
                    currentState = recoverState;
            }
        }

        public DTState FindState(Node parent)
        {
            Node current = parent;
            DTState result = null;

            if (current.State != null)
                return current.State;
            else
            {
                if (current.Question)
                {
                    result = FindState(current.Right);
                }
                else
                {
                    result = FindState(current.Left);
                }
            }

            return result;
        }
    }
}
