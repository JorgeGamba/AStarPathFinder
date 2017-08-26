using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Astar.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowSolutionForMazeIn("Sample1(tsv).txt", new Point(31, 6), new Point(19, 17));
            ShowSolutionForMazeIn("Sample2(tsv).txt", new Point(31, 22), new Point(13, 11));
            ShowSolutionForMazeIn("Hound Maze(tsv).txt", new Point(60, 83), new Point(18, 26));
            Console.ReadKey();
        }

        private static void ShowSolutionForMazeIn(string fileName, Point startingPoint, Point destinationPoint)
        {
            var maze = new HashSet<Point>();
            using (var reader = new StreamReader(fileName))
            {
                string line;
                int y = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cells = line.Split((char) 9);
                    var x = 1;
                    foreach (var cell in cells)
                    {
                        if (cell == "F")
                            maze.Add(new Point(x, y));
                        x++;
                    }
                    y++;
                }
            }
            var solution = PathFinder.FindShortestPath(startingPoint, destinationPoint, IsWakablePredicateFactory.CreateIsWakablePredicateFrom(maze), ManhattanMethod.CalculateHeuristicCostFor);
            if (solution is FoundSolution positiveSolution)
            {
                var solutionText = $"[{string.Join(",", positiveSolution.Value.Select(value => $"[{value.X}, {value.Y}]"))}]";
                Console.Write($"The solution for the maze in the file '{fileName}' is: {solutionText}");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"It was not possible to found a solution for the maze in {fileName}");
            }
        }
    }
}
