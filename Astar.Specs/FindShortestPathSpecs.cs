using System;
using Doing.BDDExtensions;
using NUnit.Framework;
using Shouldly;
using static Astar.Functions;

namespace Astar.Specs
{
    public class FindShortestPathSpecs : FeatureSpecifications
    {
        public override void When() => _exception = Catch.Exception(() =>
            _result = FindShortestPath(_startingPoint, _destinationPoint, _isWalkableFunc, _estimateHFunc)
        );

        public class When_the_starting_point_is_the_same_as_the_target_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = _startingPoint;
                _isWalkableFunc = _allIsWalkableFunc;
                _estimateHFunc = _anyEstimateHFunc;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.ShouldBeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution) _result).Value.ShouldBe(new[] {_startingPoint});
        }

        public class When_the_starting_point_is_not_a_walkable_node : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = _startingPoint;
                _isWalkableFunc = node => false;
                _estimateHFunc = _anyEstimateHFunc;
            }

            [Test]
            public void Should_throw_an_exception() =>
                _exception.ShouldNotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.ShouldBe($"The starting position (x: {_startingPoint.x}, y: {_startingPoint.y}) it is not a walkable node.");
        }

        public class When_there_is_no_way_to_reach_the_destination_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = (_startingPoint.x + 2, _startingPoint.y + 2);
                _isWalkableFunc = point => false;
                _estimateHFunc = _anyEstimateHFunc;
            }

            [Test]
            public void Should_return_a_not_found_result() =>
                _result.ShouldBeOfType<NotFoundSolution>();
        }

        public class When_the_destination_point_is_in_the_first_set_of_adjacent_points : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = (_startingPoint.x + 1, _startingPoint.y);
                _isWalkableFunc = _allIsWalkableFunc;
                _estimateHFunc = (point, destinationPoint) => point.Equals(destinationPoint) ? 1 : 2;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.ShouldBeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution)_result).Value.ShouldBe(new[] { _startingPoint, _destinationPoint });
        }

        public class When_the_destination_point_is_not_in_the_first_set_of_adjacent_points : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = (_startingPoint.x + 2, _startingPoint.y);
                _isWalkableFunc = _allIsWalkableFunc;
                _estimateHFunc = (point, destinationPoint) => point.Equals(destinationPoint) ? 1 : 20;
            }

            [Test]
            public void Should_return_a_positive_result() =>
                _result.ShouldBeOfType<FoundSolution>();

            [Test]
            public void Should_return_only_the_starting_point_as_the_solution() =>
                ((FoundSolution)_result).Value.ShouldBe(new[] { _startingPoint, _destinationPoint });
        }




        (int x, int y) _startingPoint;
        (int x, int y) _destinationPoint;
        Predicate<(int, int)> _isWalkableFunc;
        Func<(int x, int y), (int x, int y), int> _estimateHFunc;
        ISearchResult _result;
        Exception _exception;

        static (int, int) _someStartingPoint = (15, 23);
        static (int, int) _someDestinationPoint = (25, 33);
        static Predicate<(int, int)> _allIsWalkableFunc = node => true;
        static Func<(int x, int y), (int x, int y), int> _anyEstimateHFunc;
    }
}