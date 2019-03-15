using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    public class NodeDegreeReportFactory
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();
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
