using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    class Node
    {
        protected bool question;
        public Node Left;
        public Node Right;

        DTState behavior;

        public DTState State
        {
            get { return behavior; }
        }

        public bool Question
        {
            get { return question; }
            set { question = value; }
        }

        public Node(bool question, DTState behavior)
        {
            this.question = question;
            this.Left = null;
            this.Right = null;
            this.behavior = behavior;
        }
    }
}
