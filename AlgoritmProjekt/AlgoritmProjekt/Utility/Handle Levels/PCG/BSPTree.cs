using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.GameObjects.StaticObjects.Environment;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.PCG
{
    internal class BSPNode
    {
        internal int
            hierarchy,
            width,
            height,
            x,
            y;

        internal bool
            isLeaf,
            isConnected;

        internal BSPNode
            Left,
            Right,
            Parent;

        internal BSPNode(BSPNode parent, int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.Parent = parent;
            this.x = x;
            this.y = y;
            isLeaf = true;
            if (parent != null)
                hierarchy = parent.hierarchy + 1;
            isConnected = false;
            Left = null;
            Right = null;
        }

        internal bool WithinBoundariesX(int X)
        {
            if (X >= x && X <= x + width)
                return true;
            return false;
        }

        internal bool WithinBoundariesY(int Y)
        {
            if (Y >= y && Y <= y + height)
                return true;
            return false;
        }

        internal int Area()
        {
            return width * height;
        }

        public int CentralX()
        {
            return x + width / 2;
        }

        public int CentralY()
        {
            return y + height / 2;
        }

    }

    class BSPTree
    {
        Random random = new Random();
        public BSPNode Root;
        private int
            minWidth,
            minHeight,
            layers;

        public List<BSPNode> LeafNodes = new List<BSPNode>();
        public List<BSPNode> Conjunctions = new List<BSPNode>();

        /// <summary>
        /// Used for creating a level
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Partition">Set to true to partition the space, else it becomes a level with a single room</param>
        public BSPTree(int width, int height, bool Partition)
        {
            minWidth = 8;
            minHeight = 8;
            Root = new BSPNode(null, 0, 0, width, height);
            Root.hierarchy = 1;
            LeafNodes.Add(Root);

            if (Partition)
            {
                CreateTree();

                foreach (BSPNode node in LeafNodes)
                {
                    ShrinkNode(node);
                }

                foreach (BSPNode node in LeafNodes)
                {
                    AddConjunction(node.Parent.Left, node.Parent.Right);
                }
            }
        }

        private void AddConjunction(BSPNode Left, BSPNode Right)
        {
            if (!Left.isConnected || !Right.isConnected)
            {
                Conjunctions.Add(ConnectLeafNodes(Left, Right));
            }

        }

        private void FindLargestChild(BSPNode Parent)
        {

        }

        internal BSPNode ConnectLeafNodes(BSPNode Left, BSPNode Right)
        {
            Random random = new Random();
            Left.isConnected = true;
            Right.isConnected = true;

            int hallX, hallY,
                hallWidth = 1,
                hallHeight = 1;

            int tempX = Right.x - (Left.x + Left.width),
                tempY = Right.y - (Left.y + Left.height);

            if (tempX > tempY)
            {
                //Horizontal Bridge
                hallWidth = tempX + 2;
                hallX = Left.x + Left.width - 1;
                hallY = 
                    Left.CentralY() > Right.CentralY() ?
                    Left.CentralY() : Right.CentralY();
            }
            else
            {
                //Vertical Bridge
                hallHeight = tempY + 2;
                hallX = 
                    Left.CentralX() < Right.CentralX() ?
                    Left.CentralX() + random.Next(-3, 3) : Right.CentralX() + random.Next(-3, 3);
                hallY = Left.y + Left.height - 1;
            }

            return new BSPNode(null, hallX, hallY, hallWidth, hallHeight);
        }

        private void CreateTree()
        {
            int iterations = 15;

            for (int i = 0; i < iterations; i++)
            {
                int seed;

                do
                    seed = random.Next(LeafNodes.Count);
                while (!LeafNodes[seed].isLeaf);

                bool vertical = random.Next(0, 2) == 1;

                if (vertical)
                {
                    int newWidth = LeafNodes[seed].width / 2;
                    if (newWidth >= minWidth)
                    {
                        SplitANode(LeafNodes[seed], newWidth, true);
                    }
                }
                else
                {
                    int newHeight = LeafNodes[seed].height / 2;
                    if (newHeight >= minHeight)
                    {
                        SplitANode(LeafNodes[seed], newHeight, false);
                    }
                }
            }
        }

        private void SplitANode(BSPNode currentNode, int value, bool vertical)
        {
            ClearNode(currentNode);
            layers++;
            if (vertical)
            {
                currentNode.Left = new BSPNode(currentNode, currentNode.x, currentNode.y, value, currentNode.height);
                currentNode.Right = new BSPNode(currentNode, currentNode.x + value, currentNode.y, value, currentNode.height);
                LeafNodes.Add(currentNode.Left);
                LeafNodes.Add(currentNode.Right);
            }
            else
            {
                currentNode.Left = new BSPNode(currentNode, currentNode.x, currentNode.y, currentNode.width, value);
                currentNode.Right = new BSPNode(currentNode, currentNode.x, currentNode.y + value, currentNode.width, value);
                LeafNodes.Add(currentNode.Left);
                LeafNodes.Add(currentNode.Right);
            }
        }

        private void ShrinkNode(BSPNode node)
        {
            int randomWidth = node.width - random.Next(0, 6),
                randomHeight = node.height - random.Next(0, 6);
            if (random.Next(5) > 3)
            {
                node.x = (node.x + node.width) - (randomWidth);
                node.y = (node.y + node.height) - (randomHeight);
            }
            node.width = randomWidth;
            node.height = randomHeight;
        }

        private void ClearNode(BSPNode currentNode)
        {
            if (currentNode.isLeaf)
            {
                currentNode.isLeaf = false;
                for (int i = LeafNodes.Count - 1; i >= 0; --i)
                {
                    if (currentNode == LeafNodes[i])
                        LeafNodes.RemoveAt(i);
                }
            }
        }

    }
}
