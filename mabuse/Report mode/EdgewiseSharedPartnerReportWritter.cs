using System;
using CuttingEdge.Conditions;

namespace mabuse.Reportmode
{
    public class EdgewiseSharedPartnerReportWritter
    {

        
        public EdgewiseSharedPartnerReportWritter(EdgewiseSharedPartnerReportFactory result, string filePath)
        {
            //input parameter condition check
            Condition.Requires(result, "the analyzed result")
                .IsNotNull();
            Condition.Requires(filePath, "path of file to write to")
                .IsNotEmpty()
                .IsNotNull()
                .EndsWith(".txt");


            string[] lines = { SectionOne(result) };
            System.IO.File.WriteAllLines(@filePath, lines);
        }

        private string SectionOne(EdgewiseSharedPartnerReportFactory result)
        {
            throw new NotImplementedException();
        }
    }
}
