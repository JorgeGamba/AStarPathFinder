using FluentAssertions;
using NUnit.Framework;
using static Astar.Node;

namespace Astar.Specs.Node
{
    [Category("Astar.Node.CreateNodeWith")]
    public class CreateNodeSpecs
    {
        public CreateNodeSpecs()
        {
            _parent = CreateTheStartingNodeWith(new Point(10, 10));
            _adjacentPoint = (new Point(11, 11), 14);
            _result = CreateNodeWith(_parent, _adjacentPoint, _someHCost);
        }

        [Test]
        public void Should_have_the_same_former_point() =>
            _result.Point.Should().Be(_adjacentPoint.Point);

        [Test]
        public void Should_have_a_g_cost_equal_to_the_sum_of_the_g_cost_of_its_parent_and_the_cost_of_move_from_there() =>
            _result.G.Should().Be(_parent.G + 14);

        [Test]
        public void Should_have_the_h_cost_produced_by_the_heuristic_function() =>
            _result.H.Should().Be(_someHCost);

        [Test]
        public void Should_have_a_f_cost_equal_to_the_sum_of_g_and_h() =>
            _result.F.Should().Be(_result.G + _result.H);

        [Test]
        public void Should_have_the_parent_provided() =>
            _result.Parent.Should().Be(_parent);

        Astar.Node _parent;
        (Point Point, int FromParentMoveCost) _adjacentPoint;
        Astar.Node _result;

        static int _someHCost = 7;
    }
}
