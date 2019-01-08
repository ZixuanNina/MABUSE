using System;
using System.IO;
using mabuse.datamode;

namespace mabuse
{
    public class Parser
    {
        public Parser(string pathOfFile)
        {
            //initialization
            int count = 0;
            Node nodes = new Node();
            Edge edges = new Edge();
            string[] lines = File.ReadAllLines(pathOfFile);
            foreach(string line in lines)
            {
                count++;
                //temp remove the layer command.
                string l = line.Replace("drug_co_use", "");
                string[] str = l.Split(' ');
                //This part could be future redefined since this time did not consider the layer issue.
                //only generate the simple nodes and edges graph
                //need to check and save the nodes with its id.
                string command = str[1] + " " + str[2];

                //process data with the respond command And check missing information
                if (str.Length > 3)
                {
                    switch (command)
                    {
                        case "add node":
                            //add node
                            nodes.Add(str[3]);
                            break;
                        case "remove node":
                            //remove node
                            nodes.Remove(str[3]);
                            //check the edge and remove
                            throw new NotImplementedException();
                            break;
                        case "add edge":
                            Edge.Add();
                            break;
                        case "remove edge":

                            break;
                        default:
                            Console.WriteLine("new command detected: " + command);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("missing node information at line: " + count );
                }
            }
        }
    }
}
