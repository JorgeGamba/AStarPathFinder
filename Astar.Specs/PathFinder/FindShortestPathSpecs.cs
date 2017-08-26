using System;
using System.Linq;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using static Astar.PathFinder;

namespace Astar.Specs.PathFinder
{
    [Category("Astar.PathFinder.FindShortestPath")]
    public class FindShortestPathSpecs : FeatureSpecifications
    {
        public override void When() => _exception = Catch.Exception(() =>
            _result = FindShortestPath(_startingPoint, _destinationPoint, _isWalkableFunc, _anyEstimateHFunc)
        );

        public class When_the_starting_point_is_the_same_as_the_target_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = _startingPoint;
                _isWalkableFunc = _allIsWalkableFunc;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.Should().BeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution) _result).Value.Should().Equal(_startingPoint);
        }

        public class When_the_starting_point_is_not_a_walkable_node : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = _startingPoint;
                _isWalkableFunc = node => false;
            }

            [Test]
            public void Should_throw_an_exception() =>
                _exception.Should().NotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.Should().Be($"The starting position (x: {_startingPoint.X}, y: {_startingPoint.Y}) it is not a walkable point.");
        }

        public class When_there_is_no_way_to_reach_the_destination_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = new Point(_startingPoint.X + 2, _startingPoint.Y + 2);
                _isWalkableFunc = point => point.Equals(_startingPoint);
            }

            [Test]
            public void Should_return_a_not_found_result() =>
                _result.Should().BeOfType<NotFoundSolution>();
        }

        public class When_the_destination_point_is_in_the_first_set_of_adjacent_points : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = new Point(_startingPoint.X + 1, _startingPoint.Y);
                _isWalkableFunc = _allIsWalkableFunc;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.Should().BeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution)_result).Value.Should().Equal(_startingPoint, _destinationPoint);
        }

        public class When_the_destination_point_is_not_in_the_first_set_of_adjacent_points : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = new Point(_startingPoint.X + 2, _startingPoint.Y);
                _isWalkableFunc = _allIsWalkableFunc;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.Should().BeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution)_result).Value.Should().Equal(_startingPoint, new Point(_startingPoint.X + 1, _startingPoint.Y), _destinationPoint);
        }

        public class When_the_destination_point_is_on_the_other_side_of_a_wall : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = new Point(_startingPoint.X + 4, _startingPoint.Y + 1);
                var wall = new[]
                           {
                               new Point(_startingPoint.X + 2, _someStartingPoint.Y - 2),
                               new Point(_startingPoint.X + 2, _someStartingPoint.Y - 1),
                               new Point(_startingPoint.X + 2, _someStartingPoint.Y),
                               new Point(_startingPoint.X + 2, _someStartingPoint.Y + 1),
                               new Point(_startingPoint.X + 2, _someStartingPoint.Y + 2)
                           };
                _isWalkableFunc = point => !wall.Contains(point);
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.Should().BeOfType<FoundSolution>();

            [Test]
            public void Should_return_the_expected_path() =>
                ((FoundSolution) _result).Value.Should().BeEquivalentTo(
                    new[]
                    {
                        _startingPoint,
                        new Point(_startingPoint.X + 1, _startingPoint.Y + 1),
                        new Point(_startingPoint.X + 1, _startingPoint.Y + 2),
                        new Point(_startingPoint.X + 1, _startingPoint.Y + 3),
                        new Point(_startingPoint.X + 2, _startingPoint.Y + 3),
                        new Point(_startingPoint.X + 3, _startingPoint.Y + 3),
                        new Point(_startingPoint.X + 4, _startingPoint.Y + 2),
                        _destinationPoint
                    });
        }




        Point _startingPoint;
        Point _destinationPoint;
        Predicate<Point> _isWalkableFunc;
        ISearchResult _result;
        Exception _exception;

        static Point _someStartingPoint = new Point(10, 10);
        static Predicate<Point> _allIsWalkableFunc = node => true;
        static Func<Point, Point, int> _anyEstimateHFunc = (sourcePoint, targetPoint) => ManhattanMethod.CalculateHeuristicCostFor(sourcePoint, targetPoint);
    }
}