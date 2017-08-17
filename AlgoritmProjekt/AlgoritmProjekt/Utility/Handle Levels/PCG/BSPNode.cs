using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.PCG
{
    public class BSPNode
    {
        public int
            width,
            height,
            x,
            y;

        public BSPNode
            Left,
            Right,
            Parent;

        public bool isConnected;

        public int size;

        public  bool isLeaf()
        {
            if (Left != null || Right != null)
                return false;
            return true;
        }

        public BSPNode(BSPNode Parent, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.Parent = Parent;
            size = width * height;
        }
    }
}
