using System;
using System.Collections.Generic;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using static Astar.IsWakablePredicateFactory;

namespace Astar.Specs
{
    [Category("IsWakablePredicateFactory")]
    public class IsWakablePredicateFactorySpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = _predicate(_point);

        public class When_the_provided_point_is_not_recognized : IsWakablePredicateFactorySpecs
        {
            public override void Given()
            {
                _predicate = CreateIsWakablePredicateFrom(new HashSet<Point>());
                _point = _somePoint;
            }

            [Test]
            public void Should_indicate_that_it_is_not_walkable() =>
                _result.Should().BeFalse();
        }

        public class When_the_provided_point_is_recognized : IsWakablePredicateFactorySpecs
        {
            public override void Given()
            {
                _predicate = CreateIsWakablePredicateFrom(new HashSet<Point> { _somePoint});
                _point = _somePoint;
            }

            [Test]
            public void Should_indicate_that_it_is_walkable() =>
                _result.Should().BeTrue();
        }


        Predicate<Point> _predicate;
        Point _point;
        bool _result;

        static Point _somePoint = new Point(10, 10);
    }
}