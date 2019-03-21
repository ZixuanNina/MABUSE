using System;
using CuttingEdge.Conditions;
using mabuse.Reportmode;

namespace mabuse
{
    public class ReportOrganize
    {
        private static readonly string GENERAL_REPORT_OUTPUT_FILENAME = "GeneralReport.txt";
        private static readonly string NODEDEGREE_REPORT_OUTPUT_FILENAME = "NodeDegreeReport.txt";
        private static readonly string EDGE_PARTNERWISE_COUNT_OUTPUT_FILENAME = "EdgewiseSharedPartnerReport.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.ReportOrganize"/> class.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="pathToFileA">Path to file a.</param>
        /// <param name="pathToFileB">Path to file b.</param>
        /// <param name="pathToFileC">Path to file c.</param>
        public ReportOrganize(string pathOfFile, string pathToFileA, string pathToFileB, string pathToFileC)
        {
            Condition.Requires(pathOfFile, "path of file for reading")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileA, "path to write to the fileA")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileB, "path to write to the fileB")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileC, "path to write to the fileC")
                .IsNotNullOrEmpty()
                .EndsWith(".txt");

            Parser parser = new Parser(pathOfFile);
            ReportsToWrite(parser, pathToFileA, pathToFileB, pathToFileC);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:mabuse.ReportOrganize"/> class.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="cases">Cases.</param>
        /// <param name="Directory">Directory.</param>
        public ReportOrganize(string pathOfFile, string cases, string Directory)
        {
            Condition.Requires(pathOfFile, "path of file for reading")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            Parser parser = new Parser(pathOfFile);

            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph creat by parser")
                .IsNotNull()
                .IsNotEmpty();

            SelectReport(parser, cases, Directory);

        }

        /// <summary>
        /// Selects the report.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="cases">Cases.</param>
        /// <param name="Directory">Directory.</param>
        private void SelectReport(Parser parser, string cases, string Directory)
        {
            String pathToFileA = Directory + GENERAL_REPORT_OUTPUT_FILENAME,
            pathToFileB = Directory + NODEDEGREE_REPORT_OUTPUT_FILENAME,
            pathToFileC = Directory + EDGE_PARTNERWISE_COUNT_OUTPUT_FILENAME;

            switch (cases)
            {
                case "-ALL":
                    ReportsToWrite(parser, pathToFileA, pathToFileB, pathToFileC);
                    break;
                case "-GEN":
                    GenReport_Write(parser, pathToFileA);
                    break;
                case "-ND":
                    NodeDegreeReport_Write(parser, pathToFileB);
                    break;
                case "-EW":
                    EdgewiseSharedPartnerCountReport_Write(parser, pathToFileC);
                    break;
                default:
                    throw new InvalidOperationException("unknown commend");
            }
        }

        /// <summary>
        /// write all reports.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="pathToFileA">Path to file a.</param>
        /// <param name="pathToFileB">Path to file b.</param>
        /// <param name="pathToFileC">Path to file c.</param>
        private static void ReportsToWrite(Parser parser, string pathToFileA, string pathToFileB, string pathToFileC)
        {
            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph create by parser")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(pathToFileA, "path to write to the fileA")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileB, "path to write to the fileB")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileC, "path to write to the fileC")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            GenReport_Write(parser, pathToFileA);
            NodeDegreeReport_Write(parser, pathToFileB);
            EdgewiseSharedPartnerCountReport_Write(parser, pathToFileC);
        }

        /// <summary>
        /// Wite general report.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="pathToFile">Path to file.</param>
        private static void GenReport_Write(Parser parser, string pathToFile)
        {
            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph create by parser")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(pathToFile, "path to write to the fileA")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            ReportFactory reportFactory = new ReportFactory(parser);

            Condition.Ensures(reportFactory, "result of parserA")
                .IsNotNull();
            GeneralReportWriter generalReportWriter = new GeneralReportWriter(reportFactory, pathToFile);
        }

        /// <summary>
        /// write Node degree report.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="pathToFile">Path to file.</param>
        private static void NodeDegreeReport_Write(Parser parser, string pathToFile)
        {
            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph create by parser")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(pathToFile, "path to write to the fileA")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            NodeDegreeReportFactory nodeDegreeReportFactory = new NodeDegreeReportFactory(parser);

            Condition.Ensures(nodeDegreeReportFactory, "result of parserB")
                .IsNotNull();

            NodeDegreeReportWritter nodeDegreeReport = new NodeDegreeReportWritter(nodeDegreeReportFactory, pathToFile);
        }

        /// <summary>
        /// write Edgewises the shared partner count report.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="pathToFile">Path to file.</param>
        private static void EdgewiseSharedPartnerCountReport_Write(Parser parser, string pathToFile)
        {
            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph create by parser")
                .IsNotNull()
                .IsNotEmpty();
            Condition.Requires(pathToFile, "path to write to the fileA")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            EdgewiseSharedPartnerReportFactory edgewiseSharedPartnerReportFactory = new EdgewiseSharedPartnerReportFactory(parser);

            Condition.Ensures(edgewiseSharedPartnerReportFactory, "result of parserC")
                .IsNotNull();
            EdgewiseSharedPartnerReportWritter edgewiseSharedPartnerReportWritter = new EdgewiseSharedPartnerReportWritter(edgewiseSharedPartnerReportFactory, pathToFile);
        }
    }
}
