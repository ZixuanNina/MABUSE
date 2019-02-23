/*
Author: Zixuan(Nina) Hao
Purpose:
    Node is a type of the dicttionary list to format the value and store the information related to the node object
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace mabuse.datamode
{
    public class Node
    {
        //work for the node type
        public string NodeId { get; set; }
        public double NodeStartT { get; set; }
        public double NodeEndT { get; set; }
        public Dictionary<string, Edge> LEdges = new Dictionary<string, Edge>();
        public Dictionary<string, Edge> GetLEdges { get { return LEdges; } }
        public Dictionary<string, Node> LNodesNeighbors = new Dictionary<string, Node>();
        public Dictionary<string, Node> GetLNodesNeighbors { get { return LNodesNeighbors; } }

        //get the degree of Node
        public int GetDegree(double endT)
        {
            int degree = 0;
            foreach(Edge edge in LEdges.Values)
            { 
                //check if the edge exist at the current time
                if ((edge.EdgeStartT <= endT) && ((edge.EdgeEndT >= endT) || edge.EdgeEndT.Equals(0)))
                {
                        degree++;
                }
            }
            return degree;
        }
    }
}
