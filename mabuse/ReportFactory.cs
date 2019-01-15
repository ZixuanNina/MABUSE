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
        public static Dictionary<double, Graph> graphList = new Dictionary<double, Graph>();
        public ReportFactory(Dictionary<double, Graph> graphL)
        {
            graphList = graphL;
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
            "time interval\the title", 
            "the # of nodes at the current time step",
            "the # of edges at the current time step", 
            "the # of edges lost over the last 365-day interval",
            "the # of edges gained over the last 365-day interval", 
            "the # of nodes gained over the last 365-day interval",
            "the # of nodes lost over the last 365-day interval");
            string table = "Section2: \n" + title;
            foreach(Graph graph in graphList.Values) 
            {
                table += string.Format("{0,-10} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10}\n", 
                graph.StartTime,graph.LNodes.Count,graph.LEdges.Count,graph.CountLostEdge,graph.CountGainEdge,
                    graph.CountGainNode,graph.CountLostNode);
            }
            return table;
        }
        //section 3
        public string SectionThree()
        {
            string table = "Section3: \n";
            
            Dictionary<double, string> list = new Dictionary<double, string>();
            foreach (double days in graphList.Keys)
            {
                table += days + 365 + "\n";
                table += string.Format("{0,-10} {1,10} \n","intervals","counts numbers");
                foreach (string key in graphList[days].GetMaxDegree().Keys)
                {
                    table += string.Format("{0, -10:G } {1, 10:N1 } \n", key, graphList[days].GetMaxDegree()[key]);
                }

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
