using System.Collections.Generic;
using System.Linq;

namespace Astar.Specs
{
    public static class ObjectMother
    {
        public static Astar.Node CreateNodeWith(Point point)
        {
            return Astar.Node.CreateTheStartingNodeWith(point);
        }

        public static Astar.Node CreateNodeWith(Astar.Node parentNode, Point? point = null, (Point, int)? adjacentPoint = null, int? h = 0)
        {
            var actaulAdjacentPoint = adjacentPoint ?? (point == null ? (new Point(10, 10), 10) : (new Point(point.Value.X, point.Value.Y), 10));
            var node = Astar.Node.CreateNodeWith(parentNode, actaulAdjacentPoint, h ?? 0);

            return node;
        }

        public static IEnumerable<Point> CreatePointsFrom(params (int x, int y)[] rawPoints) =>
            rawPoints.Select(rawPoint => new Point(rawPoint.x, rawPoint.y));
    }
}