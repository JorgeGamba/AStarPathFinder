using System;

namespace Astar
{
    public class Node
    {
        public Node((int, int) point, int g, int h, int f)
        {
            Point = point;
            G = g;
            H = h;
            F = f;
        }

        public (int x, int y) Point { get; }

        public int G { get; }

        public int H { get; }

        public int F { get; }
    }
}