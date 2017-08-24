using System;

namespace Astar
{
    public class ManhattanMethod
    {
        public static int CalculateHeuristicCostFor((int x, int y) sourcePoint, (int x, int y) targetPoint) => 
            (Math.Abs(targetPoint.x - sourcePoint.x) + Math.Abs(targetPoint.y - sourcePoint.y)) * 10;
    }
}