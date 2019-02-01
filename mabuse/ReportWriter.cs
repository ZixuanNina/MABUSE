/*
Author: Zixuan(Nina) Hao
Purpose:
    ReportWriter class generate the report to the ideal format of tables
 */

using System;
using System.Collections.Generic;
using mabuse.datamode;

namespace mabuse
{
    public class ReportWriter
    {
        public Dictionary<double, Graph> graphList = new Dictionary<double, Graph>();
        public ReportWriter(ReportFactory result, string filePath)
        {
            graphList = result.graphList;
            string[] lines = { Section1(), SectionTwo(), SectionThree(result), SectionFour()};
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        //section 1
        public string Section1()
        {
            string title = "MABUSE Report\n";
            string paragraph = "\n";
            string reportTime = "Report Time: " + DateTime.Now.ToString("h:mm:ss tt" + "\n");
            string section1 = title + Environment.NewLine + paragraph + Environment.NewLine + reportTime + Environment.NewLine;
            return section1;
        }
        //section 2
        public string SectionTwo()
        {
            //generate the tuple
            Tuple<double, int, int, int, int, int, int>[] numbers = new Tuple<double, int, int, int, int, int, int>[graphList.Count];


            //generate the table title
            string title = string.Format("{0,-10} | {1,10} | {2,10} | {3,10} | {4,10} | {5,10} | {6,10}\n",
            "time interval",
            "the # of nodes at the current time step",
            "the # of edges at the current time step",
            "the # of edges lost over the last 365-day interval",
            "the # of edges gained over the last 365-day interval",
            "the # of nodes gained over the last 365-day interval",
            "the # of nodes lost over the last 365-day interval");
            string table = "Section2: \n" + title;
            foreach (Graph graph in graphList.Values)
            {
                table += string.Format("{0,-10} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10}\n",
                graph.StartTime, graph.LNodes.Count, graph.LEdges.Count, graph.CountLostEdge, graph.CountGainEdge,
                    graph.CountGainNode, graph.CountLostNode);
            }
            return table;
        }
        //section 3
        public string SectionThree(ReportFactory result)
        {
            string table = "Section3: \n";
            int[] interval = result.GetInterval();
            table += string.Format("{0,-10} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10} {8,10} {9,10} {10,10}\n",
            "time interval", interval[0], interval[1], interval[2], interval[3], interval[4],
                interval[5], interval[6], interval[7], interval[8], interval[9]);
            Dictionary<double, string> list = new Dictionary<double, string>();
                foreach(Graph graph in graphList.Values)
                {
                    int[] countDeg = result.CountDegree(graph);
                    table += string.Format("{0,-10} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10} {8,10} {9,10} {10,10}\n",
                    graph.StartTime, countDeg[0], countDeg[1], countDeg[2], countDeg[3], countDeg[4],
                        countDeg[5], countDeg[6], countDeg[7], countDeg[8], countDeg[9]);
                }
            return table;
        }
        //section 4
        public string SectionFour()
        {
            string table = "Section4: \n";

            return table;
        }
    }
}
