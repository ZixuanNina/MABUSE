/*
Author: Zixuan(Nina) Hao
Purpose:
Prograpm compile and run the project ot get the report of MABUSE data.
 */

using System;
using System.Collections.Generic;
using mabuse.datamode;

namespace mabuse
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Compiler(args[0], args[1]);
        }
        public static void Compiler(string pathOfFile, string pathToFile)
        {
            Parser parser = new Parser(pathOfFile);
            Dictionary<double, Graph> graphL = parser.GetGraph();
            ReportFactory result = new ReportFactory(graphL, parser.GetNodeL());
            ReportWriter writer = new ReportWriter(result,pathToFile);
        }
    }
}
