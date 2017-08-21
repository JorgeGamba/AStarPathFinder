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

            return new FoundSolution(new[] {startingPoint});
        }
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