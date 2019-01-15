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
        public Dictionary<string, Node> LNodes { get; set; }
        public Dictionary<string, Edge> LEdges { get; set; }
        //generate the degree distribution for session 3
        public Dictionary<string, int> GetMaxDegree()
        {
            //collectt the number of degrees of each node at the current graph time
            int[] degrees = new int[LNodes.Count];
            int i = 0;
            foreach (Node node in LNodes.Values)
            {
                degrees[i] = node.LNodesNeighbors.Count;
            }
            int maxDeg = degrees.Max();
            int interval = maxDeg / 10;
            Array.Sort(degrees);
            Dictionary<string, int> degreeDist = new Dictionary<string, int>();
            for (int j = 0; j < 9; j++)
            {
                degreeDist.Add(j*interval + "-" + (j + 1)*interval, CounterByRange(j*interval, (j + 1) * interval, degrees));
            }
            if (!(maxDeg % 10).Equals(0))
            {
                degreeDist.Add(9 * interval + "-" + maxDeg, CounterByRange(9 * interval, maxDeg, degrees));
            }

            return degreeDist;
        }
        public int CounterByRange(int low, int up, int[] arr)
        {
            int count = 0;
            foreach (int num in arr)
            {
                if(num <= up && num >= low)
                {
                    count++;
                }
                /*
                if(num > up)
                {
                    break;
                }
                */
            }
            return count;
        }
    }
}