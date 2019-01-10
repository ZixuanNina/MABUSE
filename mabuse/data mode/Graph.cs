using System;
using System.Collections.Generic;

namespace mabuse.datamode
{
    public class Graph
    {
        public double StartTime;
        public double EndTime;
        public Dictionary<string, Edge> LNodes { get; set; }
        public Dictionary<string, Edge> LEdges { get; set; }
        public int DegreeDist()
        {
            return -1;
        }
    }
}