using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    /// <summary>
    /// ReportWriter class generate the report to the ideal format of tables
    /// </summary>
    /// <author>
    /// Zixuan(Nina) Hao
    /// </author>
    public class GeneralReportWriter
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        public GeneralReportWriter(ReportFactory result, string filePath)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();
            Condition.Requires(filePath, "path of file to write to")
                .IsNotEmpty()
                .IsNotNull()
                .EndsWith(".txt");

            GraphTimeToGraphObjectDict = result.GraphTimeToGraphObjectDict;

            string[] lines = { SectionOne(), SectionTwo(result), SectionThree(result)};
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        /// <summary>
        /// Section two.
        /// </summary>
        private string SectionOne()
        {
            //generate the table title
            string table = "";
            table += 
            "time interval\t" +
            "the # of nodes at the current time step\t" +
            "the # of edges at the current time step\t" +
            "the # of edges lost over the last 365-day interval\t" +
            "the # of edges gained over the last 365-day interval\t" +
            "the # of nodes gained over the last 365-day interval\t" +
            "the # of nodes lost over the last 365-day interval\n";

            Condition.Requires(GraphTimeToGraphObjectDict, "list of graph to report")
                .IsNotEmpty();

            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                table += 
                graph.GraphEndTime + "\t" + graph.NodeIdToNodeObjectDict.Count + "\t" + graph.EdgeIdToEdgeObjectDict.Count + "\t" + 
                    graph.CountLostEdge + "\t" + graph.CountGainEdge + "\t" +  graph.CountGainNode + "\t" + graph.CountLostNode + "\n";
            }

            Condition.Ensures(table, "section one report")
                .IsNotNullOrEmpty();
            return table;
        }
        /// <summary>
        /// Section Three.
        /// </summary>
        /// <param name="result">Result.</param>
        private string SectionTwo(ReportFactory result)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                   .IsNotNull();

            string table = "";
            int[] interval = result.GetIntervalOfTheDistribution(result.GetMaxDegree());
           
            Condition.Ensures(interval, "interval")
                .IsNotNull()
                .IsNotEmpty()
                .HasLength(10);

            table += "time interval\t" +  0 + "-" + interval[0] + "\t" + interval[0] + "-" + interval[1] + "\t" + interval[1] + "-" + interval[2] + "\t" +
                interval[2] + "-" + interval[3] + "\t" + interval[3] + "-" + interval[4] + "\t" + interval[4] + "-" + interval[5] + "\t" +
                interval[5] + "-" + interval[6] + "\t" + interval[6] + "-" + interval[7] + "\t" + interval[7] + "-" + interval[8] + "\t" +
                interval[8] + "-" + interval[9] + "\n";
                foreach(Graph graph in GraphTimeToGraphObjectDict.Values)
                {
                    int[] countDeg = result.CountDegreeWithInterval(graph);
                    
                    Condition.Ensures(countDeg, "array of counting the partner")
                        .IsNotNull()
                        .IsNotEmpty()
                        .HasLength(10);

                table += graph.GraphEndTime + "\t" + countDeg[0] + "\t" + countDeg[1] + "\t" + countDeg[2] + "\t" + countDeg[3] + "\t" + countDeg[4] + "\t" +
                        countDeg[5] + "\t" + countDeg[6] + "\t" + countDeg[7] + "\t" + countDeg[8] + "\t" + countDeg[9] + "\n";
                }
            
            Condition.Ensures(table, "section two report")
                .IsNotNullOrEmpty();

            return table;
        }
        /// <summary>
        /// Sections Four.
        /// </summary>
        /// <param name="result">Result.</param>
        private string SectionThree(ReportFactory result)
        {
            Condition.Requires(result, "result")
                .IsNotNull();

            string table = "";
            int[] interval = result.GetIntervalOfTheDistribution(result.GetMaxNumberOfCommonNeighbor());

            Condition.Ensures(interval, "interval")
                .IsNotNull()
                .IsNotEmpty()
                .HasLength(10);

            table += "time interval\t" + 0 + "-" + interval[0] + "\t" + interval[0] + "-" + interval[1] + "\t" + interval[1] + "-" + interval[2] + "\t" +
                interval[2] + "-" + interval[3] + "\t" + interval[3] + "-" + interval[4] + "\t" + interval[4] + "-" + interval[5] + "\t" +
                interval[5] + "-" + interval[6] + "\t" + interval[6] + "-" + interval[7] + "\t" + interval[7] + "-" + interval[8] + "\t" +
                interval[8] + "-" + interval[9] + "\n";
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                int[] countPartner = result.CountPartner(graph);

                Condition.Ensures(countPartner, "array of counting the partner")
                    .IsNotNull()
                    .IsNotEmpty()
                    .HasLength(10);

                table += graph.GraphEndTime + "\t" + countPartner[0] + "\t" + countPartner[1] + "\t" + countPartner[2] + "\t" + countPartner[3] + "\t" + countPartner[4] + "\t" +
                    countPartner[5] + "\t" + countPartner[6] + "\t" + countPartner[7] + "\t" + countPartner[8] + "\t" + countPartner[9] + "\n";
            }

            Condition.Ensures(table, "section three report")
                .IsNotNullOrEmpty();

            return table;
        }
    }
}
