namespace Astar
{
    public class Node
    {
        private Node(Point point, int g, int h, int f, Node parent)
        {
            Point = point;
            G = g;
            H = h;
            F = f;
            Parent = parent;
        }

        public Point Point { get; }

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


        public static Node CreateNodeWith(Node parent, (Point Point, int FromParentMoveCost) adjacentPoint, int h)
        {
            var g = parent.G + adjacentPoint.FromParentMoveCost;
            var f = g + h;

            return new Node(adjacentPoint.Point, g, h, f, parent);
        }

        public static Node CreateTheStartingNodeWith(Point point) => 
            new Node(point, 0, 0, 0, null);
    }
}