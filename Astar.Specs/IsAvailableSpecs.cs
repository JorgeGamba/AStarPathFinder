using System;
using System.Collections.Generic;
using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;
using static Astar.PathFinder;

namespace Astar.Specs
{
    public class IsAvailableSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = IsAvailable(_isWalkable, _closedList, _node);

        public class When_the_node_is_walkable : IsAvailableSpecs
        {
            public override void Given() =>
                _isWalkable = node => true;

            public class When_the_node_is_walkable_and_is_in_the_closed_list : When_the_node_is_walkable
            {
                public override void Given() =>
                    _closedList.Add(_node);

                [Test]
                public void Should_not_be_considered_fit_to_be_added() =>
                    _result.Should().BeFalse();
            }

            public class When_the_node_is_walkable_and_is_not_in_the_closed_list : When_the_node_is_walkable
            {
                //public override void Given() =>
                //    _closedList.Add(_node);

                [Test]
                public void Should_be_considered_fit_to_be_added() =>
                    _result.Should().BeTrue();
            }
        }

        public class When_the_node_is_unwalkable : IsAvailableSpecs
        {
            public override void Given() =>
                _isWalkable = node => false;

            public class When_the_node_is_unwalkable_but_is_in_the_closed_list : When_the_node_is_unwalkable
            {
                public override void Given() =>
                    _closedList.Add(_node);

                [Test]
                public void Should_not_be_considered_fit_to_be_added() =>
                    _result.Should().BeFalse();
            }

            public class When_the_node_is_unwalkable_and_is_not_in_the_closed_list : When_the_node_is_unwalkable
            {
                //public override void Given() =>
                //    _closedList.Add(_node);

                [Test]
                public void Should_not_be_considered_fit_to_be_added() =>
                    _result.Should().BeFalse();
            }
        }


        Predicate<(int, int)> _isWalkable;
        ISet<(int x, int y)> _openList = new HashSet<(int x, int y)>();
        ISet<(int x, int y)> _closedList = new HashSet<(int x, int y)>();
        bool _result;

        static (int x, int y) _node = (2, 3);
    }
}