using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day3 : IPuzzleSolver
    {
        private List<KeyValuePair<string, int>> WireA { get; set; }
        private List<KeyValuePair<string, int>> WireB { get; set; }

        public string GetName()
        {
            return "Day 3";
        }

        public void ParseInput()
        {
            var input = "R75, D30, R83, U83, L12, D49, R71, U7, L72\nU62,R66,U55,R34,D71,R55,D58,R83".Split("\n");
            //var input = File.ReadAllLines(@".\PuzzleInput\day3.txt").ToList();
            WireA = ParseWirePath(input[0].Split(',').ToList());
            WireB = ParseWirePath(input[1].Split(',').ToList());
        }

        public void SolvePartOne()
        {
            var coordinatesA = MapWire(WireA);
            var coordinatesB = MapWire(WireB);

            List<KeyValuePair<int, int>> intersections = FindIntersections(coordinatesA, coordinatesB);
        }

        private List<KeyValuePair<int, int>> FindIntersections(List<KeyValuePair<int, int>> coordinatesA, List<KeyValuePair<int, int>> coordinatesB)
        {
        }

        private List<KeyValuePair<int, int>> MapWire(List<KeyValuePair<string, int>> wire)
        {
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

            var x = 0;
            var y = 0;

            result.Add(new KeyValuePair<int, int>(x, y));

            foreach (var pathSection in wire)
            {
                switch (pathSection.Key)
                {
                    case "R":
                        MoveOnXAxis(ref x, y, pathSection.Value, result);
                        break;

                    case "L":
                        MoveOnXAxis(ref x, y, pathSection.Value * -1, result);
                        break;

                    case "U":
                        MoveOnYAxis(ref y, x, pathSection.Value, result);
                        break;

                    case "D":
                        MoveOnYAxis(ref y, x, pathSection.Value * -1, result);
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        private void MoveOnXAxis(ref int x, int y, int value, List<KeyValuePair<int, int>> result)
        {
            x += value;
            result.Add(new KeyValuePair<int, int>(x, y));
        }

        private void MoveOnYAxis(ref int y, int x, int value, List<KeyValuePair<int, int>> result)
        {
            y += value;
            result.Add(new KeyValuePair<int, int>(x, y));
        }

        public void SolvePartTwo()
        {
            throw new NotImplementedException();
        }

        private List<KeyValuePair<string, int>> ParseWirePath(List<string> input)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            foreach (var item in input)
            {
                result.Add(new KeyValuePair<string, int>(item.Substring(0, 1), int.Parse(item.Substring(1))));
            }

            return result;
        }
    }
}