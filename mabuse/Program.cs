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
        public static void Main(string[] args)
        {
            //input parameter condition check
            Condition.Requires(args, "arguments")
                .IsNotNull()            // throw AgumentNullException if there is no input
                .IsNotEmpty()
                .IsLongerThan(1);    // throw AugumentException due to missing input information
            if (args[1].Equals("-s"))
            {
                MultiCompiler(args[0], args[2], args[3]);
            }
            else
            {
                Compiler(args[0], args[1], args[2]);
            }
        }

        /// <summary>
        /// Compiler the specified pathOfFile and pathToFile.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="pathToFileA">Path to file.</param>
        private static void Compiler(string pathOfFile, string pathToFileA, string pathToFileB)
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

            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph creat by parser")
                .IsNotNull()
                .IsNotEmpty();

            ReportFactory reportFactory = new ReportFactory(parser);
            NodeDegreeReportFactory nodeDegreeReportFactory = new NodeDegreeReportFactory(parser);

            Condition.Ensures(reportFactory, "result of parser")
                .IsNotNull();
            Condition.Ensures(nodeDegreeReportFactory, "result of parser")
                .IsNotNull();

            GeneralReportWriter generalReportWriter = new GeneralReportWriter(reportFactory,pathToFileA);

            NodeDegreeReportWritter nodeDegreeReport = new NodeDegreeReportWritter(nodeDegreeReportFactory, pathToFileB);
        }

        private static void MultiCompiler(string pathOfFile, string cases, string Directory)
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
                

            String pathToFileA, pathToFileB;
            switch (cases)
            {
                case "d":
                    ReportFactory reportFactory = new ReportFactory(parser);
                    NodeDegreeReportFactory nodeDegreeReportFactory = new NodeDegreeReportFactory(parser);

                    Condition.Ensures(reportFactory, "result of parser")
                        .IsNotNull();
                    Condition.Ensures(nodeDegreeReportFactory, "result of parser")
                        .IsNotNull();
                    pathToFileA = Directory + "GeneralReport.txt";
                    pathToFileB = Directory + "NodeDegreeReport.txt";
                    GeneralReportWriter generalReportWriter = new GeneralReportWriter(reportFactory, pathToFileA);
                    NodeDegreeReportWritter nodeDegreeReport = new NodeDegreeReportWritter(nodeDegreeReportFactory, pathToFileB);
                    break;
                case "gen":
                    ReportFactory reportFactory1 = new ReportFactory(parser);
                    Condition.Ensures(reportFactory1, "result of parser")
                        .IsNotNull();
                    pathToFileA = Directory + "GeneralReport.txt";
                    GeneralReportWriter generalReportWriter1 = new GeneralReportWriter(reportFactory1, pathToFileA);
                    break;
                case "ND":
                    NodeDegreeReportFactory nodeDegreeReportFactory2 = new NodeDegreeReportFactory(parser);
                    Condition.Ensures(nodeDegreeReportFactory2, "result of parser")
                        .IsNotNull();
                    pathToFileB = Directory + "NodeDegreeReport.txt";
                    NodeDegreeReportWritter nodeDegreeReport2 = new NodeDegreeReportWritter(nodeDegreeReportFactory2, pathToFileB);
                    break;
                default:
                    throw new InvalidOperationException("unknown commend");
            }
        }

        //Plan on add a method to handle the repots
    }
}
