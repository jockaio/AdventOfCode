using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day2 : IPuzzleSolver
    {
        public List<int> Memory { get; set; }
        public string GetName()
        {
            return "Day 2";
        }

        public void ParseInput()
        {
            Memory = Helper.ParseToInt(File.ReadAllText(@".\PuzzleInput\day2.txt").Split(',').ToList());
        }

        public void SolvePartOne()
        {
            Console.WriteLine(ProcessMemory(new List<int>(Memory), 12, 2));
        }

        public void SolvePartTwo()
        {
            var solveFor = 19690720;
            var result = 0;
            int diff;
            var noun = 0;
            var verb = 0;

            do
            {
                diff = Math.Abs(result - solveFor);

                if (diff < 99 && diff != 0)
                {
                    noun--;
                    verb = diff;
                }

                //Console.WriteLine($"Noun: {noun}, Verb: {verb}, Result: {result}, Diff: {diff} ");
                result = ProcessMemory(new List<int>(Memory), noun, verb);

                if (result < solveFor)
                {
                    noun++;
                }

            } while (diff > Math.Abs(result - solveFor));

            Console.WriteLine(100 * noun + verb);
        }

        private int ProcessMemory(List<int> memory, int noun, int verb)
        {
            //initial one time thing
            memory[1] = noun;
            memory[2] = verb;

            int instructionIndex = 0;

            while (memory[instructionIndex] != 99)
            {
                ProcessInstruction(instructionIndex, memory);
                instructionIndex += 4;
            }

            //Console.WriteLine(memory[0].ToString("N0"));

            return memory[0];
        }

        private void ProcessInstruction(int startIndex, List<int> currentList)
        {
            var opCode = currentList[startIndex];
            var xValue = currentList[currentList[startIndex + 1]];
            var yValue = currentList[currentList[startIndex + 2]];
            var resultIndex = currentList[startIndex + 3];

            //Console.WriteLine($"OpCode: {opCode}, xValue: {xValue}, yValue: {yValue}, resultIndex: {resultIndex}");

            switch (opCode)
            {
                case 1:
                    currentList[resultIndex] = xValue + yValue;
                    break;
                case 2:
                    currentList[resultIndex] = xValue * yValue;
                    break;
                case 99:
                    Console.WriteLine("OpCode 99 found. First index value: " + currentList[0]);
                    break;
                default:
                    break;
            }
        }
    }
}