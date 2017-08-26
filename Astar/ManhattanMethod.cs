using System;

namespace Astar
{
    public class ManhattanMethod
    {
        public static int CalculateHeuristicCostFor(Point sourcePoint, Point targetPoint) => 
            (Math.Abs(targetPoint.X - sourcePoint.X) + Math.Abs(targetPoint.Y - sourcePoint.Y)) * 10;
    }
}