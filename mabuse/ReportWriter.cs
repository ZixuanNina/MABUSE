using System;
using System.Collections.Generic;
using mabuse.datamode;

namespace mabuse
{
    /// <summary>
    /// ReportWriter class generate the report to the ideal format of tables
    /// </summary>
    /// <author>
    /// Zixuan(Nina) Hao
    /// </author>
    public class ReportWriter
    {
        public Dictionary<double, Graph> graphList = new Dictionary<double, Graph>();
        public ReportWriter(ReportFactory result, string filePath)
        {
            graphList = result.GraphTimeToGraphObjectDict;
            string[] lines = { SectionOne(), SectionTwo(), SectionThree(result), SectionFour(result)};
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        /// <summary>
        /// Section One
        /// </summary>
        private string SectionOne()
        {
            string title = "MABUSE Report\n";
            string paragraph = "This report output the graph data based on the time of the simulation with 365 days a cycle. \n";
            string reportTime = "Report Date: " + DateTime.Today.ToString("D") + "\nReport Time: " + DateTime.Now.ToString("h:mm:ss tt" + "\n");
            string section1 = title + Environment.NewLine + paragraph + Environment.NewLine + reportTime + Environment.NewLine;
            return section1;
        }
        /// <summary>
        /// Section two.
        /// </summary>
        private string SectionTwo()
        {
            //generate the tuple
            Tuple<double, int, int, int, int, int, int>[] numbers = new Tuple<double, int, int, int, int, int, int>[graphList.Count];


            //generate the table title
            string table = "365 day time step\n"+"Section2: \n";
            table += string.Format("{0,-10} | {1,10} | {2,10} | {3,10} | {4,10} | {5,10} | {6,10}\n",
            "time interval",
            "the # of nodes at the current time step",
            "the # of edges at the current time step",
            "the # of edges lost over the last 365-day interval",
            "the # of edges gained over the last 365-day interval",
            "the # of nodes gained over the last 365-day interval",
            "the # of nodes lost over the last 365-day interval");
            foreach (Graph graph in graphList.Values)
            {
                table += string.Format("{0,-15} {1,-41} {2,-41} {3,-52} {4,-54} {5,-54} {6,-54}\n",
                graph.GraphEndTime, graph.NodeIdToNodeObjectDict.Count, graph.EdgeIdToEdgeObjectDict.Count, graph.CountLostEdge, graph.CountGainEdge,
                    graph.CountGainNode, graph.CountLostNode);
            }
            return table;
        }
        /// <summary>
        /// Section Three.
        /// </summary>
        /// <param name="result">Result.</param>
        private string SectionThree(ReportFactory result)
        {
            string table = "Degree distribution\n" + "Section3: \n";
            int[] interval = result.GetIntervalOfTheDistribution(result.GetMaxDegree());
            table += string.Format("{0,-20} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}\n",
            "time interval", 0 + "-" + interval[0], interval[0] + "-" + interval[1], interval[1] + "-" + interval[2], 
                interval[2] + "-" + interval[3], interval[3] + "-" + interval[4],interval[4] + "-" + interval[5], 
                interval[5] + "-" + interval[6], interval[6] + "-" + interval[7],interval[7] + "-" + interval[8], 
                interval[8] + "-" + interval[9]);
            Dictionary<double, string> list = new Dictionary<double, string>();
                foreach(Graph graph in graphList.Values)
                {
                    int[] countDeg = result.CountDegreeWithInterval(graph);
                    table += string.Format("{0,-20} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}\n",
                    graph.GraphEndTime, countDeg[0], countDeg[1], countDeg[2], countDeg[3], countDeg[4],
                        countDeg[5], countDeg[6], countDeg[7], countDeg[8], countDeg[9]);
                }
            return table;
        }
        /// <summary>
        /// Sections Four.
        /// </summary>
        /// <param name="result">Result.</param>
        private string SectionFour(ReportFactory result)
        {
            string table = "Edgewise shared partner distribution\n"+"Section4: \n";
            int[] interval = result.GetIntervalOfTheDistribution(result.GetMaxNumberOfCommonNeighbor());
            table += string.Format("{0,-20} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}\n",
            "time interval", 0 + "-" + interval[0], interval[0] + "-" + interval[1], interval[1] + "-" + interval[2],
                interval[2] + "-" + interval[3], interval[3] + "-" + interval[4], interval[4] + "-" + interval[5],
                interval[5] + "-" + interval[6], interval[6] + "-" + interval[7], interval[7] + "-" + interval[8],
                interval[8] + "-" + interval[9]);
            Dictionary<double, string> list = new Dictionary<double, string>();
            foreach (Graph graph in graphList.Values)
            {
                int[] countPartner = result.CountPartner(graph);
                table += string.Format("{0,-20} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}\n",
                graph.GraphEndTime, countPartner[0], countPartner[1], countPartner[2], countPartner[3], countPartner[4],
                    countPartner[5], countPartner[6], countPartner[7], countPartner[8], countPartner[9]);
            }
            return table;
        }
    }
}
