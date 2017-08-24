using NUnit.Framework;
using Shouldly;
using static Astar.Node;

namespace Astar.Specs.Node
{
    public class CreateNodeSpecs
    {
        public CreateNodeSpecs()
        {
            _parent = CreateTheStartingNodeWith((10, 10));
            _point = (11, 11);
            _result = CreateNodeWith(_parent, _point, _someHCost);
        }

        [Test]
        public void Should_have_the_same_former_point() =>
            _result.Point.ShouldBe(_point);

        [Test]
        public void Should_have_a_g_cost_equal_to_the_sum_of_the_g_cost_of_its_parent_and_the_cost_of_move_from_there() =>
            _result.G.ShouldBe(_parent.G + 14);

        [Test]
        public void Should_have_the_h_cost_produced_by_the_heuristic_function() =>
            _result.H.ShouldBe(_someHCost);

        [Test]
        public void Should_have_a_f_cost_equal_to_the_sum_of_g_and_h() =>
            _result.F.ShouldBe(_result.G + _result.H);

        [Test]
        public void Should_have_the_parent_provided() =>
            _result.Parent.ShouldBe(_parent);

        Astar.Node _parent;
        (int x, int y) _point;
        Astar.Node _result;

        static int _someHCost = 7;
    }
}