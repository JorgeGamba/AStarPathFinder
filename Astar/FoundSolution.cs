using System.Collections.Generic;
using System.Linq;

namespace Astar
{
    public class FoundSolution : ISearchResult
    {
        public FoundSolution(IEnumerable<(int x, int y)> value)
        {
            Value = value.ToList();
        }

        public IList<(int x, int y)> Value { get; }
    }
}