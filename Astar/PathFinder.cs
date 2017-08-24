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
                throw new Exception($"The starting position (x: {startingPoint.x}, y: {startingPoint.y}) it is not a walkable node.");

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

                var availableAdjacentPoints = current.Node.GetAdjacentPoints().Where(node => IsAvailable(isWalkable, closedSet, node)); // TODO: A well candidate for partial application
                foreach (var availablePoint in availableAdjacentPoints)
                {
                    Node availableNode;
                    if (openSet.TryGetValue(availablePoint, out availableNode))
                    {
                        availableNode.UpdateGiven(current.Node);
                    }
                    else
                    {
                        var h = EstimateHFromFor(availablePoint);
                        availableNode = Node.CreateNodeWith(current.Node, availablePoint, h);
                        openSet.Add(availablePoint, availableNode);
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
        

        internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) node) => 
            isWalkable(node) && !closedList.Contains(node);
    }
}