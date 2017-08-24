using System;

namespace Astar
{
    public class Node
    {
        private Node((int, int) point, int g, int h, int f, Node parent)
        {
            Point = point;
            G = g;
            H = h;
            F = f;
            Parent = parent;
        }

        public (int x, int y) Point { get; }

        public int G { get; private set; }

        public int H { get; }

        public int F { get; private set; }

        public Node Parent { get; private set; }

        public Node UpdateGiven(Node potentialNewParent, int fromParentMoveCost)
        {
            var potentialNewG = potentialNewParent.G + fromParentMoveCost;
            if (potentialNewG < G)
            {
                Parent = potentialNewParent;
                G = potentialNewG;
                F = potentialNewG + H;  // TODO: Do refactoring
            }

            return this;
        }


        public static Node CreateNodeWith(Node parent, (int x, int y, int fromParentMoveCost) point, int h)
        {
            var g = parent.G + point.fromParentMoveCost;
            var f = g + h;

            return new Node((point.x, point.y), g, h, f, parent);
        }

        public static Node CreateTheStartingNodeWith((int x, int y) point) => 
            new Node(point, 0, 0, 0, null);
    }
}