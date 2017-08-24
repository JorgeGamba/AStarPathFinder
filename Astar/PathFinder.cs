using System;
using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public static class PathFinder
    {
        public static ISearchResult FindShortestPath((int x, int y) startingPoint, (int x, int y) destinationPoint, Predicate<(int, int)> isWalkable, Func<(int x, int y), (int x, int y), int> estimateH)
        {
            if (!isWalkable(startingPoint))
                throw new Exception($"The starting position (x: {startingPoint.x}, y: {startingPoint.y}) it is not a walkable point.");

            // Bootstrap
            var startingNode = Node.CreateTheStartingNodeWith(startingPoint);
            var openSet = new Dictionary<(int, int), Node> { {startingPoint, startingNode } };
            var closedSet = new HashSet<(int x, int y)>();
            int EstimateHFromFor((int x, int y) point) => estimateH(point, destinationPoint);

            while (openSet.Any())
            {
                var current = GetCurrentFrom(openSet);
                if (current.Point.Equals(destinationPoint))
                    return FoundSolution.CreateSolutionFor(current.Node);

                openSet.Remove(current.Point);

                var availableAdjacentPoints = GetAvailableAdjacentPointsTo(current.Point, isWalkable, closedSet);
                foreach (var availablePoint in availableAdjacentPoints)
                {
                    Node availableNode;
                    var point = (availablePoint.x, availablePoint.y);
                    if (openSet.TryGetValue(point, out availableNode))
                    {
                        availableNode.UpdateGiven(current.Node, availablePoint.fromParentMoveCost);
                    }
                    else
                    {
                        var h = EstimateHFromFor(point);
                        availableNode = Node.CreateNodeWith(current.Node, availablePoint, h);
                        openSet.Add(point, availableNode);
                    }
                }
                closedSet.Add(current.Node.Point);
            }

            return new NotFoundSolution();
        }

        internal static ((int x, int y) Point, Node Node) GetCurrentFrom(IDictionary<(int, int), Node> openSet)
        {
            var lowestF = int.MaxValue;
            Node node = null;
            (int, int) point = (0, 0);
            foreach (var pair in openSet)
            {
                if (pair.Value.F < lowestF)
                {
                    node = pair.Value;
                    point = node.Point;
                    lowestF = pair.Value.F;
                }
            }
            return (point, node);
        }

        internal static IEnumerable<(int x, int y, int fromParentMoveCost)> GetAvailableAdjacentPointsTo((int x, int y) basePoint, Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList)
        {
            var up = isWalkable((basePoint.x, basePoint.y - 1));
            var down = isWalkable((basePoint.x, basePoint.y + 1));
            var left = isWalkable((basePoint.x - 1, basePoint.y));
            var right = isWalkable((basePoint.x + 1, basePoint.y));

            var list = new List<(int x, int y, int fromParentMoveCost)>();
            if (up)
                list.Add((basePoint.x, basePoint.y - 1, 10));
            if (right)
                list.Add((basePoint.x + 1, basePoint.y, 10));
            if (down)
                list.Add((basePoint.x, basePoint.y + 1, 10));
            if (left)
                list.Add((basePoint.x - 1, basePoint.y, 10));
            if (up && right)
                list.Add((basePoint.x + 1, basePoint.y - 1, 14));
            if (right && down)
                list.Add((basePoint.x + 1, basePoint.y + 1, 14));
            if (left && down)
                list.Add((basePoint.x - 1, basePoint.y + 1, 14));
            if (left && up)
                list.Add((basePoint.x - 1, basePoint.y - 1, 14));

            return list.Where(p => IsAvailable(isWalkable, closedList, (p.x, p.y)));
        }

    internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) point) => 
            isWalkable((point.x, point.y)) && !closedList.Contains((point.x, point.y));

        public static int FindTheCostOfAdjacentMove((int x, int y) basePoint, (int x, int y) adjacentPoint) =>
            Math.Abs(adjacentPoint.x - basePoint.x + adjacentPoint.y - basePoint.y) == 1 ? 10 : 14;
    }
}