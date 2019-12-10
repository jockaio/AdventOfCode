using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day3 : IPuzzleSolver
    {
        List<KeyValuePair<string, int>> WireA { get; set; }
        List<KeyValuePair<string, int>> WireB { get; set; }
        public string GetName()
        {
            return "Day 3";
        }

        public void ParseInput()
        {
            var input = File.ReadAllLines(@".\PuzzleInput\day3.txt").ToList();
            WireA = ParseWirePath(input[0].Split(',').ToList());
            WireB = ParseWirePath(input[1].Split(',').ToList());
        }

        public void SolvePartOne()
        {
            var coordinatesA = MapWire(WireA);
            var coordinatesB = MapWire(WireB);
            var intersections = new List<KeyValuePair<int, int>>();

            foreach (var item in coordinatesA)
            {
                if (coordinatesB.Exists(x => x.Key == item.Key && x.Value == item.Value))
                {
                    intersections.Add(item);
                }
            }
        }

        private List<KeyValuePair<int, int>> MapWire(List<KeyValuePair<string, int>> wire)
        {
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

            var x = 0;
            var y = 0;

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
            int point;
            for (int i = x; i < Math.Abs(value); i++)
            {
                point = value < 0 ? i * -1 : i; 
                result.Add(new KeyValuePair<int, int>(point, y));
            }
            x += value;
        }

        private void MoveOnYAxis(ref int y, int x, int value, List<KeyValuePair<int, int>> result)
        {
            int point;
            for (int i = y; i < Math.Abs(value); i++)
            {
                point = value < 0 ? i * -1 : i;
                result.Add(new KeyValuePair<int, int>(x, point));
            }
            y += value;
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
