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
            width,
            height;
        internal bool
            isLeaf;
        internal List<BSPNode>
            children;
        internal BSPNode
            parent;

        internal BSPNode(BSPNode parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;
            children = new List<BSPNode>();
        }

        internal void AddChild(BSPNode node)
        {
            if (!isLeaf)
            {
                children.Add(node);
            }
        }
    }

    class BSPTree
    {
        private BSPNode root;
        private int
            levelWidth,
            levelHeight,
            minWidth,
            minHeight;

        public BSPTree(int levelWidth, int levelHeight)
        {
            this.levelWidth = levelWidth;
            this.levelHeight = levelHeight;
            minWidth = levelWidth / 5;
            minHeight = levelHeight / 5;
        }

        private void CreateTree()
        {

        }

        private void AddRecursively(BSPNode currentNode, int newWidth, int newHeight)
        {
            if (root == null)
            {
                root = new BSPNode(null, levelWidth, levelHeight);
            }
            else if (!currentNode.isLeaf)
            {
                currentNode.AddChild(new BSPNode(currentNode, newWidth, newHeight));
            }
            else
                currentNode.isLeaf = true;
        }

    }
}
