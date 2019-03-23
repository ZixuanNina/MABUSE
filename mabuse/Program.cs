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
            if (args[1].Equals("-help"))
            {
                HelpTxt();
            }
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
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Try to get help with ");
                    Console.WriteLine("mabuse -help");
                }
            }
        }

        private static void HelpTxt()
        {
            Console.WriteLine(">>Thanks for using mabusa data analysing report");
            Console.WriteLine(">>To report with your choice of file name:");
            Console.WriteLine("<path of file for analyzing> <path of file_Define_name for General report> <For Node Degree Report> <For EdgewisePartner Report>");
            Console.WriteLine("To report by selection:");
            Console.WriteLine("<path of file for analyzing> -ALL \"Generate all reports\"");
            Console.WriteLine("                             -GEN \"Generate general report\"");
            Console.WriteLine("                             -ND  \"Node Degree report\"");
            Console.WriteLine("                             -EW  \"Edgewise Shared partner count report\"");
            Console.WriteLine("                                 <path of directory to save reports>");
        }
    }
}
