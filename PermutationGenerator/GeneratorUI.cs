using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PermutationGenerator
{
    public partial class GeneratorUI : Form
    {
        public GeneratorUI()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(delimiterEdit.Text))
            {
                MessageBox.Show("Delimiter can't be empty!");
                return;
            }

            if (string.IsNullOrWhiteSpace(inputText.Text))
            {
                MessageBox.Show("No inpout text!");
                return;
            }

            outputText.Clear();

            var tempResults = PermutationGenerator.Generator.GetPermutations(inputText.Lines, delimiterEdit.Text);
            var results = ProcessResults(tempResults, delimiterEdit.Text);


            foreach (var row in results)
            {
                outputText.AppendText(row);
                outputText.AppendText(System.Environment.NewLine);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "This application generated permutations for input data in CSV format.\r\n";
            message += "This application supports command line interface\r\n";
            message += "Usage: PermutationsGenerator.exe <inputCsvFile> -print|<resultFileName> [<delimiter>]\r\n";
            message += "Samples:\r\n";
            message += "\tPrint permutation to screen:\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv -print\r\n";
            message += "\tSave permutation to file:\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv c:\\temp\\output.csv\r\n";
            message += "\tUse not standard delimiter (comma used by default):\r\n\t\tPermutationsGenerator.exe c:\\temp\\input.scv c:\\temp\\output.csv ;\r\n";
            MessageBox.Show(message);
        }
    }
}
