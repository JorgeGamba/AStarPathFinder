using System;
using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public static class Functions
    {
        public static ISearchResult FindShortestPath((int x, int y) startingPoint, (int x, int y) destinationPoint, Predicate<(int, int)> isWalkable, Func<(int x, int y), (int x, int y), int> estimateH)
        {
            if (!isWalkable(startingPoint))
                throw new Exception($"The starting position (x: {startingPoint.x}, y: {startingPoint.y}) it is not a walkable node.");

            // Bootstrap
            var startingNode = Node.CreateTheStartingNodeWith(startingPoint);
            var pointsSortedByF = new SortedList<int, (int, int)> { { startingNode.F, startingPoint } };
            var openSet = new Dictionary<(int, int), Node> { {startingPoint, startingNode } };
            var closedSet = new HashSet<(int x, int y)>();
            int EstimateHFromFor((int x, int y) point) => estimateH(point, destinationPoint);

            while (openSet.Any())
            {
                var currentPoint = pointsSortedByF.First().Value;
                var currentNode = openSet[currentPoint];
                if (currentPoint.Equals(destinationPoint))
                    return FoundSolution.CreateSolutionFor(currentNode);

                pointsSortedByF.RemoveAt(0);
                openSet.Remove(currentPoint);

                var availableAdjacentPoints = currentNode.GetAdjacentPoints().Where(node => IsAvailable(isWalkable, closedSet, node)); // TODO: A well candidate for partial application
                foreach (var availablePoint in availableAdjacentPoints)
                {
                    if (openSet.TryGetValue(currentPoint, out var foundNode))
                    {
                        
                    }
                    else
                    {
                        var h = EstimateHFromFor(availablePoint);
                        openSet.Add(availablePoint, Node.CreateNodeWith(currentNode, availablePoint, h));
                    }
                }
                closedSet.Add(currentNode.Point);
            }

            return new NotFoundSolution();
        }
        

        internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) node) => 
            isWalkable(node) && !closedList.Contains(node);
    }
}