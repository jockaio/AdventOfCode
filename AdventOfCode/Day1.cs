using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day1 : IPuzzleSolver
    {
        public List<int> Input { get; set; }
        public string GetName()
        {
            return "Day 1";
        }

        public void ParseInput()
        {
            Input = Helper.ParseToInt(File.ReadAllLines(@".\PuzzleInput\day1.txt").ToList());
        }

        public void SolvePartOne()
        {
            var result = Input.Sum(x => (x / 3) - 2);

            Console.WriteLine(result);
        }

        public void SolvePartTwo()
        {
            var resultCollection = new List<int>();

            Input.ForEach(x => resultCollection.Add(CalculateFuel(x)));

            Console.WriteLine(resultCollection.Sum());
        }

        private int CalculateFuel(int input)
        {
            var calculation = input / 3 - 2;
            if (calculation > 0)
            {
                return calculation + CalculateFuel(calculation);
            }

            return 0;
        }
    }
}
