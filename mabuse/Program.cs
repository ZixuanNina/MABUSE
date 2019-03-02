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
            Compiler(args[0], args[1]);
        }

        /// <summary>
        /// Compiler the specified pathOfFile and pathToFile.
        /// </summary>
        /// <param name="pathOfFile">Path of file.</param>
        /// <param name="pathToFile">Path to file.</param>
        private static void Compiler(string pathOfFile, string pathToFile)
        {
            Parser parser = new Parser(pathOfFile);
            ReportFactory result = new ReportFactory(parser.GetGraphTimeToGraphDictionary());
            ReportWriter writer = new ReportWriter(result,pathToFile);
        }
    }
}
