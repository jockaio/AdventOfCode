using System;
using System.Collections.Generic;
using System.IO;
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
            //var input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83".Split("\n");
            //var input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split("\n");
            //var input = "R8,U5,L5,D3\nU7,R6,D4,L4".Split("\n");
            var input = File.ReadAllLines(@".\PuzzleInput\day3.txt").ToList();
            WireA = ParseWirePath(input[0].Split(',').ToList());
            WireB = ParseWirePath(input[1].Split(',').ToList());
        }

        public void SolvePartOne()
        {
            List<Intersection> intersections = GetIntersections();

            List<int> results = intersections.Select(x => Math.Abs(x.Position.Key) + Math.Abs(x.Position.Value)).ToList();
            results.Remove(0);

            Console.WriteLine(results.Min());
        }

        private List<Intersection> GetIntersections()
        {
            var coordinatesA = MapWire(WireA);
            var coordinatesB = MapWire(WireB);

            List<Intersection> intersections = FindIntersections(coordinatesA, coordinatesB);
            return intersections;
        }

        private List<Intersection> FindIntersections(List<Section> coordinatesA, List<Section> coordinatesB)
        {
            int totalA = 0, totalB = 0;
            List<Intersection> result = new List<Intersection>();

            foreach (var sectionA in coordinatesA)
            {
                totalB = 0;
                foreach (var sectionB in coordinatesB)
                {
                    if (sectionA.From.X == sectionA.To.X && sectionB.From.Y == sectionB.To.Y)
                    {
                        int min = sectionA.From.Y < sectionA.To.Y ? sectionA.From.Y : sectionA.To.Y;
                        int max = sectionA.From.Y > min ? sectionA.From.Y : sectionA.To.Y;

                        if (sectionB.From.Y >= min && sectionB.From.Y <= max)
                        {
                            int minB = sectionB.From.X < sectionB.To.X ? sectionB.From.X : sectionB.To.X;
                            int maxB = sectionB.From.X > minB ? sectionB.From.X : sectionB.To.X;

                            if (sectionA.From.X >= minB && sectionA.From.X <= maxB)
                            {
                                var crossingPosition = new KeyValuePair<int, int>(sectionA.From.X, sectionB.To.Y);

                                int crossingOffsetB = GetSteps(new Section
                                {
                                    From = sectionB.From,
                                    To = new Coordinate(crossingPosition.Key, crossingPosition.Value)
                                });

                                int crossingOffsetA = GetSteps(new Section
                                {
                                    From = sectionA.From,
                                    To = new Coordinate(crossingPosition.Key, crossingPosition.Value)
                                });

                                result.Add(
                                    new Intersection
                                    {
                                        Position = crossingPosition,
                                        TotalSteps = totalA + totalB + crossingOffsetB + crossingOffsetA
                                    });
                            }
                        }
                    }

                    if (sectionA.From.Y == sectionA.To.Y && sectionB.From.X == sectionB.To.X)
                    {
                        int min = sectionA.From.X < sectionA.To.X ? sectionA.From.X : sectionA.To.X;
                        int max = sectionA.From.X > min ? sectionA.From.X : sectionA.To.X;

                        if (sectionB.From.X >= min && sectionB.From.X <= max)
                        {
                            int minB = sectionB.From.Y < sectionB.To.Y ? sectionB.From.Y : sectionB.To.Y;
                            int maxB = sectionB.From.Y > minB ? sectionB.From.Y : sectionB.To.Y;

                            if (sectionA.From.Y >= minB && sectionA.From.Y <= maxB)
                            {
                                var crossingPosition = new KeyValuePair<int, int>(sectionB.To.X, sectionA.From.Y);

                                int crossingOffsetB = GetSteps(new Section
                                {
                                    From = sectionB.From,
                                    To = new Coordinate(crossingPosition.Key, crossingPosition.Value)
                                });

                                int crossingOffsetA = GetSteps(new Section
                                {
                                    From = sectionA.From,
                                    To = new Coordinate(crossingPosition.Key, crossingPosition.Value)
                                });

                                result.Add(
                                    new Intersection
                                    {
                                        Position = crossingPosition,
                                        TotalSteps = totalA + totalB + crossingOffsetB + crossingOffsetA
                                    });
                            }
                        }
                    }

                    totalB += GetSteps(sectionB);
                }

                totalA += GetSteps(sectionA);
            }

            return result;
        }

        private int GetSteps(Section section)
        {
            if (section.From.X == section.To.X)
            {
                return Math.Abs(section.From.Y - section.To.Y);
            }
            else
            {
                return Math.Abs(section.From.X - section.To.X);
            }
        }

        private List<Section> MapWire(List<KeyValuePair<string, int>> wire)
        {
            List<Section> result = new List<Section>();

            var x = 0;
            var y = 0;

            Section section;

            foreach (var pathSection in wire)
            {
                section = new Section();
                section.From = new Coordinate(x, y);

                switch (pathSection.Key)
                {
                    case "R":
                        MoveOnXAxis(ref x, y, pathSection.Value, section);
                        break;

                    case "L":
                        MoveOnXAxis(ref x, y, pathSection.Value * -1, section);
                        break;

                    case "U":
                        MoveOnYAxis(ref y, x, pathSection.Value, section);
                        break;

                    case "D":
                        MoveOnYAxis(ref y, x, pathSection.Value * -1, section);
                        break;

                    default:
                        break;
                }

                result.Add(section);
            }

            return result;
        }

        private void MoveOnXAxis(ref int x, int y, int value, Section section)
        {
            x += value;
            section.To = new Coordinate(x, y);
        }

        private void MoveOnYAxis(ref int y, int x, int value, Section section)
        {
            y += value;
            section.To = new Coordinate(x, y);
        }

        public void SolvePartTwo()
        {
            var intersections = GetIntersections();
            if (intersections.Any(x => x.Position.Key == 0 && x.Position.Value == 0))
            {
                intersections.Remove(intersections.Where(x => x.Position.Key == 0 && x.Position.Value == 0).First());
            }
            var smallestTotal = intersections.Min(x => x.TotalSteps);
            Console.WriteLine(smallestTotal);
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

    internal class Section
    {
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
    }

    internal class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    internal class Intersection
    {
        public int TotalSteps { get; set; }
        public KeyValuePair<int, int> Position { get; set; }
    }
}