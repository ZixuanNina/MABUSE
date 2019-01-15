/*
Author: Zixuan(Nina) Hao
Purpose:
    Node is a type of the dicttionary list to format the value and store the information related to the node object
 */

using System;
using System.Collections.Generic;

namespace mabuse.datamode
{
    public class Node
    {
        //work for the node type
        public string NodeId { get; set; }
        public double NodeStartT { get; set; }
        public double NodeEndT { get; set; }
        public Dictionary<string, Edge> LEdges { get; set; }
        public Dictionary<string, Node> LNodesNeighbors { get; set; }

        //get the degree of Node
        public int GetDegree(double startT, double endT)
        {
            int degree = 0;
            foreach(Edge edge in LEdges.Values)
            { 
                //four possible ranges:sT >= eST && eT = est(two possible cases); sT > eET && eT <= eST(two possibles)
                //two impossible ranges:sT >= eET || eT <= sET
                if (!(startT >= edge.EdgeEndT) || !(endT <= edge.EdgeStartT))
                {
                    degree++;
                }
            }
            return degree;
        }
    }
}
