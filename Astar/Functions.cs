using System;
using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public static class Functions
    {
        public static ISearchResult FindShortestPath((int x, int y) startingPoint, (int x, int y) destinationPoint, Predicate<(int, int)> isWalkable)
        {
            if (!isWalkable(startingPoint))
                throw new Exception($"The starting position (x: {startingPoint.x}, y: {startingPoint.y}) it is not a walkable node.");

            var closedList = new HashSet<(int x, int y)>();
            var availableAdjacentNodes = GetAdjacentNodesTo(startingPoint).Where(node => IsAvailable(isWalkable, closedList, node));

            return new FoundSolution(new[] {startingPoint});
        }

        internal static IEnumerable<(int x, int y)> GetAdjacentNodesTo((int x, int y) node) =>
            new[]
            {
                (node.x, node.y + 1),
                (node.x, node.y - 1),
                (node.x + 1, node.y),
                (node.x - 1, node.y),
                (node.x - 1, node.y - 1),
                (node.x - 1, node.y + 1),
                (node.x + 1, node.y - 1),
                (node.x + 1, node.y + 1)
            };

        internal static bool IsAvailable(Predicate<(int, int)> isWalkable, ISet<(int x, int y)> closedList, (int x, int y) node) => 
            isWalkable(node) && !closedList.Contains(node);

        internal static int EstimateG((int x, int y) sourcePoint, (int x, int y) targetPoint) => 
            Math.Abs(targetPoint.x - sourcePoint.x + targetPoint.y - sourcePoint.y) == 1 ? 10 : 14;
    }

    public class FoundSolution : ISearchResult
    {
        public FoundSolution(IEnumerable<(int x, int y)> value)
        {
            Value = value.ToList();
        }

        public IList<(int x, int y)> Value { get; }
    }

    public interface ISearchResult
    {
    }
}