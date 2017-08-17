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
    class BSPTree
    {
        public List<Rectangle> halls = new List<Rectangle>();
        public List<BSPNode> nodes = new List<BSPNode>();
        public List<BSPNode> leafNodes = new List<BSPNode>();
        public BSPNode Root;
        Random random = new Random();
        int minRoomSize = 15;
        int maxRoomSize = 40;

        /// <summary>
        /// Used for creating a level
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Partition">Set to true to partition the space, else it becomes a level with a single room</param>
        public BSPTree(int width, int height, bool Partition)
        {
            nodes.Add(Root = new BSPNode(null, 0, 0, width, height));

            if (Partition)
            {
                CreateTree(Root);

                foreach (BSPNode node in leafNodes)
                {
                    TransformNodeToRoom(node);
                }

                foreach (BSPNode node in nodes)
                {
                    if (node.Parent != null)
                    {
                        BSPNode tempLeft = node.Parent.Left, tempRight = node.Parent.Right;
                        Traverse(ref tempLeft, ref tempRight);
                        CreateHall(tempLeft, tempRight);
                    }
                }

                //foreach (BSPNode node in leafNodes)
                //{
                //    if (node.Parent != null)
                //    {
                //        if (!node.Parent.Left.isConnected && !node.Parent.Right.isConnected)
                //        {
                //            BSPNode tempLeft = node.Parent.Left, tempRight = node.Parent.Right;
                //            Traverse(ref tempLeft, ref tempRight);
                //            CreateHall(tempLeft, tempRight);
                //        }
                //    }
                //}
            }

            Console.WriteLine(halls.Count());
        }

        public void Traverse(ref BSPNode left, ref BSPNode right)
        {
            if (left.Left != null || left.Right != null)
            {
                if (left.Left != null && left.Right == null)
                {
                    left = left.Left;
                    Traverse(ref left, ref right);
                }
                else if (left.Left == null && left.Right != null)
                {
                    left = left.Right;
                    Traverse(ref left, ref right);
                }
                else
                {
                    if (left.Left.size > left.Right.size)
                    {
                        left = left.Left;
                        Traverse(ref left, ref right);
                    }
                    else
                    {
                        left = left.Right;
                        Traverse(ref left, ref right);
                    }
                }
            }

            if (right.Left != null || right.Right != null)
            {
                if (right.Left != null && right.Right == null)
                {
                    right = right.Left;
                    Traverse(ref left, ref right);
                }
                else if (right.Left == null && right.Right != null)
                {
                    right = right.Right;
                    Traverse(ref left, ref right);
                }
                else
                {
                    if (right.Left.size > right.Right.size)
                    {
                        right = right.Left;
                        Traverse(ref left, ref right);
                    }
                    else
                    {
                        right = right.Right;
                        Traverse(ref left, ref right);
                    }
                }
            }

        }

        public void CreateHall(BSPNode l, BSPNode r)
        {
            if (!l.isConnected || !r.isConnected)
            {
                Point lPoint = new Point(random.Next(l.x + 1, l.x + l.width - 2), random.Next(l.y + 1, l.y + l.height - 2));
                Point rPoint = new Point(random.Next(r.x + 1, r.x + r.width - 2), random.Next(r.y + 1, r.y + r.height - 2));
                l.isConnected = true;
                r.isConnected = true;
                int width = rPoint.X - lPoint.X;
                int height = rPoint.Y - lPoint.Y;

                if (width < 0)
                {
                    if (height > 0)
                    {
                        //no
                        halls.Add(new Rectangle(rPoint.X, lPoint.Y, -width, 1));
                        halls.Add(new Rectangle(lPoint.X, lPoint.Y, 1, height));
                    }
                    else
                        halls.Add(new Rectangle(rPoint.X, rPoint.Y, -width, 1));
                }
                else if (width > 0)
                {
                    if (height < 0)
                    {
                        halls.Add(new Rectangle(lPoint.X, rPoint.Y, width, 1));
                        halls.Add(new Rectangle(lPoint.X, rPoint.Y, 1, -height));
                    }
                    else if (height > 0)
                    {
                        //WORKS
                        halls.Add(new Rectangle(lPoint.X, lPoint.Y, width, 1));
                        halls.Add(new Rectangle(rPoint.X, lPoint.Y, 1, height));
                    }
                    else
                    {
                        halls.Add(new Rectangle(lPoint.X, lPoint.Y, width, 1));
                    }
                }
                else
                {
                    halls.Add(new Rectangle(rPoint.X, lPoint.Y, 1, height));
                }
            }
        }

        public void TransformNodeToRoom(BSPNode node)
        {
            int roomWidth, roomHeight;
            roomWidth = random.Next(minRoomSize / 2, node.width - 1);
            roomHeight = random.Next(minRoomSize / 2, node.height - 1);
            node.width = roomWidth;
            node.height = roomHeight;
            node.x = random.Next(node.x, node.x + node.width - roomWidth);
            node.y = random.Next(node.y, node.y + node.height - roomHeight);
        }

        private void CreateTree(BSPNode node)
        {
            bool didSplit = true;
            int iterations = 6;

            while (didSplit)
            {
                didSplit = false;
                for (int i = 0; i < iterations; i++)
                {
                    if (i >= nodes.Count())
                        didSplit = false;
                    else if (nodes[i].Left == null && nodes[i].Right == null)
                    {
                        if (nodes[i].width > maxRoomSize || nodes[i].height > maxRoomSize || random.Next(0, 10) > 7)
                        {
                            if (SplitNodes(nodes[i]))
                            {
                                nodes.Add(nodes[i].Left);
                                nodes.Add(nodes[i].Right);
                                i = 0;
                                didSplit = true;
                            }
                        }
                    }
                }
            }

            foreach (BSPNode n in nodes)
            {
                if (n.isLeaf())
                    leafNodes.Add(n);
            }
        }

        public bool SplitNodes(BSPNode node)
        {
            if (node.Left != null || node.Right != null)
                return false;

            bool splitH = random.NextDouble() > 0.5;

            if (node.width > node.height && node.width / node.height >= 1.25)
                splitH = false;
            else if (node.height > node.width && node.height / node.width >= 1.25)
                splitH = true;


            int max = (splitH ? node.height : node.width) - minRoomSize;
            if (max <= minRoomSize)
                return false;

            int split = random.Next(minRoomSize, max);

            if (splitH)
            {
                node.Left = new BSPNode(node, node.x, node.y, node.width, split);
                node.Right = new BSPNode(node, node.x, node.y + split, node.width, node.height - split);
            }
            else
            {
                node.Left = new BSPNode(node, node.x, node.y, split, node.height);
                node.Right = new BSPNode(node, node.x + split, node.y, node.width - split, node.height);
            }

            return true;
        }

    }
}
