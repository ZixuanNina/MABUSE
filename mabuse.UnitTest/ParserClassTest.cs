using System;
using System.IO;
using Xunit;

namespace mabuse.UnitTest
{
    /// <summary>
    /// Parser class test.
    /// </summary>
    public class ParserClassTest
    {
        /// <summary>
        /// Parsers the empty string test.
        /// </summary>
        [Fact]
        public void ParserTestEmptyFile()
        {
            Parser parser;
            Assert.Throws<ArgumentException>(() => parser = new Parser(""));
        }

        /// <summary>
        /// Parsers the test invalid file name(non txt file).
        /// </summary>
        [Fact]
        public void ParserTestInvalidFileName()
        {
            Parser parser;
            Assert.Throws<ArgumentException>(() => parser = new Parser("/new path/Test/test.cs"));
        }

        /// <summary>
        /// Parsers the test invalid file path.
        /// </summary>
        [Fact]
        public void ParserTestInvalidFilePath()
        {
            Parser parser;
            Assert.Throws<DirectoryNotFoundException>(() => parser = new Parser("/new path/Test/test.txt"));
        }
    }
}
