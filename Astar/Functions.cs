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

            var currentNode = Node.CreateTheStartingNodeWith(startingPoint);
            var openSet = new HashSet<Node> { currentNode };
            var closedSet = new HashSet<(int x, int y)>();
            var solution = new List<(int x, int y)>() {startingPoint};
            var availableAdjacentPoints = currentNode.GetAdjacentPoints().Where(node => IsAvailable(isWalkable, closedSet, node)); // TODO: A well candidate for partial application
            foreach (var adjacentPoint in availableAdjacentPoints)
            {
                if (adjacentPoint.Equals(destinationPoint))
                {
                    solution.Add(adjacentPoint);
                    break;
                }
            }

            return new FoundSolution(solution);
        }
        

        internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) node) => 
            isWalkable(node) && !closedList.Contains(node);
    }
}