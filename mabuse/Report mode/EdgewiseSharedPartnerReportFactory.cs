using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse.Reportmode
{
    /// <summary>
    /// Edgewise shared partner report factory.
    /// </summary>
    public class EdgewiseSharedPartnerReportFactory
    {
        private Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();

        public EdgewiseSharedPartnerReportFactory(Dictionary<double, Graph> graph, Dictionary<string, Edge> edges)
        {
            Condition.Requires(graph, "graph of Test")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(edges, "node of test")
                .IsNotNull()
                .IsNotEmpty();

            GraphTimeToGraphObjectDict = graph;
            EdgeIdToEdgeObjectDict = edges;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.Reportmode.EdgewiseSharedPartnerReportFactory"/> class.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public EdgewiseSharedPartnerReportFactory(Parser parser)
        {
            Condition.Requires(parser, "parser result")
                .IsNotNull();

            EdgeIdToEdgeObjectDict = parser.GetEdgeIdToEdgeObjectDict();
            GraphTimeToGraphObjectDict = parser.GetGraphTimeToGraphDictionary();
        }

        /// <summary>
        /// Counts the partnerwise of edge.
        /// </summary>
        /// <returns>The partnerwise of edge.</returns>
        public Dictionary<string, Edge> CountPartnerwiseOfEdge()
        {
            int graphCount = 0;
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                graphCount++;
                foreach(Edge edge in graph.EdgeIdToEdgeObjectDict.Values)
                {
                     EdgeIdToEdgeObjectDict[edge.EdgeId].CountPartnerwiseByTime.Add(graph.CountNumberOfPatnerwiseNeighbors(edge.NodeA, edge.NodeB));
                }
                foreach(Edge edge in EdgeIdToEdgeObjectDict.Values)
                {
                    if(edge.EdgeId.Equals("N_46_20190312135056450 - N_76_20190312135056451"))
                    {
                        Console.WriteLine();
                    }
                    if (EdgeIdToEdgeObjectDict[edge.EdgeId].CountPartnerwiseByTime.Count < graphCount)
                    {
                        EdgeIdToEdgeObjectDict[edge.EdgeId].CountPartnerwiseByTime.Add(-1);
                    }
                }
            }
            return EdgeIdToEdgeObjectDict;
        }
    }
}
