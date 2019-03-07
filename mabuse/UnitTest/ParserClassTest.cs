using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using mabuse.datamode;
namespace mabuse.UnitTest
{
    /// <summary>
    /// Parser class test.
    /// </summary>
    [TestFixture()]
    public class ParserClassTest
    {
        /// <summary>
        /// Parsers the empty string test.
        /// </summary>
        [Test()]
        public void ParserTestEmptyFile()
        {
            Parser parser;
            Assert.Throws<ArgumentException>(() => parser = new Parser(""));
        }

        /// <summary>
        /// Parsers the test invalid file name(non txt file).
        /// </summary>
        [Test()]
        public void ParserTestInvalidFileName()
        {
            Parser parser;
            Assert.Throws<ArgumentException>(() => parser = new Parser("/new path/Test/test.cs"));
        }

        /// <summary>
        /// Parsers the test invalid file path.
        /// </summary>
        [Test()]
        public void ParserTestInvalidFilePath()
        {
            Parser parser;
            Assert.Throws<DirectoryNotFoundException>(() => parser = new Parser("/new path/Test/test.txt"));
        }
    }
}
