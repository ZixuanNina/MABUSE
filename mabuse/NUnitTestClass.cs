using mabuse.datamode;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mabuse
{
    [TestFixture()]
    public class NUnitTestClass
    {
        [Test()]
        public void TestParser(Parser parser)
        {
            parser.AddEdge("nodeA", "nodeB", 365, 3);
            Assert.AreEqual(365, parser.GetGraph()[365].EndTime);
            Assert.IsNotEmpty(parser.GetGraph());
        }
    }
}
