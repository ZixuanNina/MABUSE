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
        public Dictionary<double, Graph> graphList = new Dictionary<double, Graph>();
        public Dictionary<string, Node> nodeList = new Dictionary<string, Node>();
        public ReportFactory(Dictionary<double, Graph> graphL, Dictionary<string, Node> nodeL) 
        {
            graphList = graphL;
            nodeList = nodeL;
        }
        /*
         * degree distribution       
         */
        //get the max degree through the graphs
        public int GetMaxDeg()
        {
            int maxVal = Int32.MinValue;
            foreach(Node node in nodeList.Values)
            {
                int max = node.GetDegree(graphList.Keys.Last());
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
            int[] range = GetInterval(GetMaxDeg());

            foreach (Node node in graph.LNodes.Values)
            {
                int degree = node.GetDegree(graph.EndTime);
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
            foreach (Graph graph in graphList.Values)
            {
                if (graph.GetGraphMaxTri() > maxVal)
                {
                    maxVal = graph.GetGraphMaxTri();
                }
            }
            return maxVal;
        }
        //count the number of edgewise shared partner
        public int[] CountPartner(Graph graph)
        {
            int[] countPartner = new int[10];
            int[] range = GetInterval(GetMaxtri());

            foreach (Edge edge in graph.LEdges.Values)
            {
                int count = graph.GetTriNum(edge.NodeA, edge.NodeB);
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
