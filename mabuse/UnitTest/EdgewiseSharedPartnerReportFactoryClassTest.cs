using mabuse.datamode;
using mabuse.Reportmode;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mabuse.UnitTest
{
    /// <summary>
    /// Edgewise shared partner report factory class test.
    /// </summary>
    [TestFixture()]
    public class EdgewiseSharedPartnerReportFactoryClassTest
    {
        /// <summary>
        /// Tests the edgewise shared partner report factory empty.
        /// </summary>
        [Test()]
        public void Test_EdgewiseSharedPartnerReportFactoryEmpty()
        {
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
            Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();
            EdgewiseSharedPartnerReportFactory edgewiseSharedPartnerCount;
            Assert.Throws<ArgumentException>(() => edgewiseSharedPartnerCount = new EdgewiseSharedPartnerReportFactory(GraphTimeToGraphObjectDict, EdgeIdToEdgeObjectDict));
            Dictionary<string, Edge> EdgeIdToEdgeObjectDictNonEmpty = new Dictionary<string, Edge>()
            {
                { "N_1-N_2", new Edge{ EdgeId= "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} }
            };
            Dictionary<double, Graph> GraphTimeToGraphObjectDictNonEmpty = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0, } }
            };
            Assert.Throws<ArgumentException>(() => edgewiseSharedPartnerCount = new EdgewiseSharedPartnerReportFactory(GraphTimeToGraphObjectDict, EdgeIdToEdgeObjectDict));
            Assert.Throws<ArgumentException>(() => edgewiseSharedPartnerCount = new EdgewiseSharedPartnerReportFactory(GraphTimeToGraphObjectDictNonEmpty, EdgeIdToEdgeObjectDict));
        }

        /// <summary>
        /// Tests the count partnerwise of edge with triangle in graph.
        /// </summary>/
        [Test]
        public void Test_CountPartnerwiseOfEdgeWithTriangleInGraph()
        {
            Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>()
            {
                { "N_1", new Node
                        {
                            NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365,
                            EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} },
                                {"N_1-N_3", new Edge{ EdgeId = "N_1-N_3", EdgeStartTime = 40, EdgeEndTime = 365} }
                            },
                            NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                            {
                                { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} },
                                { "N_3", new Node { NodeId = "N_3", NodeStartTime = 40, NodeEndTime = 365} }
                            }
                        }
                },
                { "N_2", new Node
                        {
                            NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365,
                            EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>
                            {
                                {"N_1-N_2", new Edge{ EdgeId = "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} },
                                {"N_2-N_3", new Edge{ EdgeId = "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365} }
                            },
                            NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>{
                                { "N_1", new Node { NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365} },
                                { "N_3", new Node { NodeId = "N_3", NodeStartTime = 0, NodeEndTime = 365} }
                            }
                        }
                },
                { "N_3", new Node
                        {
                            NodeId = "N_3", NodeStartTime = 0, NodeEndTime = 365,
                            EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>
                            {
                                {"N_1-N_3", new Edge{ EdgeId = "N_1-N_3", EdgeStartTime = 40, EdgeEndTime = 30} },
                                {"N_2-N_3", new Edge{ EdgeId = "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365} }
                            },
                            NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                            {
                                { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} },
                                { "N_1", new Node { NodeId = "N_1", NodeStartTime = 40, NodeEndTime = 365} }
                            }
                        }
                }
            };
            Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>
            {
                { 0, new Graph 
                    { 
                        GraphStartTime = 0, GraphEndTime = 0,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>
                        {
                            { "N_1-N_2", new Edge{ EdgeId= "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365 } },
                            { "N_2-N_3", new Edge{ EdgeId= "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365 } }
                        },
                        NodeIdToNodeObjectDict = new Dictionary<string, Node>
                        {
                            { "N_1", new Node
                                {   
                                    NodeId = "N_1", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    { 
                                        { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} } }
                                }
                            },
                            { "N_2", new Node
                                { 
                                    NodeId = "N_2", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    { 
                                        { "N_1", new Node { NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365} },
                                        { "N_3", new Node { NodeId = "N_3", NodeStartTime = 0, NodeEndTime = 365} } 
                                    }
                                }
                            },
                            { "N_3", new Node
                                { 
                                    NodeId = "N_3", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    { 
                                        { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} } 
                                    }
                                }
                            }
                        }
                    }
                },
                { 365, new Graph 
                    { 
                        GraphStartTime = 0, GraphEndTime = 365,
                        EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>
                        {
                            { "N_1-N_2", new Edge{ EdgeId= "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365} },
                            { "N_1-N_3", new Edge{ EdgeId= "N_1-N_3", EdgeStartTime = 40, EdgeEndTime = 365} },
                            { "N_2-N_3", new Edge{ EdgeId= "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365} }
                        },
                        NodeIdToNodeObjectDict = new Dictionary<string, Node>
                        {
                            { "N_1", new Node
                                { 
                                    NodeId = "N_1", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    { 
                                        { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} },
                                        { "N_3", new Node { NodeId = "N_3", NodeStartTime = 40, NodeEndTime = 365} }
                                    }
                                }
                            },
                            { "N_2", new Node
                                { 
                                    NodeId = "N_2", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    {
                                        { "N_1", new Node { NodeId = "N_1", NodeStartTime = 0, NodeEndTime = 365} },
                                        { "N_3", new Node { NodeId = "N_3", NodeStartTime = 0, NodeEndTime = 365} }
                                    }
                                }
                            },
                            { "N_3", new Node
                                { 
                                    NodeId = "N_3", NodeStartTime = 0, NodeEndTime = int.MaxValue,
                                    NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>
                                    { 
                                        { "N_2", new Node { NodeId = "N_2", NodeStartTime = 0, NodeEndTime = 365} },
                                        { "N_1", new Node { NodeId = "N_1", NodeStartTime = 40, NodeEndTime = 365} }
                                    }
                                }
                            }
                        }
                    } 
                }
            };
            Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>()
            {
                { "N_1-N_2", new Edge
                            { EdgeId= "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_1"], NodeB = NodeIdToNodeObjectDict["N_2"]} },
                { "N_1-N_3", new Edge
                            { EdgeId= "N_1-N_3", EdgeStartTime = 40, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_1"], NodeB = NodeIdToNodeObjectDict["N_3"]} },
                { "N_2-N_3", new Edge
                            { EdgeId= "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_2"], NodeB = NodeIdToNodeObjectDict["N_3"]} }
            };
            EdgewiseSharedPartnerReportFactory edgewiseSharedPartner = new EdgewiseSharedPartnerReportFactory(GraphTimeToGraphObjectDict, EdgeIdToEdgeObjectDict);
            Dictionary<String, Edge> edgewiseSharedPartnerCount = edgewiseSharedPartner.CountPartnerwiseOfEdge();
            Dictionary<String, Edge> edgewiseSharedPartnerCountExpect = new Dictionary<string, Edge>()
            {
                { "N_1-N_2", new Edge{ EdgeId= "N_1-N_2", EdgeStartTime = 0, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_1"], NodeB = NodeIdToNodeObjectDict["N_2"],
                    CountPartnerwiseByTime = new List<int>{ 0, 1} 
                    } 
                },
                { "N_1-N_3", new Edge{ EdgeId= "N_1-N_3", EdgeStartTime = 40, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_1"], NodeB = NodeIdToNodeObjectDict["N_3"],
                    CountPartnerwiseByTime = new List<int>{ -1, 1} 
                    } 
                },
                { "N_2-N_3", new Edge{ EdgeId= "N_2-N_3", EdgeStartTime = 0, EdgeEndTime = 365, NodeA = NodeIdToNodeObjectDict["N_2"], NodeB = NodeIdToNodeObjectDict["N_3"],
                    CountPartnerwiseByTime = new List<int>{ 0, 1} 
                    } 
                }
            };
            Assert.AreEqual(edgewiseSharedPartnerCount["N_1-N_3"].CountPartnerwiseByTime, edgewiseSharedPartnerCountExpect["N_1-N_3"].CountPartnerwiseByTime);
            Assert.AreEqual(edgewiseSharedPartnerCount["N_1-N_2"].CountPartnerwiseByTime, edgewiseSharedPartnerCountExpect["N_1-N_2"].CountPartnerwiseByTime);
            Assert.AreEqual(edgewiseSharedPartnerCount["N_2-N_3"].CountPartnerwiseByTime, edgewiseSharedPartnerCountExpect["N_2-N_3"].CountPartnerwiseByTime);
        }


    }
}
