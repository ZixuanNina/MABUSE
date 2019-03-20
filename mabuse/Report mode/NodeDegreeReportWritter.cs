using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse
{
    /// <summary>
    /// Node degree report writter.
    /// </summary>
    public class NodeDegreeReportWritter
    {
        public Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.NodeDegreeReportWritter"/> class.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="filePath">File path.</param>
        public NodeDegreeReportWritter(NodeDegreeReportFactory result, string filePath)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();
            Condition.Requires(filePath, "path of file to write to")
                .IsNotEmpty()
                .IsNotNull()
                .EndsWith(".txt");

            GraphTimeToGraphObjectDict = result.GraphTimeToGraphObjectDict;

            string[] lines = { SectionTwo(result)};
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        /// <summary>
        /// Section Two.
        /// </summary>
        /// <returns>The five.</returns>
        /// <param name="result">Result.</param>
        private string SectionTwo(NodeDegreeReportFactory result)
        {
            Condition.Requires(result, "Result")
                .IsNotNull();

            string table = "";
            string title = "Node Id";
            foreach (Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                title += "\t" + graph.GraphEndTime;
            }
            table += title;

            Dictionary<string, int[]> NodeIsNodeIdToItsDegree = result.GetNodeDegrees();

            foreach (string id in NodeIsNodeIdToItsDegree.Keys)
            {
                table += "\n"+id;
                foreach (int count in NodeIsNodeIdToItsDegree[id])
                {
                    table += string.Format("\t" + count);
                }
            }
            return table;
        }
    }
}
