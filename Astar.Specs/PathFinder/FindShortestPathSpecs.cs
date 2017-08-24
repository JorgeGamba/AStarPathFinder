using System;
using System.Linq;
using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;

namespace Astar.Specs.PathFinder
{
    public class FindShortestPathSpecs : FeatureSpecifications
    {
        public override void When() => _exception = Catch.Exception(() =>
            _result = Astar.PathFinder.FindShortestPath(_startingPoint, _destinationPoint, _isWalkableFunc, _anyEstimateHFunc)
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
                _exception.Message.Should().Be($"The starting position (x: {_startingPoint.x}, y: {_startingPoint.y}) it is not a walkable point.");
        }

        public class When_there_is_no_way_to_reach_the_destination_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = (_startingPoint.x + 2, _startingPoint.y + 2);
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
                _destinationPoint = (_startingPoint.x + 1, _startingPoint.y);
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
                _destinationPoint = (_startingPoint.x + 2, _startingPoint.y);
                _isWalkableFunc = _allIsWalkableFunc;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.Should().BeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution)_result).Value.Should().Equal(new[] { _startingPoint, (_startingPoint.x + 1, _startingPoint.y), _destinationPoint });
        }

        public class When_the_destination_point_is_on_the_other_side_of_a_wall : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = (_startingPoint.x + 4, _startingPoint.y + 1);
                var wall = new[]
                           {
                               (_startingPoint.x + 2, _someStartingPoint.y - 2),
                               (_startingPoint.x + 2, _someStartingPoint.y - 1),
                               (_startingPoint.x + 2, _someStartingPoint.y),
                               (_startingPoint.x + 2, _someStartingPoint.y + 1),
                               (_startingPoint.x + 2, _someStartingPoint.y + 2)
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
                        (_startingPoint.x + 1, _startingPoint.y + 1),
                        (_startingPoint.x + 1, _startingPoint.y + 2),
                        (_startingPoint.x + 1, _startingPoint.y + 3),
                        (_startingPoint.x + 2, _startingPoint.y + 3),
                        (_startingPoint.x + 3, _startingPoint.y + 3),
                        (_startingPoint.x + 4, _startingPoint.y + 2),
                        _destinationPoint
                    });
        }




        (int x, int y) _startingPoint;
        (int x, int y) _destinationPoint;
        Predicate<(int, int)> _isWalkableFunc;
        ISearchResult _result;
        Exception _exception;

        static (int x, int y) _someStartingPoint = (10, 10);
        static Predicate<(int, int)> _allIsWalkableFunc = node => true;
        static Func<(int x, int y), (int x, int y), int> _anyEstimateHFunc = (sourcePoint, targetPoint) => ManhattanMethod.CalculateHeuristicCostFor(sourcePoint, targetPoint);
    }
}