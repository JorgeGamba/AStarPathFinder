using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public class FoundSolution : ISearchResult
    {
        private FoundSolution(IEnumerable<Point> value)
        {
            Value = value.ToList();
        }

        public IList<Point> Value { get; }

        public static FoundSolution CreateSolutionFor(Node node) => 
            new FoundSolution(GetChainedNodesFrom(node).Reverse());


        private static IEnumerable<Point> GetChainedNodesFrom(Node node)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                yield return currentNode.Point;
                currentNode = currentNode.Parent;
            }
        }
    }
}