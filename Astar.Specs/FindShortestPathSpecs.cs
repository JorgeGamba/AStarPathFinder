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
            _result = FindShortestPath(_startingPoint, _destinationPoint, _isWalkableFunc)
        );

        public class When_the_starting_point_is_the_same_as_the_target_point : FindShortestPathSpecs
        {
            public override void Given()
            {
                _startingPoint = _someStartingPoint;
                _destinationPoint = _startingPoint;
                _isWalkableFunc = _anyIsWalkableFunc;
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
            }

            [Test]
            public void Should_throw_an_exception() =>
                _exception.ShouldNotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.ShouldBe($"The starting position (x: {_startingPoint.x}, y: {_startingPoint.y}) it is not a walkable node.");
        }




        (int x, int y) _startingPoint;
        (int x, int y) _destinationPoint;
        Predicate<(int, int)> _isWalkableFunc;
        ISearchResult _result;
        Exception _exception;

        static (int, int) _someStartingPoint = (15, 23);
        static (int, int) _someDestinationPoint = (25, 33);
        static Predicate<(int, int)> _anyIsWalkableFunc = node => true;
    }
}