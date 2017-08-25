using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Astar.Specs.Node
{
    public class GetAdjacentPointsToSpecs
    {
        public GetAdjacentPointsToSpecs() => 
            _result = ObjectMother.CreateSomeNodeWith((10, 10)).GetAdjacentPoints();

        [Test]
        public void Should_return_8_items() =>
            _result.Count().Should().Be(8);

        [Test]
        public void Should_return_the_expectd_values() =>
            _result.Should().BeSubsetOf(new[] { (10, 9), (10, 11), (9, 10), (11, 10), (9, 9), (11, 11), (9, 11), (11, 9) });


        IEnumerable<(int x, int y)> _result;
    }
}