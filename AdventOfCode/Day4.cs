using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day4 : IPuzzleSolver
    {
        public List<int> Input { get; set; }

        public string GetName()
        {
            return "Day 4";
        }

        public void ParseInput()
        {
            Input = new List<int> { 138307, 654504 };
            //Input = new List<int> { 122244, 123444 };
        }

        public void SolvePartOne()
        {
            int combination = Input[0];
            int end = Input[1];
            int combinations = 0;

            int[] numbersArray;

            while (combination <= end)
            {
                numbersArray = GetNumbersArray(combination++);
                if (!AllIncreasingNumbers(numbersArray))
                {
                    continue;
                }

                if (!ContaisDoubleNumber(numbersArray, false))
                {
                    continue;
                }

                combinations++;
            }

            Console.WriteLine("Number of combinations: " + combinations);
        }

        private int[] GetNumbersArray(int number)
        {
            var numbers = new Stack<int>();

            for (; number > 0; number /= 10)
                numbers.Push(number % 10);

            return numbers.ToArray();
        }

        private bool AllIncreasingNumbers(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < numbers[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        private bool ContaisDoubleNumber(int[] numbers, bool partTwo)
        {
            int? validDouble = null;
            int low = 2;
            for (int i = 1; i < numbers.Length; i++)
            {
                if (validDouble != null && numbers[i] != validDouble)
                {
                    break;
                }
                if (numbers[i] == numbers[i - 1])
                {
                    if (!partTwo)
                    {
                        return true;
                    }
                    else
                    {
                        if (i >= low)
                        {
                            if (numbers[i - low] == numbers[i])
                            {
                                validDouble = null;
                                continue;
                            }
                        }

                        if (i + 1 < numbers.Length - 1)
                        {
                            if (numbers[i] == numbers[i + 1])
                            {
                                validDouble = null;
                                continue;
                            }
                        }
                        validDouble = numbers[i];
                    }
                }
            }

            return validDouble.HasValue;
        }

        public void SolvePartTwo()
        {
            //Test();
            int combination = Input[0];
            int end = Input[1];
            int combinations = 0;

            int[] numbersArray;

            while (combination <= end)
            {
                numbersArray = GetNumbersArray(combination++);
                if (!AllIncreasingNumbers(numbersArray))
                {
                    continue;
                }

                if (!ContaisDoubleNumber(numbersArray, true))
                {
                    continue;
                }

                combinations++;
            }

            Console.WriteLine("Number of combinations: " + combinations);
        }

        public void Test()
        {
            //int testNumber = 579999;
            //int testNumber = 578999;
            int testNumber = 578899;
            var numbersArray = GetNumbersArray(testNumber);
            Console.WriteLine(testNumber + " contains valid double numbers: " + ContaisDoubleNumber(numbersArray, true));
        }
    }
}