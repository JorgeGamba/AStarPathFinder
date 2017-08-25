using Doing.BDDExtensions;
using NUnit.Framework;
using FluentAssertions;
using static Astar.Node;

namespace Astar.Specs.Node
{
    public class UpdateSpecs : FeatureSpecifications
    {
        public override void Given()
        {
            var startingNode = CreateTheStartingNodeWith((10, 10));
            _nodeWithTheHighestG = CreateNodeWith(CreateNodeWith(startingNode, (11, 11), _someH), (12, 12), _someH); // Becauses its double diagonal move (G = 14 + 14)
            _nodeWithTheLowestG = CreateNodeWith(CreateNodeWith(startingNode, (11, 10), _someH), (12, 10), _someH);  // Becauses its double horizontal move (G = 10 + 10)
        }

        public override void When() =>
            _result = _node.UpdateGiven(_potentialNewParent);

        public class When_the_new_potential_g_is_less_than_the_current : UpdateSpecs
        {
            public override void Given()
            {
                _node = CreateNodeWith(_nodeWithTheHighestG, (12, 11), _someH);
                _potentialNewParent = _nodeWithTheLowestG;
            }

            [Test]
            public void Should_take_the_passed_potential_parent_as_its_new_parent() =>
                _result.Parent.Should().Be(_potentialNewParent);

            [Test]
            public void Should_change_its_g_cost_to_its_parents_g_plus_the_adjacent_move_cost() =>
                _result.G.Should().Be(_potentialNewParent.G + 10);

            [Test]
            public void Should_not_change_its_h_cost() =>
                _result.H.Should().Be(_someH);

            [Test]
            public void Should_change_its_f_according_the_new_g() =>
                _result.F.Should().Be(_potentialNewParent.G + 10 + _result.H);
        }

        public class When_the_new_potential_g_is_greater_than_the_current : UpdateSpecs
        {
            public override void Given()
            {
                _node = CreateNodeWith(_nodeWithTheLowestG, (12, 11), _someH);
                _potentialNewParent = _nodeWithTheHighestG;
            }

            [Test]
            public void Should_preserve_its_former_parent() =>
                _result.Parent.Should().Be(_nodeWithTheLowestG);

            [Test]
            public void Should_preserve_its_former_g_cost() =>
                _result.G.Should().Be(_nodeWithTheLowestG.G + 10);

            [Test]
            public void Should_not_change_its_h_cost() =>
                _result.H.Should().Be(_someH);

            [Test]
            public void Should_preserve_its_former_f() =>
                _result.F.Should().Be(_nodeWithTheLowestG.G + 10 + _result.H);
        }


        Astar.Node _nodeWithTheHighestG;
        Astar.Node _nodeWithTheLowestG;
        Astar.Node _node;
        Astar.Node _potentialNewParent;
        Astar.Node _result;
        int _someH = 100;
    }
}