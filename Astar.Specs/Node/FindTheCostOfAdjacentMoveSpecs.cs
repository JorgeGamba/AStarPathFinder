using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;

namespace Astar.Specs.Node
{
    public class FindTheCostOfAdjacentMoveSpecs : FeatureSpecifications
    {
        public override void When() => 
            _result = _someNode.FindTheCostOfAdjacentMoveTo(_targetPoint);

        public class When_the_target_point_is_just_above_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_below_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x - 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x + 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_in_the_top_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x - 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_top_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x + 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_bottom_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x - 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_bottom_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (FindTheCostOfAdjacentMoveSpecs._someNode.Point.x + 1, FindTheCostOfAdjacentMoveSpecs._someNode.Point.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }


        (int x, int y) _targetPoint;
        int _result;

        static Astar.Node _someNode = ObjectMother.CreateSomeNodeWith((10, 10));
    }
}