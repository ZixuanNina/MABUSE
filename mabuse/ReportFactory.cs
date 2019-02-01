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
        public ReportFactory(Dictionary<double, Graph> graphL)
        {
            graphList = graphL;
        }
        /*
         * degree distribution       
         */
        //get the max degree through the graphs
        public int GetMaxDeg()
        {
            int maxVal = Int32.MinValue;
            foreach (Graph graph in graphList.Values)
            {
                if(graph.GetGraphMaxDeg() > maxVal)
                {
                    maxVal = graph.GetGraphMaxDeg();
                }
            }
            return maxVal;
        }
        //get the intervals of degree distribution
        public int[] GetInterval()
        {
            int max = GetMaxDeg();
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
            for(int i = 0; i < 10; i++)
            {
                 countDeg[i] = 0;
            }
            int[] range = GetInterval();

            foreach (Node node in graph.LNodes.Values)
            {
                int degree = node.LNodesNeighbors.Count;
                if (degree >= 0) {
                    for(int i = 0; i < 10; i++)
                    {
                        if(degree <= range[i])
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

    }
}
