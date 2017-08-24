using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;
using static Astar.PathFinder;

namespace Astar.Specs.PathFinder
{
    public class FindTheCostOfAdjacentMoveSpecs : FeatureSpecifications
    {
        public override void When() => 
            _result = FindTheCostOfAdjacentMove(_basePoint, _adjacentPoint);

        public class When_the_target_point_is_just_above_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x, _basePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_below_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x, _basePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x - 1, _basePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x + 1, _basePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_in_the_top_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x - 1, _basePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_top_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x + 1, _basePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_bottom_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x - 1, _basePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }

        public class When_the_target_point_is_in_the_bottom_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _adjacentPoint = (_basePoint.x + 1, _basePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.Should().Be(14);
        }


        (int x, int y) _adjacentPoint;
        int _result;

        static (int x, int y) _basePoint = (10, 10);
    }
}