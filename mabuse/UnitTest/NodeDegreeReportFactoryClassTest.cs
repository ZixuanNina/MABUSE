using mabuse.datamode;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mabuse.UnitTest
{
    [TestFixture()]
    public class NodeDegreeReportFactoryClassTest
    {
        /// <summary>
        /// Tests the get node degrees function.
        /// </summary>
        // Test when parameter is empty stack
        [Test]
        public void Test_GetNodeDegreesEmpty()
        {
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
            Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();
            NodeDegreeReportFactory nodeDegreeReport;
            Assert.Throws<ArgumentException>(() => nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDict, NodeIdToNodeObjectDict));
            Dictionary<string, Node> NodeIdToNodeObjectDictNonEmpty = new Dictionary<string, Node>()
            {
                { "N_1", new Node{NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365} }
            };
            Dictionary<double, Graph> GraphTimeToGraphObjectDictNonEmpty = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            Assert.Throws<ArgumentException>(() => nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDict, NodeIdToNodeObjectDictNonEmpty));
            Assert.Throws<ArgumentException>(() => nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDictNonEmpty, NodeIdToNodeObjectDict));
        }

        //Test when there is only one node without existence in graph
        [Test]
        public void Test_GetNodeDegreesNodeWithoutEdge()
        {
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>()
            {
                { "N_1", new Node{NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365} }
            };
            NodeDegreeReportFactory nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDict, NodeIdToNodeObjectDict);
            Dictionary<String, int[]> NodeIdToItsDegree = nodeDegreeReport.GetNodeDegrees();
            Dictionary<String, int[]> NodeIdToItsDegreeExpect = new Dictionary<string, int[]>()
            {
                {"N_1", new int[]{0}} 
            };
            Assert.AreEqual(NodeIdToItsDegree, NodeIdToItsDegreeExpect);
        }

        //Test when there is only one node with edge existence in graph
        [Test]
        public void Test_GetNodeDegreesNodeWithEdge()
        {
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>()
            {
                { "N_1", new Node
                        {NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365, 
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} }
                            } 
                            } },
                { "N_2", new Node
                        {NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} }
                            }
                            } }
            };
            NodeDegreeReportFactory nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDict, NodeIdToNodeObjectDict);
            Dictionary<String, int[]> NodeIdToItsDegree = nodeDegreeReport.GetNodeDegrees();
            Dictionary<String, int[]> NodeIdToItsDegreeExpect = new Dictionary<string, int[]>()
            {
                {"N_1", new int[]{1}},
                {"N_2", new int[]{1}}
            };
            Assert.AreEqual(NodeIdToItsDegree, NodeIdToItsDegreeExpect);
        }

        //Test when there is only one node with edge existence in graphs
        [Test]
        public void Test_GetNodeDegreesNodeWithEdgeInGraphs()
        {
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365 } }
            };
            Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>()
            {
                { "N_1", new Node
                        {NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} },
                                {"N_1-N_3", new Edge{ EdgeId = "N_1-N_3", EdgeStartTime = 0, EdgeEndTime = 30} }
                            }
                            } },
                { "N_2", new Node
                        {NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} }
                            }
                            } },
                { "N_3", new Node
                        {NodeId = "N_3", NodeStartTime = 0, NodeEndTime = 365,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
                            {
                                {"N_1-N_3", new Edge{ EdgeId = "N_1-N_3", EdgeStartTime = 0, EdgeEndTime = 30} }
                            }
                            } }
            };
            NodeDegreeReportFactory nodeDegreeReport = new NodeDegreeReportFactory(GraphTimeToGraphObjectDict, NodeIdToNodeObjectDict);
            Dictionary<String, int[]> NodeIdToItsDegree = nodeDegreeReport.GetNodeDegrees();
            Dictionary<String, int[]> NodeIdToItsDegreeExpect = new Dictionary<string, int[]>()
            {
                {"N_1", new int[]{2, 1}},
                {"N_2", new int[]{1, 1}},
                {"N_3", new int[]{1, 0}}
            };
            Assert.AreEqual(NodeIdToItsDegree, NodeIdToItsDegreeExpect);
        }
    }
}
