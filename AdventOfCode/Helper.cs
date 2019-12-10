using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Helper
    {
        public static List<int> ParseToInt(List<string> stringInput)
        {
            List<int> result = new List<int>();
            stringInput.ForEach(x => result.Add(int.Parse(x)));

            return result;
        }
    }
}
