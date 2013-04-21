using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PermutationGenerator
{
    /// <summary>
    /// Class to generate permutations for data defined in CSV file
    /// </summary>
    public static class Generator
    {
        private const string CsvDelimiter = ",";

        public static IEnumerable<IEnumerable<string>> GetPermutations(string pathToCsvFile, string csvDelimiter = CsvDelimiter)
        {
            if (string.IsNullOrWhiteSpace(pathToCsvFile))
            {
                throw  new ArgumentNullException("pathToCsvFile");
            }

            if (string.IsNullOrWhiteSpace(csvDelimiter))
            {
                throw new ArgumentNullException("csvDelimiter");
            }

            var dataFromFile = ReadCsvFile(pathToCsvFile, csvDelimiter);

            return GenerateCartesianProduct(dataFromFile);
        }

        /// <summary>
        /// Reads the CSV file. First rown in csv file should be header
        /// </summary>
        /// <param name="pathToCsvFile">The path to CSV file.</param>
        /// <param name="csvDelimiter">The CSV delimiter.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Can't read data from file:  + pathToCsvFile</exception>
        /// <exception cref="System.ArgumentException">When file is empty</exception>
        private static IEnumerable<IList<string>> ReadCsvFile(string pathToCsvFile, string csvDelimiter)
        {
            if (!File.Exists(pathToCsvFile))
            {
                throw new FileNotFoundException("Can't read data from file: " + pathToCsvFile);
            }

            var fileContent = File.ReadAllLines(pathToCsvFile);
            if (fileContent.Length <= 1)
            {
                throw new ArgumentException(string.Format("File '{0}' has no content", pathToCsvFile));
            }

            return ParseCsvData(fileContent, csvDelimiter);
        }

        private static IEnumerable<IList<string>> ParseCsvData(IList<string> fileContent, string csvDelimiter)
        {
            // Assuming first line is the header, get number of columns
            var columnNumber = fileContent[0].Split(csvDelimiter.ToCharArray()).Length;

            // Init result object: create List object for each column
            IList<IList<string>> result = new List<IList<string>>();
            for (int i = 0; i < columnNumber; i++)
            {
                result.Add(new List<string>());
            }

            // Parse lines from CSV file, row by row
            for (int rowIdx = 1; rowIdx < fileContent.Count; rowIdx++)
            {
                var columns = fileContent[rowIdx].Split(csvDelimiter.ToCharArray());
                // Number of columns is bigger that number of columns in header
                if (columns.Length > columnNumber)
                {
                    throw new InvalidDataException(string.Format("Malformed CSV file, number of columns in row {0} ({1}) is bigger that number of columns in header ({2})", rowIdx, columns.Length, columnNumber));
                }

                for (int colIdx = 0; colIdx < columns.Length; colIdx++)
                {
                    // Skip empty cells
                    if (!string.IsNullOrWhiteSpace(columns[colIdx]))
                    {
                        result[colIdx].Add(columns[colIdx]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Generates the cartesian product.
        /// http://blogs.msdn.com/b/ericlippert/archive/2010/06/28/computing-a-cartesian-product-with-linq.aspx
        /// </summary>
        /// <typeparam name="T">Type of the items</typeparam>
        /// <param name="sequences">The sequences.</param>
        /// <returns>Collection with generated permutations</returns>
        private  static IEnumerable<IEnumerable<T>> GenerateCartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>()};
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) => 
                    from accseq in accumulator 
                    from item in sequence 
                    select accseq.Concat(new[] {item})                        
                );
         }
    }
}
