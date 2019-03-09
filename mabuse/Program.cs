using CuttingEdge.Conditions;
using System;

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
                .IsLongerOrEqual(2);    // throw AugumentException due to missing input information

            Compiler(args[0], args[1]);
        }

        /// <summary>
        /// Compiler the specified pathOfFile and pathToFile.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="pathToFile">Path to file.</param>
        private static void Compiler(string pathOfFile, string pathToFile)
        {
            //input parameter condition check
            Condition.Requires(pathOfFile, "path of file for reading")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();
            Condition.Requires(pathToFile, "path to write to the file")
                .IsNotNullOrEmpty()
                .EndsWith(".txt")
                .IsNotNullOrWhiteSpace();

            Parser parser = new Parser(pathOfFile);

            Condition.Ensures(parser.GetGraphTimeToGraphDictionary(), "graph creat by parser")
                .IsNotNull()
                .IsNotEmpty();

            ReportFactory result = new ReportFactory(parser);

            Condition.Ensures(result, "result of parser")
                .IsNotNull();

            ReportWriter writer = new ReportWriter(result,pathToFile);
        }
    }
}
