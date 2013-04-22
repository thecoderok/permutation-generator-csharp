using System;
using System.Collections.Generic;
using System.IO;
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
            // Check if there are any command line arguments
            if (args == null || args.Length == 0)
            {
                ShowUsage();
                return;
            }
            // If no arguments or argument is '/?' or 'help' or '?' or '-help' -Show help
            if (args.Length == 1 &&
                (args[0].Equals("/?") || args[0].Equals("?") ||
                 args[0].Equals("help", StringComparison.OrdinalIgnoreCase) ||
                 args[0].Equals("-help", StringComparison.OrdinalIgnoreCase)))
            {
                ShowUsage();
                return;
            }

            string inputFile = GetArgument(args, 0);
            if (string.IsNullOrWhiteSpace(inputFile))
            {
                ShowUsage();
                return;
            }

            string outputOption = GetArgument(args, 1);
            if (string.IsNullOrWhiteSpace(outputOption))
            {
                ShowUsage();
                return;
            }

            string outputFile = null;
            bool isPrintResults = true;

            if (!outputOption.Equals("-print", StringComparison.OrdinalIgnoreCase))
            {
                outputFile = outputOption;
                isPrintResults = false;
            }

            var delimiterParam = GetArgument(args, 2);
            var delimiter = PermutationGenerator.Generator.CsvDelimiter;
            if (!string.IsNullOrWhiteSpace(delimiterParam))
            {
                delimiter = delimiterParam;
            }

            var permutations = PermutationGenerator.Generator.GetPermutations(inputFile, delimiter);
            var results = ProcessResults(permutations, delimiter);
            // Output results or save to file
            if (isPrintResults)
            {
                Console.WriteLine("\r\nGenerated results:");
                foreach (var row in results)
                {
                    Console.WriteLine(row);
                }
            }
            else
            {
                File.WriteAllLines(outputFile, results);
            }

        }

        private static IEnumerable<string> ProcessResults(IEnumerable<IEnumerable<string>> input, string delimiter)
        {
            var resultsToSave = new List<string>();
            foreach (var row in input)
            {
                var line = new StringBuilder();
                foreach (var item in row)
                {
                    line.Append(item).Append(delimiter);
                }
                resultsToSave.Add(line.ToString());
            }

            return resultsToSave;
        }

        private static string GetArgument(IList<string> args, int index)
        {
            if (index > args.Count - 1)
            {
                return null;
            }

            return args[index];
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: PermutationsGenerator.exe <inputCsvFile> -print|<resultFileName> [<delimiter>]");
            Console.WriteLine("Samples:");
            Console.WriteLine("\tPrint permutation to screen:\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv -print");
            Console.WriteLine("\tSave permutation to file:\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv c:\\temp\\output.csv");
            Console.WriteLine("\tUse not standard delimiter (comma used by default):\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv c:\\temp\\output.csv ;");
        }
    }
}
