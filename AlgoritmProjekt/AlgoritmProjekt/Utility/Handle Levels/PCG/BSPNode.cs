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
            hierarchy,
            width,
            height,
            x,
            y;

        public BSPNode
            Left,
            Right,
            Parent;

        public bool isConnected;


        public int xCenter()
        {
            return x + (width / 2);
        }

        public int yCenter()
        {
            return y + (height / 2);
        }

        public int xDistanceFromCenter(int X)
        {
            return xCenter() - X;
        }

        public int yDistanceFromCenter(int Y)
        {
            return yCenter() - Y;
        }

        public bool isLeaf()
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
            if (Parent != null)
                hierarchy = Parent.hierarchy + 1;
        }

        public bool x_CheckSAT(BSPNode other)
        {
            if (x < other.x + other.width && x + width > other.x)
                return true;
            return false;
        }

        public bool y_CheckSAT(BSPNode other)
        {
            if (y < other.y + other.height && y + height > other.y)
                return true;
            return false;
        }

    }
}
