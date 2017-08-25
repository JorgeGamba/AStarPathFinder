using NUnit.Framework;
using FluentAssertions;
using static Astar.Node;

namespace Astar.Specs.Node
{
    public class CreateTheStartingNodeSpecs
    {
        public CreateTheStartingNodeSpecs()
        {
            _point = (11, 11);
            _result = CreateTheStartingNodeWith(_point);
        }

        [Test]
        public void Should_have_the_same_former_point() =>
            _result.Point.Should().Be(_point);

        [Test]
        public void Should_have_a_g_cost_equal_to_0() =>
            _result.G.Should().Be(0);

        [Test]
        public void Should_have_a_h_cost_equal_to_0() =>
            _result.H.Should().Be(0);

        [Test]
        public void Should_have_a_f_cost_equal_to_0() =>
            _result.F.Should().Be(0);

        [Test]
        public void Should_have_no_parent() =>
            _result.Parent.Should().BeNull();

        (int x, int y) _point;
        Astar.Node _result;
    }
}