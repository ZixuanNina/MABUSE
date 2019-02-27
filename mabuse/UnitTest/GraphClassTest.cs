using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mabuse.datamode;

namespace mabuse.UnitTest
{
    [TestFixture()]
    public class GraphClassTest
    {
        /*
         * Test for GetMaxDeg function
         */
         //Test when there is one edge
        [Test()]
        public void Test_GetMaxDeg()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { StartTime = 0, EndTime = 0, } }
            };
            graph[0].LNodes.Add("a", new Node {NodeId = "a" });
            graph[0].LNodes.Add("b", new Node { NodeId = "b" });
            graph[0].LNodes["a"].LEdges.Add("a-b", new Edge { EdgeId = "a-b"});
            graph[0].LNodes["b"].LEdges.Add("a-b", new Edge { EdgeId = "a-b" });
            int maxDeg = graph[0].GetMaxDeg();
            Assert.AreEqual(maxDeg, 1);
        }
        //Test when there is no node and no edge
        [Test]
        public void Test_GEtMaxDegZero()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { StartTime = 0, EndTime = 0, } }
            };
            int maxDeg = graph[0].GetMaxDeg();
            Assert.AreEqual(maxDeg, 0);
            graph[0].LNodes.Add("a", new Node { NodeId = "a" });
            maxDeg = graph[0].GetMaxDeg();
            Assert.AreEqual(maxDeg, 0);
        }
        //test if function could find the max degree with comparision
        [Test]
        public  void Tesr_GetMaxDegCompare()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { StartTime = 0, EndTime = 0, } }
            };
            graph[0].LNodes.Add("a", new Node { NodeId = "a" });
            graph[0].LNodes.Add("b", new Node { NodeId = "b" });
            graph[0].LNodes.Add("c", new Node { NodeId = "c" });
            graph[0].LNodes["a"].LEdges.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].LNodes["b"].LEdges.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].LNodes["a"].LEdges.Add("a-c", new Edge { EdgeId = "a-c" });
            graph[0].LNodes["c"].LEdges.Add("a-c", new Edge { EdgeId = "a-c" });
            int maxDeg = graph[0].GetMaxDeg();
            Assert.AreEqual(maxDeg, 2);
        }

        /*
         * Test for GetTrinum
         */
         [Test]
         public void Test_GetTriNum()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { StartTime = 0, EndTime = 0, } }
            };
            graph[0].LNodes.Add("a", new Node { NodeId = "a" });
            graph[0].LNodes.Add("b", new Node { NodeId = "b" });
            int maxDeg = graph[0].GetTriNum(graph[0].LNodes["a"], graph[0].LNodes["b"]);
            Assert.AreEqual(maxDeg, 0);
        }
    }
}
