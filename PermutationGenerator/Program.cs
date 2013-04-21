using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    class Program
    {
        private static string filePath = @"C:\Users\vitalig\Documents\tst.csv";
        static void Main(string[] args)
        {
            var result = PermutationGenerator.Generator.GetPermutations(filePath, ";");
            foreach (var row in result)
            {
                foreach (var item in row)
                {
                    Console.Write(item + "; ");
                }
                Console.WriteLine();
            }
        }
    }
}
