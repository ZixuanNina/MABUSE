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
            Compiler("/Users/zixuanhao/Projects/mabuse/mabuse/historian_test/case_one/trial_1/networks/trace.txt", 
            "/Users/zixuanhao/Projects/mabuse/mabuse/historian_test/case_one/result.txt");
           
        }
        public static void Compiler(string pathOfFile, string pathToFile)
        {
            Parser parser = new Parser(pathOfFile);
            Dictionary<double, Graph> graphL = parser.GetGraph();
            ReportFactory result = new ReportFactory(graphL);
            ReportWriter writer = new ReportWriter(result,pathToFile);
        }
    }
}
