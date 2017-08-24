using Doing.BDDExtensions;
using NUnit.Framework;
using Shouldly;
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
             _node.UpdateGiven(_potentialNewParent);

        public class When_the_new_potential_g_is_less_than_the_current : UpdateSpecs
        {
            public override void Given()
            {
                _node = CreateNodeWith(_nodeWithTheHighestG, (12, 11), _someH);
                _potentialNewParent = _nodeWithTheLowestG;
            }

            [Test]
            public void Should_take_the_passed_potential_parent_as_its_new_parent() =>
                _node.Parent.ShouldBe(_potentialNewParent);

            [Test]
            public void Should_change_its_g_cost_to_its_parents_g_plus_the_adjacent_move_cost() =>
                _node.G.ShouldBe(_potentialNewParent.G + 10);

            [Test]
            public void Should_not_change_its_h_cost() =>
                _node.H.ShouldBe(_someH);

            [Test]
            public void Should_change_its_f_according_the_new_g() =>
                _node.F.ShouldBe(_potentialNewParent.G + 10 + _node.H);
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
                _node.Parent.ShouldBe(_nodeWithTheLowestG);

            [Test]
            public void Should_preserve_its_former_g_cost() =>
                _node.G.ShouldBe(_nodeWithTheLowestG.G + 10);

            [Test]
            public void Should_not_change_its_h_cost() =>
                _node.H.ShouldBe(_someH);

            [Test]
            public void Should_preserve_its_former_f() =>
                _node.F.ShouldBe(_nodeWithTheLowestG.G + 10 + _node.H);
        }


        Astar.Node _nodeWithTheHighestG;
        Astar.Node _nodeWithTheLowestG;
        Astar.Node _node;
        Astar.Node _potentialNewParent;
        int _someH = 100;
    }
}