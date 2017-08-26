using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
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
                _result.Should().Be(0);
        }

        public class When_the_target_point_is_just_above_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X, _sourcePoint.Y - 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_below_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X, _sourcePoint.Y + 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X - 1, _sourcePoint.Y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_just_at_the_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X + 1, _sourcePoint.Y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.Should().Be(10);
        }

        public class When_the_target_point_is_in_the_top_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X - 1, _sourcePoint.Y - 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.Should().Be(20);
        }

        public class When_the_target_point_is_in_the_top_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X + 1, _sourcePoint.Y - 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.Should().Be(20);
        }

        public class When_the_target_point_is_in_the_bottom_left_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X - 1, _sourcePoint.Y + 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.Should().Be(20);
        }

        public class When_the_target_point_is_in_the_bottom_right_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X + 1, _sourcePoint.Y + 1);

            [Test]
            public void Should_calculate_a_cost_of_20() =>
                _result.Should().Be(20);
        }

        public class When_the_target_point_is_far_in_the_same_horizontal_line_of_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X, _sourcePoint.Y + 100);

            [Test]
            public void Should_calculate_a_cost_of_points_straight_towards_the_target_by_10() =>
                _result.Should().Be(1000);
        }

        public class When_the_target_point_is_far_in_a_perfect_diagonal_line_from_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X + 100, _sourcePoint.Y + 100);

            [Test]
            public void Should_calculate_a_cost_of_the_double_of_the_diagonal_points_straight_towards_the_target_by_10() =>
                _result.Should().Be(2000);
        }

        public class When_the_target_point_is_far_in_an_irregular_direction_from_the_source_point : ManhattanMethodSpecs
        {
            public override void Given() =>
                _targetPoint = new Point(_sourcePoint.X + 5, _sourcePoint.Y + 3);

            [Test]
            public void Should_calculate_the_expected_cost() =>
                _result.Should().Be(80);
        }


        Point _targetPoint;
        int _result;

        static Point _sourcePoint = new Point(10, 10);
    }
}