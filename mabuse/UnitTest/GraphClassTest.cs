using NUnit.Framework;
using System.Collections.Generic;
using mabuse.datamode;

namespace mabuse.UnitTest
{
    [TestFixture()]
    public class GraphClassTest
    {
        /// <summary>
        ///Test for GetMaxDegOfGraph function
        /// </summary>

        // Test when there is one edge
        [Test()]
        public void Test_GetMaxDegOfGraphOneEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node {NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b"});
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            int maxDeg = graph[0].GetMaxDegreeOfGraph();
            Assert.AreEqual(maxDeg, 1);
        }
        //Test when there is no node and no edge
        [Test]
        public void Test_GEtMaxDegOfGraphZeroNodeAndEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            int maxDeg = graph[0].GetMaxDegreeOfGraph();
            Assert.AreEqual(maxDeg, int.MinValue);
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            maxDeg = graph[0].GetMaxDegreeOfGraph();
            Assert.AreEqual(maxDeg, 0);
        }
        //test if function could find the max degree with comparision
        [Test]
        public  void Tesr_GetMaxDegOfGraphWithComparision()
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
            int maxDeg = graph[0].GetMaxDegreeOfGraph();
            Assert.AreEqual(maxDeg, 2);
        }

        /// <summary>
        /// Tests the CountNumberOfPatnerwiseNeighbors function.
        /// </summary>/

        //Test when there is no edge
        [Test]
         public void Test_CountNumberOfPatnerwiseNeighborsZeroEdge()
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
        public void Test_CountNumberOfPatnerwiseNeighborsZeroNeighbor()
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

            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b"});
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            int NumberOfTriangle = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(NumberOfTriangle, 0);
        }

        //Test when there is a partnerwise sharing
        [Test]
        public void Test_CountNumberOfPatnerwiseNeighborsOneSharedPartner()
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

            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            int maxDeg = graph[0].CountNumberOfPatnerwiseNeighbors(graph[0].NodeIdToNodeObjectDict["a"], graph[0].NodeIdToNodeObjectDict["b"]);
            Assert.AreEqual(maxDeg, 1);
        }

        ///<summary>
        /// Test the GetGraphMaxNumberOfPartnerwiseNeighbors function
        /// </summary>
        //Test if there no edge
        [Test]
        public void Test_GetGraphMaxNumberOfPartnerwiseNeighborsEmpty()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            int maxNumberOfTriangles = graph[0].GetGraphMaxNumberOfPartnerwiseNeighbors();
            Assert.AreEqual(maxNumberOfTriangles, int.MinValue);
        }

        //Test if there are edges able to find the maximum
        [Test]
        public void Test_GetGraphMaxNumberOfPartnerwiseNeighbors()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["d"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].EdgeIdToEdgeObjectDict.Add("a-b", new Edge 
            { 
                EdgeId = "a-b", 
                NodeA = graph[0].NodeIdToNodeObjectDict["a"], 
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("b-c", new Edge 
            { 
                EdgeId = "b-c", 
                NodeA = graph[0].NodeIdToNodeObjectDict["b"], 
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("a-c", new Edge 
            { 
                EdgeId = "a-c", 
                NodeA = graph[0].NodeIdToNodeObjectDict["a"], 
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("c-d", new Edge 
            { 
                EdgeId = "c-d", 
                NodeA = graph[0].NodeIdToNodeObjectDict["c"], 
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            int maxNumberOfTriangles = graph[0].GetGraphMaxNumberOfPartnerwiseNeighbors();
            Assert.AreEqual(maxNumberOfTriangles, 1);
        }
    }
}
