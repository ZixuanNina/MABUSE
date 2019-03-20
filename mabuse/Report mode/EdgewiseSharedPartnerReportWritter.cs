using System;
using System.Collections.Generic;
using CuttingEdge.Conditions;
using mabuse.datamode;

namespace mabuse.Reportmode
{
    /// <summary>
    /// Edgewise shared partner report writter.
    /// </summary>
    public class EdgewiseSharedPartnerReportWritter
    {
        private readonly Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.Reportmode.EdgewiseSharedPartnerReportWritter"/> class.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="filePath">File path.</param>
        public EdgewiseSharedPartnerReportWritter(EdgewiseSharedPartnerReportFactory result, string filePath)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();
            Condition.Requires(filePath, "path of file to write to")
                .IsNotEmpty()
                .IsNotNull()
                .EndsWith(".txt");

            GraphTimeToGraphObjectDict = result.GraphTimeToGraphObjectDict;
            string[] lines = { SectionOne(result) };
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        /// <summary>
        /// Section one.
        /// </summary>
        /// <returns>The one.</returns>
        /// <param name="result">Result.</param>
        private string SectionOne(EdgewiseSharedPartnerReportFactory result)
        {
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();

            Dictionary<string, Edge> EdgeWithPartnerwiseCount = result.CountPartnerwiseOfEdge();

            string table = "Edge Id";
            foreach(Graph graph in GraphTimeToGraphObjectDict.Values)
            {
                table += "\t" + graph.GraphEndTime;
            }

            foreach(Edge edge in EdgeWithPartnerwiseCount.Values)
            {
                table += "\n" + edge.EdgeId;
                foreach(int count in edge.CountPartnerwiseByTime)
                {
                    table += "\t" + count;
                }
            }

            return table;
        }
    }
}
