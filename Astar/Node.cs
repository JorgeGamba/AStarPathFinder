using System;
using System.Collections.Generic;

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

        public IEnumerable<(int x, int y)> GetAdjacentPoints() =>
            new[]
            {
                (Point.x, Point.y + 1),
                (Point.x, Point.y - 1),
                (Point.x + 1, Point.y),
                (Point.x - 1, Point.y),
                (Point.x - 1, Point.y - 1),
                (Point.x - 1, Point.y + 1),
                (Point.x + 1, Point.y - 1),
                (Point.x + 1, Point.y + 1)
            };

        public int FindTheCostOfAdjacentMoveTo((int x, int y) targetPoint) =>
            Math.Abs(targetPoint.x - Point.x + targetPoint.y - Point.y) == 1 ? 10 : 14;

        public Node UpdateGiven(Node potentialNewParent)
        {
            var potentialNewG = potentialNewParent.G + potentialNewParent.FindTheCostOfAdjacentMoveTo(Point);
            if (potentialNewG < G)
            {
                Parent = potentialNewParent;
                G = potentialNewG;
                F = potentialNewG + H;  // TODO: Do refactoring
            }

            return this;
        }


        public static Node CreateNodeWith(Node parent, (int x, int y) point, int h)
        {
            var g = parent.G + parent.FindTheCostOfAdjacentMoveTo(point);
            var f = g + h;

            return new Node(point, g, h, f, parent);
        }

        public static Node CreateTheStartingNodeWith((int x, int y) point) => 
            new Node(point, 0, 0, 0, null);
    }
}