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
        public int countGainNode;
        public int countLostNode;
        public int countGainEdge;
        public int countLostEdge;
        public Parser(string pathOfFile)
        {
            //initialization
            countGainNode = 0;
            countLostNode = 0;
            countGainEdge = 0;
            countLostEdge = 0;
            int countL = 0;
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
                        foreach (Node node in lGraphs[graphTime].LNodes.Values)
                        {
                            lGraphs[time].LNodes.Add(node.NodeId, node);
                        }
                        foreach (Edge edge in lGraphs[graphTime].LEdges.Values)
                        {
                            lGraphs[time].LEdges.Add(edge.EdgeId, edge);
                        }
                        graphTime = time;
                    }
                    //switch case
                    switch (command)
                    {
                        case "add node":
                            AddNode(str[3], graphTime, time);
                            break;
                        case "remove node":
                            RemoveNode(str[3], graphTime, time);
                            break;
                        case "add edge":
                            AddEdge(str[4], str[5], graphTime, time);
                            break;
                        case "remove edge":
                            RemoveEdge(str[4], str[5], graphTime, time);
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

        //commands functions
        //Add node
        public void AddNode(string node, double graphTime, double time)
        {
            //increment the gained node
            countGainNode++;
            //add node set the start time
            lNodes.Add(node, new Node { NodeId = node, NodeStartT = time });
            lGraphs[graphTime].LNodes.Add(node, new Node { NodeId = node, NodeStartT = time });
        }

        //remove node and nodes' neighbors in the node
        public void RemoveNode(string node, double graphTime, double time)
        {
            //increment lose node
            countLostNode++;
            //remove node set the end time 
            lNodes[node].NodeEndT = time;
            lGraphs[graphTime].LNodes.Remove(node);
            //remove all the connected edge, set the end time the same as the node
            foreach (Edge edge in lNodes[node].LEdges.Values)
            {
                lNodes[node].LEdges[edge.EdgeId].EdgeEndT = time;
                lEdges[edge.EdgeId].EdgeEndT = time;
                lEdges[edge.EdgeId].NodeA.LEdges[edge.EdgeId].EdgeEndT = time;
                lEdges[edge.EdgeId].NodeB.LEdges[edge.EdgeId].EdgeEndT = time;
                if (lGraphs[graphTime].LEdges.ContainsKey(edge.EdgeId))
                {
                    lGraphs[graphTime].LEdges.Remove(edge.EdgeId);
                }
            }
            //remove all neighbor status of neighbor nodes

            foreach (Node nodeTemp in lNodes[node].LNodesNeighbors.Values)
            {

                if (nodeTemp.LNodesNeighbors.ContainsKey(node))
                {
                    nodeTemp.LNodesNeighbors[node].NodeEndT = time;
                    if (lGraphs[graphTime].LNodes[nodeTemp.NodeId].LNodesNeighbors.ContainsKey(node))
                    {
                        lGraphs[graphTime].LNodes[nodeTemp.NodeId].LNodesNeighbors.Remove(nodeTemp.NodeId);
                    }
                }

            }
        }

        //add edge
        public void AddEdge(string nodeA, string nodeB, double graphTime, double time)
        {
            if (!(lEdges.ContainsKey(nodeA + "-" + nodeB)) && !(lEdges.ContainsKey(nodeB + "-" + nodeA)))
            {
                //increment gain edge
                countGainEdge++;

                //add the neighbor to each node
                lNodes[nodeA].LNodesNeighbors.Add(nodeB, new Node { NodeId = nodeB, NodeStartT = time });
                lNodes[nodeB].LNodesNeighbors.Add(nodeA, new Node { NodeId = nodeA, NodeStartT = time });
                lGraphs[graphTime].LNodes[nodeA].LNodesNeighbors.Add(nodeB, new Node { NodeId = nodeB, NodeStartT = time });
                lGraphs[graphTime].LNodes[nodeB].LNodesNeighbors.Add(nodeA, new Node { NodeId = nodeA, NodeStartT = time });
                //add the edge to the node
                lNodes[nodeA].LEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
                lNodes[nodeB].LEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
                lGraphs[graphTime].LNodes[nodeA].LEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
                lGraphs[graphTime].LNodes[nodeB].LEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
                //set the start time of edge
                lEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
                lGraphs[graphTime].LEdges.Add(nodeA + "-" + nodeB, new Edge { EdgeId = nodeA + "-" + nodeB, NodeA = lNodes[nodeA], NodeB = lNodes[nodeB], EdgeStartT = time });
            }
        }

        //remove edge
        public void RemoveEdge(string nodeA, string nodeB, double graphTime, double time)
        {
            if (lEdges.ContainsKey(nodeA + "-" + nodeB))
            {
                //increment lost edge
                countLostEdge++;
                //set the end time of edge
                lEdges[nodeA + "-" + nodeB].EdgeEndT = time;
                lGraphs[graphTime].LEdges.Remove(nodeA + "-" + nodeB);
                //remove neighbor
                lNodes[nodeA].LNodesNeighbors[nodeB].NodeEndT = time;
                lNodes[nodeB].LNodesNeighbors[nodeA].NodeEndT = time;
                lGraphs[graphTime].LNodes[nodeA].LNodesNeighbors.Remove(nodeB);
                lGraphs[graphTime].LNodes[nodeB].LNodesNeighbors.Remove(nodeA);
                //remove edge to the node
                lNodes[nodeA].LEdges[nodeA + "-" + nodeB].EdgeEndT = time;
                lGraphs[graphTime].LNodes[nodeA].LEdges.Remove(nodeA + "-" + nodeB);
                lGraphs[graphTime].LNodes[nodeB].LEdges.Remove(nodeA + "-" + nodeB);
            }
        }
    }
}
