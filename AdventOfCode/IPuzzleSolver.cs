using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IPuzzleSolver
    {
        string GetName();
        void ParseInput();
        void SolvePartOne();
        void SolvePartTwo();
    }
}
