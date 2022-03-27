using System.Collections.Generic;

namespace A_SearchInABrokenArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var res = Solution.BrokenSearch(new List<int> { 1,5}, 1);
            System.Console.WriteLine(res);
        }
    }
}
