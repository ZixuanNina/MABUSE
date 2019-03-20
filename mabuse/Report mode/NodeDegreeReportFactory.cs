using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    /// <summary>
    /// Node degree report factory.
    /// </summary>
    public class NodeDegreeReportFactory
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.NodeDegreeReportFactory"/> class.
        /// </summary>
        /// <param name="graph">Graph.</param>
        /// <param name="nodes">Nodes.</param>
        public NodeDegreeReportFactory(Dictionary<double, Graph> graph, Dictionary<string, Node> nodes)
        {
            Condition.Requires(graph, "graph of Test")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(nodes, "node of test")
                .IsNotNull()
                .IsNotEmpty();

            GraphTimeToGraphObjectDict = graph;
            NodeIdToNodeObjectDict = nodes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.NodeDegreeReportFactory"/> class.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public NodeDegreeReportFactory(Parser parser)
        {
            //input parameter condition check
            Condition.Requires(parser, "Result of Parser")
                .IsNotNull();
            Condition.Requires(parser.GetGraphTimeToGraphDictionary(), "graph of Test")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(parser.GetNodeIdToNodeObjectDictionary(), "graph of Test")
                .IsNotNull()
                .IsNotEmpty();

            GraphTimeToGraphObjectDict = parser.GetGraphTimeToGraphDictionary();
            NodeIdToNodeObjectDict = parser.GetNodeIdToNodeObjectDictionary();
        }

        /// <summary>
        /// Gets the node degrees.
        /// </summary>
        /// <returns>The node degrees.</returns>
        public Dictionary<string, int[]> GetNodeDegrees()
        {
            Dictionary<String, int[]> NodeIdToItsDegree = new Dictionary<string, int[]>();
            foreach (Node node in NodeIdToNodeObjectDict.Values)
            {
                int[] countDegreeByTime = new int[GraphTimeToGraphObjectDict.Count];
                int i = 0;
                foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
                {
                    countDegreeByTime[i] = node.CountDegree(graph.GraphEndTime);
                    i++;
                }
                NodeIdToItsDegree.Add(node.NodeId, countDegreeByTime);
            }

            Condition.Ensures(NodeIdToItsDegree, "node id direct to its degree")
                .IsNotEmpty();

            return NodeIdToItsDegree;
        }
    }
}
