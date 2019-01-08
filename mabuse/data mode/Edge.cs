using System;
using System.Collections.Generic;

namespace mabuse.datamode
{
    public class Edge
    {
        public static List<string> edges = new List<string>();

        //finish the command task add and remove edge
        public void Add(string edge)
        {
            edges.Add(edge);
        }

        public void Remove(string edge)
        {
            edges.Remove(edge);
        }
    }
}
