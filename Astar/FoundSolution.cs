using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public class FoundSolution : ISearchResult
    {
        private FoundSolution(IEnumerable<(int x, int y)> value)
        {
            Value = value.ToList();
        }

        public IList<(int x, int y)> Value { get; }

        public static FoundSolution CreateSolutionFor(Node node) => 
            new FoundSolution(GetChainedNodesFrom(node).Reverse());


        private static IEnumerable<(int, int)> GetChainedNodesFrom(Node node)
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