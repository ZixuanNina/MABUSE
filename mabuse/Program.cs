using System;
using CuttingEdge.Conditions;
using mabuse.Reportmode;

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

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            //input parameter condition check
            Condition.Requires(args, "arguments")
                .IsNotNull()            // throw AgumentNullException if there is no input
                .IsNotEmpty()
                .IsLongerThan(1);    // throw AugumentException due to missing input information

            //handle multiple file selection
            for (int i = 1; i < args.Length - 1; i++)
            {
                if (args[i].Contains("-"))
                {
                    ReportOrganize report = new ReportOrganize(args[0], args[i], args[args.Length - 1]);
                }
                else if (args[i].EndsWith(".txt", StringComparison.CurrentCulture))
                {
                    ReportOrganize report = new ReportOrganize(args[0], args[1], args[2], args[3]);
                    break;
                }
                else
                {
                    throw new ArgumentException("Wrong argument input");
                }
            }
        }
    }
}
