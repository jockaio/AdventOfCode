using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1 = new Day1();
            PrintResult(day1);

            var day2 = new Day2();
            PrintResult(day2);

            var day3 = new Day3();
            PrintResult(day3);

            Console.ReadLine();
        }

        static void PrintResult(IPuzzleSolver day)
        {
            Console.WriteLine(day.GetName());
            day.ParseInput();
            Console.WriteLine("Result part 1: ");
            day.SolvePartOne();
            Console.WriteLine("Result part 2: ");
            day.SolvePartTwo();
        }
    }
}
