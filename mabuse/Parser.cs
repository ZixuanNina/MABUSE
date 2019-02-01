/*
Author: Zixuan(Nina) Hao
Purpose:
    This class manage the file reading and parsing the information store in thr Dictionary list
 */
using System;
using System.Collections.Generic;
using System.IO;
using mabuse.datamode;


namespace mabuse
{
    public class Parser
    {

        public Dictionary<string, Node> lNodes = new Dictionary<string, Node>();
        public Dictionary<string, Edge> lEdges = new Dictionary<string, Edge>();
        public Dictionary<double, Graph> lGraphs = new Dictionary<double, Graph>();
        public Parser(string pathOfFile)
        {
            //initialization
            int countL = 0;
            int countGainNode = 0;
            int countLostNode = 0;
            int countGainEdge = 0;
            int countLostEdge = 0;
            double timeInterVal = 365;
            double graphTime = 0;
            //read file line by line
            string[] lines = File.ReadAllLines(pathOfFile);

            foreach(string line in lines)
            {
                //counting the lines
                countL++;
                string[] str = line.Split(' ');
                //This part could be future redefined since this time did not consider the layer issue.
                //only generate the simple nodes and edges graph
                //need to check and save the nodes with its id.
                //process data with the respond command And check missing information
                if (str.Length >= 3)
                { 
                    //convert the string time to double
                    double time = Convert.ToDouble(str[0].Replace(":", ""));
                    string command = str[1] + " " + str[2];
                    if (!lGraphs.ContainsKey(time) && (time%timeInterVal).Equals(0))
                    {
                        lGraphs.Add(time, new Graph { StartTime = time, EndTime = 365 + time });
                        //save the counts
                        lGraphs[graphTime].CountGainEdge = countGainEdge;
                        lGraphs[graphTime].CountLostEdge = countLostEdge;
                        lGraphs[graphTime].CountGainNode = countGainNode;
                        lGraphs[graphTime].CountLostNode = countLostNode;
                        countGainNode = countLostNode = countGainEdge = countLostEdge = 0;
                        //generate the next graph and transit the exist nodes and edges to the current new graph
                        lGraphs[time].LNodes = lGraphs[graphTime].LNodes;
                        lGraphs[time].LEdges = lGraphs[graphTime].LEdges;
                        graphTime = time;
                    }
                    //switch case
                    switch (command)
                    {
                        case "add node":
                            //increment the gained node
                            countGainNode++;
                            //add node set the start time
                            lNodes.Add(str[3],new Node { NodeId = str[3], NodeStartT = time});
                            lGraphs[graphTime].LNodes.Add(str[3], new Node { NodeId = str[3], NodeStartT = time});
                            break;
                        case "remove node":
                            //increment lose node
                            countLostNode++;
                            //remove node set the end time 
                            lNodes[str[3]].NodeEndT = time;
                            lGraphs[graphTime].LNodes.Remove(str[3]);
                            //remove all the connected edge, set the end time the same as the node
                            foreach(Edge edge in lNodes[str[3]].LEdges.Values)
                            {
                                lNodes[str[3]].LEdges[edge.EdgeId].EdgeEndT = time;
                                lEdges[edge.EdgeId].EdgeEndT = time;
                                if (lGraphs[graphTime].LEdges.ContainsValue(edge))
                                {
                                    lGraphs[graphTime].LEdges.Remove(edge.EdgeId);
                                }
                            }
                            //remove all neighbor status of neighbor nodes
                            foreach (Node node in lNodes[str[3]].LNodesNeighbors.Values)
                            {
                                if (node.LNodesNeighbors.ContainsKey(str[3])) 
                                { 
                                    node.LNodesNeighbors[str[3]].NodeEndT = time;
                                    if (lGraphs[graphTime].LNodes[node.NodeId].LNodesNeighbors.ContainsKey(str[3]))
                                    {
                                        lGraphs[graphTime].LNodes[node.NodeId].LNodesNeighbors.Remove(node.NodeId);
                                    }
                                }
                            }
                            break;
                        case "add edge":
                            if (!(lEdges.ContainsKey(str[4] + "-" + str[5])) && !(lEdges.ContainsKey(str[5] + "-" + str[4])))
                            {
                                //increment gain edge
                                countGainEdge++;
                                //set the start time of edge
                                lEdges.Add(str[4] + "-" + str[5], new Edge { EdgeId = str[4] + "-" + str[5], NodeA = str[4], NodeB = str[5], EdgeStartT = time });
                                lGraphs[graphTime].LEdges.Add(str[4] + "-" + str[5], new Edge { EdgeId = str[4] + "-" + str[5], NodeA = str[4], NodeB = str[5], EdgeStartT = time });
                                //add the neighbor to each node
                                lNodes[str[4]].LNodesNeighbors.Add(str[5], new Node { NodeId = str[5], NodeStartT = time });
                                lNodes[str[5]].LNodesNeighbors.Add(str[4], new Node { NodeId = str[4], NodeStartT = time });
                                lGraphs[graphTime].LNodes[str[4]].LNodesNeighbors.Add(str[5], new Node { NodeId = str[5], NodeStartT = time });
                                lGraphs[graphTime].LNodes[str[5]].LNodesNeighbors.Add(str[4], new Node { NodeId = str[4], NodeStartT = time });
                                //add the edge to the node
                                lNodes[str[4]].LEdges.Add(str[4] + "-" + str[5], new Edge { EdgeId = str[4] + "-" + str[5], NodeA = str[4], NodeB = str[5], EdgeStartT = time });
                                lGraphs[graphTime].LNodes[str[4]].LEdges.Add(str[4] + "-" + str[5], new Edge { EdgeId = str[4] + "-" + str[5], NodeA = str[4], NodeB = str[5], EdgeStartT = time });
                                lGraphs[graphTime].LNodes[str[5]].LEdges.Add(str[4] + "-" + str[5], new Edge { EdgeId = str[4] + "-" + str[5], NodeA = str[4], NodeB = str[5], EdgeStartT = time });
                            }
                            break;
                        case "remove edge":
                            if (lEdges.ContainsKey(str[4] + "-" + str[5]))
                            {
                                //increment lost edge
                                countLostEdge++;
                                //set the end time of edge
                                lEdges[str[4] + "-" + str[5]].EdgeEndT = time;
                                lGraphs[graphTime].LEdges.Remove(str[4] + "-" + str[5]);
                                //remove neighbor
                                lNodes[str[4]].LNodesNeighbors[str[5]].NodeEndT = time;
                                lNodes[str[5]].LNodesNeighbors[str[4]].NodeEndT = time;
                                lGraphs[graphTime].LNodes[str[4]].LNodesNeighbors.Remove(str[5]);
                                lGraphs[graphTime].LNodes[str[5]].LNodesNeighbors.Remove(str[4]);
                                //remove edge to the node
                                lNodes[str[4]].LEdges[str[4] + "-" + str[5]].EdgeEndT = time;
                                lGraphs[graphTime].LNodes[str[4]].LEdges.Remove(str[4] + "-" + str[5]);
                                lGraphs[graphTime].LNodes[str[5]].LEdges.Remove(str[4] + "-" + str[5]);
                            }
                            break;
                        default:
                            Console.WriteLine("new command detected: " + command);
                            break;

                    }
                }
                else
                {
                    //get the error line for debuging
                    if (!line.Length.Equals(0)) {
                        Console.WriteLine("missing node information at line: " + countL);
                    }
                }
            }
        }
        public Dictionary<double, Graph> GetGraph()
        {
            return lGraphs;
        }
    }
}
