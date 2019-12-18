using System;
using System.Diagnostics;

namespace AdventOfCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day1 = new Day1();
            PrintResult(day1);

            var day2 = new Day2();
            PrintResult(day2);

            var day3 = new Day3();
            PrintResult(day3);

            var day4 = new Day4();
            PrintResult(day4);

            Console.ReadLine();
        }

        private static void PrintResult(IPuzzleSolver day)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("{0}{1}{2}", "-----", day.GetName(), "-----");
            day.ParseInput();
            Console.WriteLine("Result part 1: ");
            day.SolvePartOne();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            PrintTimeElapsed(ts);

            stopWatch.Reset();

            stopWatch.Start();

            Console.WriteLine("Result part 2: ");
            day.SolvePartTwo();

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            PrintTimeElapsed(ts);
        }

        private static void PrintTimeElapsed(TimeSpan ts)
        {
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("");
        }
    }
}