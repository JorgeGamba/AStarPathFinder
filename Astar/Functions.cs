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

            var startingNode = Node.CreateTheStartingNodeWith(startingPoint);
            var openSet = new HashSet<Node> { startingNode };
            var closedSet = new HashSet<(int x, int y)>();
            int EstimateHFromFunc((int x, int y) point) => estimateH(point, destinationPoint);
            while (openSet.Any())
            {
                var currentNode = openSet.OrderBy(x => x.F).First();
                if (currentNode.Point.Equals(destinationPoint))
                    return FoundSolution.CreateSolutionFor(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode.Point);

                var availableAdjacentPoints = startingNode.GetAdjacentPoints().Where(node => IsAvailable(isWalkable, closedSet, node)); // TODO: A well candidate for partial application
                foreach (var adjacentPoint in availableAdjacentPoints)
                {
                    openSet.Add(Node.CreateNodeWith(EstimateHFromFunc, currentNode, adjacentPoint));
                }
                
            }

            return new NotFoundSolution();
        }
        

        internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) node) => 
            isWalkable(node) && !closedList.Contains(node);
    }
}