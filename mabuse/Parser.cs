using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mabuse.datamode;

namespace mabuse
{
    public class Parser
    {
        public Parser(string pathOfFile)
        {
            //initialization
            Dictionary<string, Node> lNodes = new Dictionary<string,Node>();
            Dictionary<string, Edge> lEdges = new Dictionary<string, Edge>();
            Dictionary<string, Graph> lGraphs = new Dictionary<string, Graph>();
            int count = 0;
            //read file line by line
            string[] lines = File.ReadAllLines(pathOfFile);
            foreach(string line in lines)
            {
                //counting the lines
                count++;
                string[] str = line.Split(' ');
                //This part could be future redefined since this time did not consider the layer issue.
                //only generate the simple nodes and edges graph
                //need to check and save the nodes with its id.
                string command = str[1] + " " + str[2];
                //convert the string time to double
                double time = Convert.ToDouble(str[0]);
                //process data with the respond command And check missing information
                if (str.Length >= 3)
                {
                    switch (command)
                    {
                        case "add node":
                            //add node set the start time
                            lNodes.Add(str[3],new Node { NodeId = str[3], NodeStartT = time });
                            break;
                        case "remove node":
                            //remove node set the end time 
                            lNodes[str[3]].NodeEndT = time;
                            //remove all the connected edge, set the end time the same as the node
                            foreach(Edge edge in lNodes[str[3]].LEdges.Values)
                            {
                                edge.EdgeEndT = time;
                                lEdges[edge.EdgeId].EdgeEndT = time;
                            }
                            //remove all neighbor sttatus of neighbor nodes
                            foreach(Node node in lNodes[str[3]].LNodesNeighbors.Values)
                            {
                                node.LNodesNeighbors[str[3]].NodeEndT = time;
                            }
                            break;
                        case "add edge":
                            //set the start time of edge
                            lEdges.Add(str[4]+"-"+str[5],new Edge { NodeA = str[4],NodeB = str[5],EdgeStartT = time});
                            //add the neighbor to each node
                            lNodes[str[4]].LNodesNeighbors.Add(str[5], new Node { NodeId = str[5], NodeStartT = time});
                            lNodes[str[5]].LNodesNeighbors.Add(str[4], new Node { NodeId = str[4], NodeStartT = time});
                            //add the edge to the node
                            lNodes[str[4]].LEdges.Add(str[4] + "-" + str[5], new Edge { NodeA = str[4], NodeB = str[5], EdgeStartT = time});
                            break;
                        case "remove edge":
                            //set the end time of edge
                            lEdges[str[4] + "-" + str[5]].EdgeEndT = time;
                            //remove neighbor
                            lNodes[str[4]].LNodesNeighbors[str[5]].NodeEndT = time;
                            lNodes[str[5]].LNodesNeighbors[str[4]].NodeEndT = time;
                            //remove edge to the node
                            lNodes[str[4]].LEdges[str[4] + "-" + str[5]].EdgeEndT = time;
                            break;
                        default:
                            Console.WriteLine("new command detected: " + command);
                            break;
                    }
                }
                else
                {
                    //get the error line for debuging
                    Console.WriteLine("missing node information at line: " + count );
                }
            }
        }
    }
}
