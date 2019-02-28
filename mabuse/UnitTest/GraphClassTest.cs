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
        /// <summary>
        ///Test for GetMaxDeg function
        /// </summary>

        // Test when there is one edge
        [Test()]
        public void Test_GetMaxDegOneEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node {NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b"});
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            int maxDeg = graph[0].GetMaxDegree();
            Assert.AreEqual(maxDeg, 1);
        }
        //Test when there is no node and no edge
        [Test]
        public void Test_GEtMaxDegZeroNodeAndEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            int maxDeg = graph[0].GetMaxDegree();
            Assert.AreEqual(maxDeg, 0);
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            maxDeg = graph[0].GetMaxDegree();
            Assert.AreEqual(maxDeg, 0);
        }
        //test if function could find the max degree with comparision
        [Test]
        public  void Tesr_GetMaxDegWithComparision()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });
            int maxDeg = graph[0].GetMaxDegree();
            Assert.AreEqual(maxDeg, 2);
        }

        /// <summary>
        /// Tests the get tri number function.
        /// </summary>/
         
        //Test when there is no edge
         [Test]
         public void Test_GetTriNumZeroEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            int NumberOfTriangle = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(NumberOfTriangle, 0);
        }

        //Test when there is edges without neighbor
        [Test]
        public void Test_GetTriNumZeroNeighbor()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge { EdgeId = "b-c" });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge { EdgeId = "b-c" });
            int NumberOfTriangle = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(NumberOfTriangle, 0);
        }

        //Test when there is a partnerwise sharing
        [Test]
        public void Test_GetTriNumOneSharedPartner()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge { EdgeId = "b-c" });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge { EdgeId = "b-c" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });
            int maxDeg = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(maxDeg, 1);
        }

        ///<summary>
        /// Test the GetGraphMaxTri function
        /// </summary>
        //Test if there no edge
        [Test]
        public void Test_GetGraphMaxTriEmpty()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            int maxNumberOfTriangles = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(maxNumberOfTriangles, 1);
        }

    }
}
