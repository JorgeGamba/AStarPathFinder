using System;
using System.Collections.Generic;
using System.Linq;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using static Astar.Specs.ObjectMother;

namespace Astar.Specs.PathFinder
{
    [Category("Astar.PathFinder.GetAvailableAdjacentPointsTo")]
    public class GetAvailableAdjacentPointsSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = Astar.PathFinder.GetAvailableAdjacentPointsTo(new Point(10, 10), _isWalkableFunc, _closedList);

        public class When_all_adjacent_points_are_available : GetAvailableAdjacentPointsSpecs
        {
            public override void Given()
            {
                _isWalkableFunc = point => true;
                _closedList = _emptyClosedList;
            }

            [Test]
            public void Should_return_8_elements() =>
                _result.Count().Should().Be(8);

            [Test]
            public void Should_return_all_the_adjacent_possible_points() =>
                _result.Select(x => x.Point).Should().BeEquivalentTo(CreatePointsFrom((10, 9), (10, 11), (9, 10), (11, 10), (9, 9), (11, 11), (9, 11), (11, 9)));

            [Test]
            public void Should_have_10_as_the_move_cost_for_each_lateral_point() =>
                GetResultLateralAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 10).Should().BeTrue();

            [Test]
            public void Should_have_14_as_the_move_cost_for_each_corner_point() =>
                GetResultDiagonalAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 14).Should().BeTrue();
        }

        public class When_a_point_is_unwalkable : GetAvailableAdjacentPointsSpecs
        {
            public override void Given()
            {
                _isWalkableFunc = point => !point.Equals(new Point(10, 9));
                _closedList = _emptyClosedList;
            }

            [Test]
            public void Should_return_7_elements() =>
                _result.Count().Should().Be(5);

            [Test]
            public void Should_not_return_the_unwalkable_point() =>
                _result.Should().NotContain((new Point(10, 9), 10));

            [Test]
            public void Should_not_return_the_cutting_corners_points() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((9, 9), (11, 9)));

            [Test]
            public void Should_return_all_other_points() =>
                _result.Select(x => x.Point).Should().BeEquivalentTo(CreatePointsFrom((10, 11), (9, 10), (11, 10), (11, 11), (9, 11)));

            [Test]
            public void Should_have_10_as_the_move_cost_for_each_lateral_point() =>
                GetResultLateralAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 10).Should().BeTrue();

            [Test]
            public void Should_have_14_as_the_move_cost_for_each_corner_point() =>
                GetResultDiagonalAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 14).Should().BeTrue();
        }

        public class When_multiple_points_are_unwalkable : GetAvailableAdjacentPointsSpecs
        {
            public override void Given()
            {
                _isWalkableFunc = point => !CreatePointsFrom((10, 9), (11, 9), (9, 10)).Contains(point);
                _closedList = _emptyClosedList;
            }

            [Test]
            public void Should_return_only_the_number_of_walkable_elements() =>
                _result.Count().Should().Be(3);

            [Test]
            public void Should_not_return_the_unwalkable_point() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((10, 9), (11, 9), (9, 10)));

            [Test]
            public void Should_not_return_the_cutting_corners_points() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((9, 9), (11, 9), (9, 11)));

            [Test]
            public void Should_return_all_other_points() =>
                _result.Select(p => p.Point).Should().Contain(CreatePointsFrom((10, 11), (11, 10), (11, 11)));

            [Test]
            public void Should_have_10_as_the_move_cost_for_each_lateral_point() =>
                GetResultLateralAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 10).Should().BeTrue();

            [Test]
            public void Should_have_14_as_the_move_cost_for_each_corner_point() =>
                GetResultDiagonalAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 14).Should().BeTrue();
        }

        public class When_some_points_are_walkable_but_they_are_in_the_closed_list : GetAvailableAdjacentPointsSpecs
        {
            public override void Given()
            {
                _isWalkableFunc = point => !CreatePointsFrom((11,11), (9, 10)).Contains(point);
                _closedList = new HashSet<Point> {new Point(11, 10), new Point(9, 11)};
            }

            [Test]
            public void Should_return_only_the_number_of_walkable_and_not_closed_elements() =>
                _result.Count().Should().Be(3);

            [Test]
            public void Should_not_return_the_unwalkable_point() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((11, 11), (9, 10)));

            [Test]
            public void Should_not_return_the_closed_point() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((11, 10), (9, 11)));

            [Test]
            public void Should_not_return_the_cutting_corners_points() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((9, 9), (9, 11)));

            [Test]
            public void Should_return_all_other_points() =>
                _result.Select(p => p.Point).Should().Contain(CreatePointsFrom((10, 9), (10, 11), (11, 9)));

            [Test]
            public void Should_have_10_as_the_move_cost_for_each_lateral_point() =>
                GetResultLateralAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 10).Should().BeTrue();

            [Test]
            public void Should_have_14_as_the_move_cost_for_each_corner_point() =>
                GetResultDiagonalAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 14).Should().BeTrue();
        }

        public class When_some_points_are_walkable_and_they_are_not_in_the_closed_list_but_they_would_involve_cutting_a_corner : GetAvailableAdjacentPointsSpecs
        {
            public override void Given()
            {
                _isWalkableFunc = point => !CreatePointsFrom((10, 9), (9, 10)).Contains(point);
                _closedList = new HashSet<Point> {new Point(11, 10)};
            }

            [Test]
            public void Should_return_only_the_number_of_walkable_and_not_closed_elements_and_not_cutting_corners_points() =>
                _result.Count().Should().Be(2);

            [Test]
            public void Should_not_return_the_unwalkable_point() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((10, 9), (9, 10)));

            [Test]
            public void Should_not_return_the_closed_point() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((11, 10)));

            [Test]
            public void Should_not_return_the_cutting_corners_points() =>
                _result.Select(p => p.Point).Should().NotContain(CreatePointsFrom((9, 9), (9, 11), (11, 9)));

            [Test]
            public void Should_return_all_other_points() =>
                _result.Select(p => p.Point).Should().Contain(CreatePointsFrom((10, 11), (11, 11)));

            [Test]
            public void Should_have_10_as_the_move_cost_for_each_lateral_point() =>
                GetResultLateralAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 10).Should().BeTrue();

            [Test]
            public void Should_have_14_as_the_move_cost_for_each_corner_point() =>
                GetResultDiagonalAdjacentPoints().All(adjacentPoint => adjacentPoint.FromParentMoveCost == 14).Should().BeTrue();
        }

        IEnumerable<(Point, int FromParentMoveCost)> GetResultLateralAdjacentPoints() =>
            _result.Where(adjacentPoint => Math.Abs(adjacentPoint.Point.X - 10 + adjacentPoint.Point.Y - 10) == 1);

        IEnumerable<(Point, int FromParentMoveCost)> GetResultDiagonalAdjacentPoints() =>
            _result.Where(adjacentPoint => Math.Abs(adjacentPoint.Point.X - 10 + adjacentPoint.Point.Y - 10) == 2);


        Predicate<Point> _isWalkableFunc;
        ISet<Point> _closedList;
        IEnumerable<(Point Point, int FromParentMoveCost)> _result;

        static Point _basePoint = new Point(10, 10);
        static HashSet<Point> _emptyClosedList = new HashSet<Point>();
    }
}