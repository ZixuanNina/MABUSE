/*
Author: Zixuan(Nina) Hao
Purpose:
    ReportFactory collectt all the information based on the object provided to retrive the data and generate the report.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using mabuse.datamode;

namespace mabuse
{
    public class ReportFactory
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public ReportFactory(Dictionary<double, Graph> Graphs) 
        {
            GraphTimeToGraphObjectDict = Graphs;
        }
        /*
         * degree distribution       
         */
        //get the max degree through the graphs
        public int GetMaxDegree()
        {
            int maxVal = Int32.MinValue;
            foreach(Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                int max = graph.GetMaxDegree();
                if (max > maxVal)
                {
                    maxVal = max;
                }
            }
            return maxVal;
        }
        //get the intervals based on the max given
        public int[] GetInterval(int max)
        {
            int num = 10;
            int interval = max / num;
            int[] ranges = new int[10];
            if (interval == 0)
            {
                for(int i = 1; i < 11; i++)
                {
                    ranges[i - 1] = i;
                }
            }
            else if(interval > 0)
            {
                if(max % num == 0)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        ranges[i - 1] = i * interval;
                    }
                }
                else
                {
                    int reminder = max % num;
                    for (int i = 1; i < 10; i++)
                    {
                        ranges[i - 1] = i * interval;
                    }
                    ranges[9] = 10 * interval + reminder;
                }
            }
            return ranges;
        }
        //count the number of existence of the nodes in the degree interval
        public int[] CountDegree(Graph graph)
        {
            int[] countDeg = new int[10];
            int[] range = GetInterval(GetMaxDegree());

            foreach (Node node in graph.NodeIdToNodeObjectDict.Values)
            {
                int degree = node.EdgeIdToEdgeObjectDict.Count;
                if (degree >= 0) {
                    for(int i = 0; i < 10; i++)
                    {
                        if (degree <= range[i])
                        {
                            countDeg[i]++;
                            break;
                        }
                    }
                }
            }
            return  countDeg;
        }
        /*
         * edgewise shared partner distribution       
         */
         //get the max value of number of triangle
        public int GetMaxtri()
        {
            int maxVal = Int32.MinValue;
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                if (graph.GetGraphMaxNumberOfPartnerwiseNeighbors() > maxVal)
                {
                    maxVal = graph.GetGraphMaxNumberOfPartnerwiseNeighbors();
                }
            }
            return maxVal;
        }
        //count the number of edgewise shared partner
        public int[] CountPartner(Graph graph)
        {
            int[] countPartner = new int[10];
            int[] range = GetInterval(GetMaxtri());

            foreach (Edge edge in graph.EdgeIdToEdgeObjectDict.Values)
            {
                int count = graph.CountNumberOfPatnerwiseNeighbors(edge.NodeA, edge.NodeB);
                if (count >= 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (count <= range[i])
                        {
                            countPartner[i]++;
                            break;
                        }
                    }
                }
            }
            return countPartner;
        }
    }
}
