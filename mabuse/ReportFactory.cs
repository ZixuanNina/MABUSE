using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    /// <summary>
    /// ReportFactory collectt all the information based on the object provided to retrive the data 
    /// and generate the information to report.
    /// </summary>
    /// <author>
    /// Zixuan(Nina) Hao
    /// </author>
    public class ReportFactory
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public ReportFactory(Dictionary<double, Graph> Graphs) 
        {
            //input parameter condition check
            Condition.Requires(Graphs, "graph dictionary")
                .IsNotNull()
                .IsNotEmpty()
                .IsOfType(GraphTimeToGraphObjectDict.GetType());

            GraphTimeToGraphObjectDict = Graphs;
        }
        /// <summary>
        /// Gets the maximum number of degree of the nodes.
        /// </summary>
        /// <returns>The max degree.</returns>/
        public int GetMaxDegree()
        {
            int maxValOfDegrees = Int32.MinValue;
            foreach(Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                int max = graph.GetMaxDegreeOfGraph();
                if (max > maxValOfDegrees)
                {
                    maxValOfDegrees = max;
                }
            }
            return maxValOfDegrees;
        }
        /// <summary>
        /// Gets the interval of tupes
        /// </summary>
        /// <returns>The interval of tubes for report.</returns>
        /// <param name="max">Max.</param>
        public int[] GetIntervalOfTheDistribution(int max)
        {
            //input parameter condition check
            Condition.Requires(max, "max value")
                .IsGreaterOrEqual(0);

            int num = 10;
            if(max < 0)
            {
                throw new Exception($"Negative counting found value: {max}");
            }
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
        /// <summary>
        /// count the number of existence of the nodes in the degree interval
        /// </summary>
        /// <returns>The degree.</returns>
        /// <param name="graph">Graph.</param>
        public int[] CountDegreeWithInterval(Graph graph)
        {
            //input parameter condition check
            Condition.Requires(graph, "graph")
                .IsNotNull();

            int[] countDegree = new int[10];
            int[] range = GetIntervalOfTheDistribution(GetMaxDegree());

            foreach (Node node in graph.NodeIdToNodeObjectDict.Values)
            {
                int degree = node.EdgeIdToEdgeObjectDict.Count;
                if (degree >= 0) {
                    for(int i = 0; i < 10; i++)
                    {
                        if (degree <= range[i])
                        {
                            countDegree[i]++;
                            break;
                        }
                    }
                }
            }
            return  countDegree;
        }
        //get the max value of number of triangle
        /// <summary>
        /// get the max value of number of triangle for edgewise shared partner distribution 
        /// </summary>
        /// <returns>The maxtri.</returns>
        public int GetMaxNumberOfCommonNeighbor()
        {
            int maxValue = Int32.MinValue;
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                if (graph.GetGraphMaxNumberOfPartnerwiseNeighbors() > maxValue)
                {
                    maxValue = graph.GetGraphMaxNumberOfPartnerwiseNeighbors();
                }
            }
            return maxValue;
        }
        /// <summary>
        /// count the number of edgewise shared partner
        /// </summary>
        /// <returns>The partner.</returns>
        /// <param name="graph">Graph.</param>
        public int[] CountPartner(Graph graph)
        {
            //input parameter condition check
            Condition.Requires(graph, "graph")
                .IsNotNull();

            int[] countPartner = new int[10];
            int[] range = GetIntervalOfTheDistribution(GetMaxNumberOfCommonNeighbor());

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
