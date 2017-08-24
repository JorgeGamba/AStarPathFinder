namespace Astar.Specs.Node
{
    public static class ObjectMother
    {
        public static Astar.Node CreateNodeWith((int, int) point)
        {
            return Astar.Node.CreateTheStartingNodeWith(point);
        }

        public static Astar.Node CreateNodeWith(Astar.Node parentNode, (int x, int y)? point = null, (int, int, int)? adjacentPoint = null, int? h = 0)
        {
            var actaulAdjacentPoint = adjacentPoint ?? (point == null ? (10, 10, 10) : (point.Value.x, point.Value.y, 10));
            var node = Astar.Node.CreateNodeWith(parentNode, actaulAdjacentPoint, h ?? 0);

            return node;
        }
    }
}