namespace Astar.Specs.Node
{
    public static class ObjectMother
    {
        public static Astar.Node CreateSomeNodeWith((int, int) point)
        {
            return Astar.Node.CreateTheStartingNodeWith(point);
        }
    }
}