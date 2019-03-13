using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    public class NodeDegreeReportWritter
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public NodeDegreeReportWritter(ReportFactory result, string filePath)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();
            Condition.Requires(filePath, "path of file to write to")
                .IsNotEmpty()
                .IsNotNull()
                .EndsWith(".txt");

            GraphTimeToGraphObjectDict = result.GraphTimeToGraphObjectDict;

            string[] lines = { SectionOne(), SectionTwo(result)};
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        /// <summary>
        /// Section One
        /// </summary>
        private string SectionOne()
        {
            string title = "MABUSE Node degree Report\n";
            string paragraph = "This report output the node degree at each 365 days interval. \n";
            string reportTime = "Report Date: " + DateTime.Today.ToString("D") + "\nReport Time: " + DateTime.Now.ToString("h:mm:ss tt" + "\n");
            string section1 = title + Environment.NewLine + paragraph + Environment.NewLine + reportTime + Environment.NewLine;

            Condition.Ensures(section1, "section one report")
                .IsNotNullOrEmpty();

            return section1;
        }

        /// <summary>
        /// Section Two.
        /// </summary>
        /// <returns>The five.</returns>
        /// <param name="result">Result.</param>
        private string SectionTwo(ReportFactory result)
        {
            Condition.Requires(result, "Result")
                .IsNotNull();

            string table = "Node degree Report\n Section5: \n";
            string title = string.Format("{0, -40}", "Node Id");
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                title += string.Format("{0,-10}", graph.GraphEndTime);
            }
            table += title + "";

            Dictionary<string, int[]> NodeIsNodeIdToItsDegree = result.GetNodeDegrees();

            foreach (string id in NodeIsNodeIdToItsDegree.Keys)
            {
                table += string.Format("\n{0, -40}", id);
                foreach (int count in NodeIsNodeIdToItsDegree[id])
                {
                    table += string.Format("{0,-10}", count);
                }
            }
            return table;
        }
    }
}
