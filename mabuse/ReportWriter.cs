/*
Author: Zixuan(Nina) Hao
Purpose:
    ReportWriter class generate the report to the ideal format of tables
 */

using System;
namespace mabuse
{
    public class ReportWriter
    {
        public ReportWriter(ReportFactory result, string filePath)
        {
            string[] lines = { result.Section1(), result.SectionTwo(), result.SectionThree(), result.SectionFour()};
            System.IO.File.WriteAllLines(@filePath, lines);
        }
    }
}
