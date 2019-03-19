using System;
using CuttingEdge.Conditions;

namespace mabuse
{
    /// <summary>
    /// Prograpm compile and run the project ot get the report of MABUSE data.
    /// </summary>
    /// <author>
    /// Zixuan(Nina) Hao
    /// </author>
    class MainClass
    {
        private static readonly string GENERAL_REPORT_OUTPUT_FILENAME = "GeneralReport.txt";
        private static readonly string NODEDEGREE_REPORT_OUTPUT_FILENAME = "NodeDegreeReport.txt";
        private static readonly string COMMAND_SELECETING_REPORTFILE = "-s";

        public static void Main(string[] args)
        {
            //input parameter condition check
            Condition.Requires(args, "arguments")
                .IsNotNull()            // throw AgumentNullException if there is no input
                .IsNotEmpty()
                .IsLongerThan(1);    // throw AugumentException due to missing input information
            if (args[1].Equals(COMMAND_SELECETING_REPORTFILE))
            {
                ReportGenerateBySelection(args[0], args[2], args[3]);
            }
            else
            {
                ReportGenerateWithPathsGiven(args[0], args[1], args[2]);
            }
        }

        /// <summary>
        /// Compiler the specified pathOfFile and pathToFiles.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="pathToFileA">Path to file.</param>
        private static void ReportGenerateWithPathsGiven(string pathOfFile, string pathToFileA, string pathToFileB)
        {
            //input parameter condition check
            Condition.Requires(pathOfFile, "path of file for reading")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileA, "path to write to the file")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFileB, "path to write to the file")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            Parser parser = new Parser(pathOfFile);
            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph create by parser")
                .IsNotNull()
                .IsNotEmpty();
            ReportsToWrite(parser, pathToFileA, pathToFileB);
        }

        /// <summary>
        /// Reports to write.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <param name="pathToFileA">Path to file a.</param>
        /// <param name="pathToFileB">Path to file b.</param>
        private static void ReportsToWrite(Parser parser, string pathToFileA, string pathToFileB)
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

            ReportFactory reportFactory = new ReportFactory(parser);
            NodeDegreeReportFactory nodeDegreeReportFactory = new NodeDegreeReportFactory(parser);

            Condition.Ensures(reportFactory, "result of parser")
                .IsNotNull();
            Condition.Ensures(nodeDegreeReportFactory, "result of parser")
                .IsNotNull();
            GeneralReportWriter generalReportWriter = new GeneralReportWriter(reportFactory, pathToFileA);
            NodeDegreeReportWritter nodeDegreeReport = new NodeDegreeReportWritter(nodeDegreeReportFactory, pathToFileB);
        }

        /// <summary>
        /// Reports the generate by selection.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="cases">Cases.</param>
        /// <param name="Directory">Directory.</param>
        private static void ReportGenerateBySelection(string pathOfFile, string cases, string Directory)
        {
            //input parameter condition check
            Condition.Requires(pathOfFile, "path of file for reading")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            Parser parser = new Parser(pathOfFile);

            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph creat by parser")
                .IsNotNull()
                .IsNotEmpty();
                

            String pathToFileA = Directory + GENERAL_REPORT_OUTPUT_FILENAME,
            pathToFileB = Directory + NODEDEGREE_REPORT_OUTPUT_FILENAME;
            switch (cases)
            {
                case "d":
                    ReportsToWrite(parser, pathToFileA, pathToFileB);
                    break;
                case "gen":
                    ReportFactory reportFactory1 = new ReportFactory(parser);
                    Condition.Ensures(reportFactory1, "result of parser")
                        .IsNotNull();
                    GeneralReportWriter generalReportWriter1 = new GeneralReportWriter(reportFactory1, pathToFileA);
                    break;
                case "ND":
                    NodeDegreeReportFactory nodeDegreeReportFactory2 = new NodeDegreeReportFactory(parser);
                    Condition.Ensures(nodeDegreeReportFactory2, "result of parser")
                        .IsNotNull();
                    NodeDegreeReportWritter nodeDegreeReport2 = new NodeDegreeReportWritter(nodeDegreeReportFactory2, pathToFileB);
                    break;
                default:
                    throw new InvalidOperationException("unknown commend");
            }
        }

    }
}
