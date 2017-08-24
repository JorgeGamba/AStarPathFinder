using Doing.BDDExtensions;
using NUnit.Framework;
using Shouldly;
using static Astar.ManhattanMethod;

namespace Astar.Specs
{
    public class ManhattanMethodSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = CalculateHeuristicCostFor(_sourcePoint, _targetPoint);

        public class When_the_target_point_is_the_same_as_the_source : ManhattanMethodSpecs
        {
            public override void When() =>
                _targetPoint = _sourcePoint;

            [Test]
            public void Should_return_0() =>
                _result.ShouldBe(0);
        }

        public class When_the_target_point_is_just_above_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_below_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_at_the_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_at_the_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_in_the_top_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.ShouldBe(20);
        }

        public class When_the_target_point_is_in_the_top_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.ShouldBe(20);
        }

        public class When_the_target_point_is_in_the_bottom_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.ShouldBe(20);
        }

        public class When_the_target_point_is_in_the_bottom_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.ShouldBe(20);
        }

        public class When_the_target_point_is_far_in_the_same_horizontal_line_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x, _sourcePoint.y + 100);

            [Test]
            public void Should_calculate_a_cost_of_points_straight_towards_the_target_by_10() =>
                _result.ShouldBe(1000);
        }

        public class When_the_target_point_is_far_in_a_perfect_diagonal_line_from_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 100, _sourcePoint.y + 100);

            [Test]
            public void Should_calculate_a_cost_of_the_double_of_the_diagonal_points_straight_towards_the_target_by_10() =>
                _result.ShouldBe(2000);
        }

        public class When_the_target_point_is_far_in_an_irregular_direction_from_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 5, _sourcePoint.y + 3);

            [Test]
            public void Should_calculate_the_expected_cost() =>
                _result.ShouldBe(80);
        }


        (int x, int y) _targetPoint;
        int _result;

        static (int x, int y) _sourcePoint = (10, 10);
    }
}