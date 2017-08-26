using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Astar
{
    public static class PathFinder
    {
        public static ISearchResult FindShortestPath(Point startingPoint, Point destinationPoint, Predicate<Point> isWalkable, Func<Point, Point, int> estimateH)
        {
            GuardAgainstUnwakableStartingPoint(startingPoint, isWalkable);

            // Bootstrap
            var startingNode = Node.CreateTheStartingNodeWith(startingPoint);
            var openList = new SimplePriorityQueue<Point>();
            openList.Enqueue(startingPoint, 0);
            var nodesByPoint = new Dictionary<Point, Node> { {startingPoint, startingNode } };
            var closedSet = new HashSet<Point>();
            int EstimateHFromFor(Point point) => estimateH(point, destinationPoint);

            while (openList.Any())
            {
                var currentPoint = openList.Dequeue();
                var currentNode = nodesByPoint[currentPoint];
                if (currentPoint.Equals(destinationPoint))
                    return FoundSolution.CreateSolutionFor(currentNode);

                nodesByPoint.Remove(currentPoint);

                var availableAdjacentPoints = GetAvailableAdjacentPointsTo(currentPoint, isWalkable, closedSet);
                foreach (var adjacentPoint in availableAdjacentPoints)
                {
                    Node availableNode;
                    if (nodesByPoint.TryGetValue(adjacentPoint.Point, out availableNode))
                    {
                        availableNode.UpdateGiven(currentNode, adjacentPoint.FromParentMoveCost);
                        openList.UpdatePriority(adjacentPoint.Point, availableNode.F);
                    }
                    else
                    {
                        var h = EstimateHFromFor(adjacentPoint.Point);
                        availableNode = Node.CreateNodeWith(currentNode, adjacentPoint, h);
                        nodesByPoint.Add(adjacentPoint.Point, availableNode);
                        openList.Enqueue(adjacentPoint.Point, availableNode.F);
                    }
                }
                closedSet.Add(currentPoint);
            }
            return new NotFoundSolution();
        }

        internal static IEnumerable<(Point Point, int FromParentMoveCost)> GetAvailableAdjacentPointsTo(Point basePoint, Predicate<Point> isWalkable, ISet<Point> closedList)
        {
            // laterals
            var topPoint = new Point(basePoint.X, basePoint.Y - 1);
            var bottomPoint = new Point(basePoint.X, basePoint.Y + 1);
            var leftPoint = new Point(basePoint.X - 1, basePoint.Y);
            var rightPoint = new Point(basePoint.X + 1, basePoint.Y);

            var topIsWalkable = isWalkable(topPoint);
            var bottomIsWalkable = isWalkable(bottomPoint);
            var leftIsWalkable = isWalkable(leftPoint);
            var rightIsWalkable = isWalkable(rightPoint);

            if (topIsWalkable && !closedList.Contains(topPoint))
                yield return (topPoint, 10);
            if (rightIsWalkable && !closedList.Contains(rightPoint))
                yield return (rightPoint, 10);
            if (bottomIsWalkable && !closedList.Contains(bottomPoint))
                yield return (bottomPoint, 10);
            if (leftIsWalkable && !closedList.Contains(leftPoint))
                yield return (leftPoint, 10);

            // corners
            var topRightPoint = new Point(basePoint.X + 1, basePoint.Y - 1);
            var bottomRightPoint = new Point(basePoint.X + 1, basePoint.Y + 1);
            var bottomLeftPoint = new Point(basePoint.X - 1, basePoint.Y + 1);
            var topLeftPoint = new Point(basePoint.X - 1, basePoint.Y - 1);

            bool Comply(Point point) => isWalkable(point) && !closedList.Contains(point);
            if (topIsWalkable && rightIsWalkable && Comply(topRightPoint))
                yield return (topRightPoint, 14);
            if (rightIsWalkable && bottomIsWalkable && Comply(bottomRightPoint))
                yield return (bottomRightPoint, 14);
            if (leftIsWalkable && bottomIsWalkable && Comply(bottomLeftPoint))
                yield return (bottomLeftPoint, 14);
            if (leftIsWalkable && topIsWalkable && Comply(topLeftPoint))
                yield return (topLeftPoint, 14);
        }


        private static void GuardAgainstUnwakableStartingPoint(Point startingPoint, Predicate<Point> isWalkable)
        {
            if (!isWalkable(startingPoint))
                throw new Exception($"The starting position (x: {startingPoint.X}, y: {startingPoint.Y}) it is not a walkable point.");
        }
    }
}