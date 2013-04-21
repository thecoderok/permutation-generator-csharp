﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PermutationGenerator.Test
{
    [TestClass]
    public class PermutationGeneratorTests
    {
        #region Tests for Null and empty
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPathToFileNull()
        {
            PermutationGenerator.Generator.GetPermutations(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPathToFileEmpty()
        {
            PermutationGenerator.Generator.GetPermutations(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPathToCsvSeparatorNull()
        {
            PermutationGenerator.Generator.GetPermutations(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPathToCsvSeparatorEmpty()
        {
            PermutationGenerator.Generator.GetPermutations(" ");
        }
        #endregion


        #region Negative tests with Files
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestFileNotFound()
        {
            PermutationGenerator.Generator.GetPermutations("dummypath");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileEmpty()
        {
            PermutationGenerator.Generator.GetPermutations("InputEmpty.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileHeaderOnly()
        {
            PermutationGenerator.Generator.GetPermutations("InputHeaderOnly.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestFileMalformedAndDelimiter()
        {
            PermutationGenerator.Generator.GetPermutations("InputMalformed.txt", ";");
        }
        #endregion

        #region Positive tests with expected data
        [TestMethod]
        public void TestPermutations()
        {
            var result = PermutationGenerator.Generator.GetPermutations("InputNormal.txt");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());

            var expectedData = new List<string>() {"a;b;", "c;b;"};
            ValidateExpectedData(expectedData, result);
        }

        [TestMethod]
        public void TestPermutationsSemiolon()
        {
            var result = PermutationGenerator.Generator.GetPermutations("InputSemicolon.txt", ";");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());

            var expectedData = new List<string>() { "admin;1;", "admin;3;", "root;1;", "root;3;", "dev;1;", "dev;3" };
            ValidateExpectedData(expectedData, result);
        }
        #endregion

        #region Helper methods
        public void ValidateExpectedData(List<string> expected,
                                         IEnumerable<IEnumerable<string>> actual)
        {
            var actualProcessed = new List<string>();

            AppendToList(actual, actualProcessed);

            Assert.AreEqual<int>(expected.Count, actualProcessed.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actualProcessed[i]);
            }
        }

        private void AppendToList(IEnumerable<IEnumerable<string>> data, List<string> result)
        {
            foreach (var row in data)
            {
                var item = new StringBuilder();
                foreach (var col in row)
                {
                    item.Append(col).Append(";");
                }
                result.Add(item.ToString());
            }
        }
        #endregion
    }
}
