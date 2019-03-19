using System;
using System.Collections.Generic;
using mabuse.datamode;

namespace mabuse.Reportmode
{
    public class EdgewiseSharedPartnerReportFactory
    {
        private Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();
        private Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();
        private Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        private Dictionary<string, int> EdgeWithCountedPartnerwise = new Dictionary<string, int>();

        public EdgewiseSharedPartnerReportFactory(Parser parser)
        {
            NodeIdToNodeObjectDict = parser.GetNodeIdToNodeObjectDictionary();
            EdgeIdToEdgeObjectDict = parser.GetEdgeIdToEdgeObjectDict();
            GraphTimeToGraphObjectDict = parser.GetGraphTimeToGraphDictionary();
        }

        public Dictionary<string, int> GetEdgePartnerWiseDataOfCurr(Graph graph)
        {
            foreach(Edge edge in graph.EdgeIdToEdgeObjectDict.Values)
            {
                EdgeWithCountedPartnerwise.Add(edge.EdgeId, graph.CountNumberOfPatnerwiseNeighbors(edge.NodeA, edge.NodeB));
            }
            foreach (Edge edge in EdgeIdToEdgeObjectDict.Values)
            {
                foreach (string edgeId in EdgeWithCountedPartnerwise.Keys)
                {
                    if (edgeId.Equals(edge.EdgeId))
                    {
                        EdgeWithCountedPartnerwise.Add(edge.EdgeId, -1);
                        break;
                    }
                }
            }
            return EdgeWithCountedPartnerwise;
        }

        public int CountPartnerwise(double EndTime)
        {
            int count = 0;
            foreach (Edge edge in EdgeIdToEdgeObjectDict.Values)
            {
                if (edge.EdgeEndTime >= EndTime && edge.EdgeStartTime <= EndTime)
                {
                    CountNumberOfPatnerwiseNeighbors(edge.NodeA, edge.NodeB, EndTime);
                }
            }
            return count;
        }

        public int CountNumberOfPatnerwiseNeighbors(Node nodeA, Node nodeB, double EndTime)
        {
            int count = 0;
            foreach (Node node in NodeIdToNodeObjectDict[nodeA.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Values)
            {
                if (!nodeB.NodeId.Equals(node.NodeId)
                    && NodeIdToNodeObjectDict[nodeB.NodeId].NodeIdOfNeighborsOfNodeObjectDict.ContainsKey(node.NodeId))
                {
                    if(NodeIdToNodeObjectDict[nodeB.NodeId].NodeIdOfNeighborsOfNodeObjectDict[node.NodeId].NodeStartTime <= EndTime 
                    && NodeIdToNodeObjectDict[nodeB.NodeId].NodeIdOfNeighborsOfNodeObjectDict[node.NodeId].NodeEndTime >= EndTime)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int temp()
        {

            return 0;
        }
    }
}
