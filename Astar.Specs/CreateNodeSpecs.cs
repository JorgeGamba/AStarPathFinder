using NUnit.Framework;
using Shouldly;
using static Astar.Functions;

namespace Astar.Specs
{
    public class CreateNodeSpecs
    {
        public CreateNodeSpecs()
        {
            int EstimateHFunc((int x, int y) anyStartingPoint) => _someHCost;
            _parent = new Node((10, 10), 15, 0, 0);
            _rawNode = (11, 11);
            _result = CreateNodeWith(EstimateHFunc, _parent, _rawNode);
        }

        [Test]
        public void Should_have_a_g_cost_equal_to_the_sum_of_the_g_cost_of_its_parent_and_the_cost_of_move_from_there() =>
            _result.G.ShouldBe(_parent.G + 14);

        [Test]
        public void Should_have_the_h_cost_produced_by_the_heuristic_function() =>
            _result.H.ShouldBe(_someHCost);

        [Test]
        public void Should_have_a_f_cost_equal_to_the_sum_of_g_and_h() =>
            _result.F.ShouldBe(_result.G + _result.H);

        Node _parent;
        (int x, int y) _rawNode;
        Node _result;

        static int _someHCost = 7;
    }
}