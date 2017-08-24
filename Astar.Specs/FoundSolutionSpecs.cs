using System.Linq;
using Astar.Specs.Node;
using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;
using static Astar.FoundSolution;

namespace Astar.Specs
{
    public class FoundSolutionSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = CreateSolutionFor(_node);

        public class When_the_solution_is_just_the_starting_point : FoundSolutionSpecs
        {
            public override void Given() =>
                _node = Astar.Node.CreateTheStartingNodeWith((10, 10));

            [Test]
            public void Should_contain_only_one_element() =>
                _result.Value.Count.Should().Be(1);

            [Test]
            public void Should_contain_only_the_exactly_former_starting_point() =>
                _result.Value.Should().Equal(_node.Point);
        }

        public class When_the_solution_is_two_elements : FoundSolutionSpecs
        {
            public override void Given()
            {
                var parentNode = Astar.Node.CreateTheStartingNodeWith(_someStartingPoint);
                _node = ObjectMother.CreateNodeWith(parentNode, _someDestinationPoint);
            }

            [Test]
            public void Should_contain_only_two_elements() =>
                _result.Value.Count.Should().Be(2);

            [Test]
            public void Should_the_first_element_be_the_starting_point() =>
                _result.Value.First().Should().Be(_someStartingPoint);

            [Test]
            public void Should_the_last_element_be_the_destination_point() =>
                _result.Value.Last().Should().Be(_someDestinationPoint);
        }

        public class When_the_solution_has_multiple_elements : FoundSolutionSpecs
        {
            public override void Given()
            {
                var startingNode = Astar.Node.CreateTheStartingNodeWith(_someStartingPoint);
                var middleNode = ObjectMother.CreateNodeWith(startingNode, _someMiddlePoint);
                _node = ObjectMother.CreateNodeWith(middleNode, _someDestinationPoint);
            }

            [Test]
            public void Should_contain_the_total_of_chained_nodes() =>
                _result.Value.Count.Should().Be(3);

            [Test]
            public void Should_the_first_element_be_the_starting_point() =>
                _result.Value.First().Should().Be(_someStartingPoint);

            [Test]
            public void Should_have_all_the_solution_elements_ordered_as_they_were_added() =>
                _result.Value.Should().Equal(_someStartingPoint, _someMiddlePoint, _someDestinationPoint);

            [Test]
            public void Should_the_last_element_be_the_destination_point() =>
                _result.Value.Last().Should().Be(_someDestinationPoint);


            static (int, int) _someMiddlePoint = (10, 11);
        }

        Astar.Node _node;
        FoundSolution _result;

        static int _someEstimatedH = 1;
        static (int, int) _someStartingPoint = (10, 10);
        static (int, int) _someDestinationPoint = (11, 10);
    }
}