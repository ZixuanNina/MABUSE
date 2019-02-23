/*
Author: Zixuan(Nina) Hao
Purpose:
    Graph collect the Node list and Edge list to generat the useful result for the graph based on the time interval.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace mabuse.datamode
{
    public class Graph
    {
        public int CountGainNode { get; set; }
        public int CountLostNode { get; set; }
        public int CountGainEdge { get; set; }
        public int CountLostEdge { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public Dictionary<string, Node> LNodes = new Dictionary<string, Node>();
        public Dictionary<string, Node> GetLNodes { get { return LNodes; } }
        public Dictionary<string, Edge> LEdges = new Dictionary<string, Edge>();
        public Dictionary<string, Edge> GetLEdges { get { return LEdges; } }

        //get the number pf neighbors of two nodes in a graph.
        public int GetTriNum(Node nodeA, Node nodeB)
        {
            int count = 0;
            foreach (Node node in LNodes[nodeA.NodeId].LNodesNeighbors.Values)
            {
                if (nodeB.NodeId != node.NodeId && LNodes[nodeB.NodeId].LNodesNeighbors.ContainsKey(node.NodeId))
                {
                    Console.WriteLine(nodeB.NodeId + "&&" + node + "&&" + nodeA.NodeId);
                    count++;
                }
            }
            if(count > 1)
            {
                Console.WriteLine("edgewise partner");
            }
            return count;
        }
        //get the max value
        public int GetGraphMaxTri()
        {
            int count = 0;
            int maxCount = int.MinValue;
            foreach (Edge edge in LEdges.Values)
            {
                count = GetTriNum(edge.NodeA, edge.NodeB);
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
            return maxCount;
        }
    }

}