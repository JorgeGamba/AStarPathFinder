using Doing.BDDExtensions;
using NUnit.Framework;
using Shouldly;
using static Astar.Functions;

namespace Astar.Specs
{
    public class FindTheCostOfAdjacentMoveSpecs : FeatureSpecifications
    {
        public override void When() => 
            _result = FindTheCostOfAdjacentMove(_sourcePoint, _targetPoint);

        public class When_the_target_point_is_just_above_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_below_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_at_the_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_just_at_the_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y);

            [Test]
            public void Should_calculate_a_cost_of_10() =>
                _result.ShouldBe(10);
        }

        public class When_the_target_point_is_in_the_top_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.ShouldBe(14);
        }

        public class When_the_target_point_is_in_the_top_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y - 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.ShouldBe(14);
        }

        public class When_the_target_point_is_in_the_bottom_left_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x - 1, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.ShouldBe(14);
        }

        public class When_the_target_point_is_in_the_bottom_right_of_the_source_point : FindTheCostOfAdjacentMoveSpecs
        {
            public override void Given() =>
                _targetPoint = (_sourcePoint.x + 1, _sourcePoint.y + 1);

            [Test]
            public void Should_calculate_a_cost_of_14() =>
                _result.ShouldBe(14);
        }


        (int x, int y) _targetPoint;
        int _result;


        static (int x, int y) _sourcePoint = (10, 10);
    }
}