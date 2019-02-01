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

        //get the max degree of this graph
        public int GetGraphMaxDeg()
        {
            int maxVal = int.MinValue;
            int degree = 0;
            foreach (Node node in LNodes.Values)
            {
                degree = node.GetLNodesNeighbors.Count;
                if (degree > maxVal)
                {
                    maxVal = degree;
                }
            }
            return maxVal;
        }

    }
}