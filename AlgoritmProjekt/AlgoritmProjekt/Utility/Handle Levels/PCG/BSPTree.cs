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
        public List<BSPNode> halls = new List<BSPNode>();
        public List<BSPNode> nodes = new List<BSPNode>();
        public BSPNode Root;
        Random random = new Random();
        int minRoomSize = 18;
        int maxRoomSize = 50;

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

                int depthIndex = 0;

                foreach (BSPNode node in nodes)
                {
                    if (node.hierarchy > depthIndex)
                        depthIndex = node.hierarchy;
                    if (node.isLeaf())
                        TransformNodeToRoom(node);
                }

                while (depthIndex > 0)
                {
                    BSPNode prevNode = null;
                    foreach (BSPNode node in nodes)
                    {
                        if (node.Parent != null && node.hierarchy == depthIndex)
                        {
                            if (prevNode == null)
                            {
                                TEST(node.Parent.Left, node.Parent.Right);
                                prevNode = node;
                            }

                            if (prevNode.Parent != node.Parent)
                            {
                                TEST(node.Parent.Left, node.Parent.Right);
                                prevNode = node;
                            }
                        }
                    }

                    depthIndex--;
                }
            }

        }

        public void TEST(BSPNode Left, BSPNode Right)
        {
            if (Left.isLeaf() && Right.isLeaf())
            {
                CreateHall(Left, Right);
            }
            else
            {
                if (!Left.isLeaf() && Right.isLeaf())
                {
                    if (Left.Left != null && Left.Right != null)
                    {
                        if (Left.Left.xDistanceFromCenter(Right.xCenter()) < Left.Right.xDistanceFromCenter(Right.xCenter()) &&
                            Left.Left.yDistanceFromCenter(Right.yCenter()) < Left.Right.yDistanceFromCenter(Right.yCenter()))
                            TEST(Left.Left, Right);
                        else
                            TEST(Left.Right, Right);
                    }
                    else if (Left.Left != null)
                        TEST(Left.Left, Right);
                    else
                        TEST(Left.Right, Right);
                }
                else if (Left.isLeaf() && !Right.isLeaf())
                {
                    if (Right.Left != null && Right.Right != null)
                    {
                        if (Right.Left.xDistanceFromCenter(Left.xCenter()) < Right.Right.xDistanceFromCenter(Left.xCenter()) &&
                            Right.Left.yDistanceFromCenter(Right.yCenter()) < Right.Right.yDistanceFromCenter(Left.yCenter()))
                            TEST(Left, Right.Left);
                        else
                            TEST(Left, Right.Right);
                    }
                    else if (Right.Left != null)
                        TEST(Left, Right.Left);
                    else
                        TEST(Left, Right.Right);
                }
                else
                {
                    int leftleft_RightX = Math.Abs(Left.Left.xDistanceFromCenter(Right.xCenter()));
                    int leftleft_RightY = Math.Abs(Left.Left.yDistanceFromCenter(Right.yCenter()));
                    int leftleft_RightTot = leftleft_RightX + leftleft_RightY;

                    int leftright_RightX = Math.Abs(Left.Right.xDistanceFromCenter(Right.xCenter()));
                    int leftright_RightY = Math.Abs(Left.Right.yDistanceFromCenter(Right.yCenter()));
                    int leftright_RightTot = leftright_RightX + leftright_RightY;

                    int rightleft_LeftX = Math.Abs(Left.Left.xDistanceFromCenter(Left.xCenter()));
                    int rightleft_LeftY = Math.Abs(Left.Left.yDistanceFromCenter(Left.yCenter()));
                    int rightleft_LeftTot = rightleft_LeftX + rightleft_LeftY;

                    int rightright_LeftX = Math.Abs(Left.Right.xDistanceFromCenter(Left.xCenter()));
                    int rightright_LeftY = Math.Abs(Left.Right.yDistanceFromCenter(Left.yCenter()));
                    int rightright_LeftTot = rightright_LeftX + rightright_LeftY;

                    // find left
                    if (leftleft_RightTot < leftright_RightTot)
                    {
                        //Find right
                        if (rightleft_LeftTot < rightright_LeftTot)
                            TEST(Left.Left, Right.Left);
                        else
                            TEST(Left.Left, Right.Right);

                    }
                    else
                    {
                        //Find right
                        if (rightleft_LeftTot < rightright_LeftTot)
                            TEST(Left.Right, Right.Left);
                        else
                            TEST(Left.Right, Right.Right);
                    }
                }

            }
        }

        public void CreateHall(BSPNode l, BSPNode r)
        {
            Point lPoint = new Point(random.Next(l.x + 1, l.x + l.width - 2), random.Next(l.y + 1, l.y + l.height - 2));
            Point rPoint = new Point(random.Next(r.x + 1, r.x + r.width - 2), random.Next(r.y + 1, r.y + r.height - 2));

            int width = rPoint.X - lPoint.X;
            int height = rPoint.Y - lPoint.Y;

            if (width < 0)
            {
                if (height > 0)
                {
                    halls.Add(new BSPNode(null, rPoint.X, lPoint.Y, -width, random.Next(2, 4)));
                    halls.Add(new BSPNode(null, rPoint.X, lPoint.Y, random.Next(2, 4), height));
                }
            }
            else if (width > 0)
            {
                if (height < 0)
                {
                    halls.Add(new BSPNode(null, lPoint.X, rPoint.Y, width, random.Next(2, 4)));
                    halls.Add(new BSPNode(null, lPoint.X, rPoint.Y, random.Next(2, 4), -height));
                }
                else if (height > 0)
                {
                    halls.Add(new BSPNode(null, lPoint.X, lPoint.Y, width, random.Next(2, 4)));
                    halls.Add(new BSPNode(null, rPoint.X, lPoint.Y, random.Next(2, 4), height));
                }
                else
                {
                    halls.Add(new BSPNode(null, lPoint.X, lPoint.Y, width, random.Next(2, 4)));
                }
            }
            else
            {
                halls.Add(new BSPNode(null, rPoint.X, lPoint.Y, random.Next(2, 4), height));
            }
        }

        public void TransformNodeToRoom(BSPNode node)
        {
            int roomWidth, roomHeight, tempX, tempY;
            roomWidth = random.Next(minRoomSize / 2, node.width - 1);
            roomHeight = random.Next(minRoomSize / 2, node.height - 1);
            tempX = random.Next(node.x, node.x + node.width - roomWidth);
            tempY = random.Next(node.y, node.y + node.height - roomHeight);
            node.width = roomWidth;
            node.height = roomHeight;
            node.x = tempX;
            node.y = tempY;
        }

        private void CreateTree(BSPNode node)
        {
            bool didSplit = true;
            int iterations = random.Next(4, 10);

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
