/*
Author: Zixuan(Nina) Hao
Purpose:
    This class manage the file reading and parsing the information store in thr Dictionary list
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        //list of temporary lNodes and lEdge for the purpose of avoiding the pointer changing data
        public Dictionary<string, Node> lNodesTemp = new Dictionary<string, Node>();
        public Dictionary<string, Edge> lEdgesTemp = new Dictionary<string, Edge>();
        /*
         * main constructor
         */
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
            double time = 0;
            //read file line by line
            string[] lines = File.ReadAllLines(pathOfFile);

            lGraphs.Add(0, new Graph { StartTime = 0, EndTime = 0 });
            int mod = 0;
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
                    time = Convert.ToDouble(str[0].Replace(":", ""));

                    mod = (int)(time / timeInterVal);
                    string command = str[1] + " " + str[2];
                    if (!(time%timeInterVal).Equals(0)&&!(lGraphs.ContainsKey((mod+1)*365)))
                    {
                        double timeTmp = (mod+1)*365;
                        lGraphs.Add(timeTmp, new Graph { StartTime = timeTmp - 365, EndTime = timeTmp });
                        //save the counts
                        lGraphs[graphTime].CountGainEdge = countGainEdge;
                        lGraphs[graphTime].CountLostEdge = countLostEdge;
                        lGraphs[graphTime].CountGainNode = countGainNode;
                        lGraphs[graphTime].CountLostNode = countLostNode;
                        countGainNode = countLostNode = countGainEdge = countLostEdge = 0;
                        //generate the next graph and transit the exist nodes and edges to the current new graph
                        foreach (Node node in lNodesTemp.Values)
                        {
                            lGraphs[graphTime].LNodes.Add(node.NodeId, node);
                        }
                        foreach (Edge edge in lEdgesTemp.Values)
                        {
                            lGraphs[graphTime].LEdges.Add(edge.EdgeId, edge);
                        }
                        graphTime = timeTmp;
                    }
                    if (time.Equals(3))
                    {
                        Console.WriteLine("kkk");
                    }
                    //switch case
                    switch (command)
                    {
                        case "add node":
                            AddNode(str[3], time);
                            break;
                        case "remove node":
                            RemoveNode(str[3], time);
                            break;
                        case "add edge":
                            AddEdge(str[4], str[5], time);
                            break;
                        case "remove edge":
                            RemoveEdge(str[4], str[5], time);
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
            //storing the last information for the last interval/day of report
            lGraphs[graphTime].CountGainEdge = countGainEdge;
            lGraphs[graphTime].CountLostEdge = countLostEdge;
            lGraphs[graphTime].CountGainNode = countGainNode;
            lGraphs[graphTime].CountLostNode = countLostNode;
            //add edges to edge list
            foreach (Edge edge in lEdgesTemp.Values)
            {
                //update edges to edge list
                lEdges.Add(edge.EdgeId, new Edge
                {
                    EdgeId = edge.EdgeId,
                    NodeA = edge.NodeA,
                    NodeB = edge.NodeB,
                    EdgeStartT = edge.EdgeStartT,
                    EdgeEndT = int.MaxValue
                });
                //update edge in graph
                lGraphs[graphTime].LEdges.Add(edge.EdgeId, lEdges[edge.EdgeId]);
            }
            //add nodes to node list
            foreach (Node node in lNodesTemp.Values)
            {

                lNodes.Add(node.NodeId, new Node
                { 
                    NodeId = node.NodeId,
                    NodeStartT = node.NodeStartT,
                    NodeEndT = int.MaxValue
                });
                foreach (Edge edge in lEdges.Values)
                {
                    if (!lNodes[node.NodeId].LEdges.ContainsKey(edge.EdgeId) && (edge.NodeA.NodeId.Equals(node.NodeId) || edge.NodeB.NodeId.Equals(node.NodeId)))
                    {
                        lNodes[node.NodeId].LEdges.Add(edge.EdgeId, edge);
                        if (edge.NodeA.NodeId.Equals(node.NodeId)) 
                        {
                            lNodes[node.NodeId].LNodesNeighbors.Add(edge.NodeB.NodeId, new Node
                            { 
                                NodeId = edge.NodeB.NodeId
                            });
                        }
                        else
                        {
                            lNodes[node.NodeId].LNodesNeighbors.Add(edge.NodeA.NodeId, new Node
                            {
                                NodeId = edge.NodeA.NodeId
                            });
                        }
                    }
                }
                //update node in graph
                lGraphs[graphTime].LNodes.Add(node.NodeId, lNodes[node.NodeId]);
            }
        }

        /*
         * Get the graph and nodes list
         */        
        public Dictionary<double, Graph> GetGraph()
        {
            return lGraphs;
        }
        public Dictionary<string, Node> GetNodeL()
        {
            return lNodes;
        }

        /*
         * functions to achive commands from the graph
         */       
        /*
         * Add node
         */        
        public void AddNode(string node, double time)
        {
            //increment the gained node
            countGainNode++;
            //add node set the start time
            lNodesTemp.Add(node, new Node 
            { 
                NodeId = node, 
                NodeStartT = time 
            });
        }

        /*
         * remove node and nodes' neighbors in the node
         */       
        public void RemoveNode(string node, double time)
        {
            //increment lose node
            countLostNode++;
            //set the condition of the node
            lNodes.Add(node, new Node 
            { 
                NodeId = node, 
                NodeStartT = lNodesTemp[node].NodeStartT, 
                NodeEndT = time 
            });
            //remove all the connected edge, set the end time the same as the node
            foreach (Edge edge in lNodesTemp[node].LEdges.Values) //need to think about the edge infor origin
            {
                //update edge to edge list
                lEdges.Add(edge.EdgeId, new Edge
                {
                    EdgeId = edge.EdgeId,
                    NodeA = edge.NodeA,
                    NodeB = edge.NodeB,
                    EdgeStartT = lEdgesTemp[edge.EdgeId].EdgeStartT,
                    EdgeEndT = time
                });
                //updata edge of the node' edge list
                lNodes[node].LEdges.Add(edge.EdgeId, new Edge
                {
                    EdgeId = edge.EdgeId,
                    NodeA = edge.NodeA,
                    NodeB = edge.NodeB,
                    EdgeStartT = lEdgesTemp[edge.EdgeId].EdgeStartT,
                    EdgeEndT = time
                });
                //updata the temporary edge information of the list
                if (lEdgesTemp.ContainsKey(edge.EdgeId))
                {
                    lEdgesTemp.Remove(edge.EdgeId);
                }
                countLostEdge++;
            }
            //remove all neighbor status of neighbor nodes

            foreach (Node nodeTemp in lNodesTemp[node].LNodesNeighbors.Values)
            {
                //update the node neighbor to the node list
                lNodes[node].LNodesNeighbors.Add(nodeTemp.NodeId, new Node
                {
                    NodeId = nodeTemp.NodeId,
                    NodeStartT = nodeTemp.NodeStartT,
                    NodeEndT = time
                });
                //remove the neighbor node' s neighbor node
                lNodesTemp[nodeTemp.NodeId].LNodesNeighbors.Remove(node);
            }
            lNodesTemp[node].LEdges.Clear();
            lNodesTemp[node].LNodesNeighbors.Clear();
            lNodesTemp.Remove(node);
        }

        /*
         * add edge
        */
        public void AddEdge(string nodeA, string nodeB, double time)
        {
            if (!(lEdgesTemp.ContainsKey(nodeA + "-" + nodeB)) && !(lEdgesTemp.ContainsKey(nodeB + "-" + nodeA)))
            {
                //increment gain edge
                countGainEdge++;
                //add the neighbor to each node
                lNodesTemp[nodeA].LNodesNeighbors.Add(nodeB, new Node 
                { 
                    NodeId = nodeB, 
                    NodeStartT = time 
                });
                lNodesTemp[nodeB].LNodesNeighbors.Add(nodeA, new Node 
                { 
                    NodeId = nodeA, 
                    NodeStartT = time 
                });
                //add the edge to the node
                lNodesTemp[nodeA].LEdges.Add(nodeA + "-" + nodeB, new Edge 
                { 
                    EdgeId = nodeA + "-" + nodeB, 
                    NodeA = lNodesTemp[nodeA], 
                    NodeB = lNodesTemp[nodeB], 
                    EdgeStartT = time 
                });
                lNodesTemp[nodeB].LEdges.Add(nodeA + "-" + nodeB, new Edge 
                { 
                    EdgeId = nodeA + "-" + nodeB, 
                    NodeA = lNodesTemp[nodeA], 
                    NodeB = lNodesTemp[nodeB], 
                    EdgeStartT = time 
                });
                //set the start time of edge
                lEdgesTemp.Add(nodeA + "-" + nodeB, new Edge 
                { 
                    EdgeId = nodeA + "-" + nodeB, 
                    NodeA = lNodesTemp[nodeA], 
                    NodeB = lNodesTemp[nodeB], 
                    EdgeStartT = time 
                });
            }
        }

        /*
         * remove edge
        */
        public void RemoveEdge(string nodeA, string nodeB, double time)
        {
            if (lEdgesTemp.ContainsKey(nodeA + "-" + nodeB) && !lEdges.ContainsKey(nodeA + "-" + nodeB))
            {
                //increment lost edge
                countLostEdge++;
                //set the condition of the edge
                lEdges.Add(nodeA + "-" + nodeB, new Edge 
                { 
                    EdgeId = nodeA + "-" + nodeB, 
                    NodeA = lNodesTemp[nodeA], 
                    NodeB = lNodesTemp[nodeB], 
                    EdgeStartT = lEdgesTemp[nodeA + "-" + nodeB].EdgeStartT, 
                    EdgeEndT = time 
                });
                //remove neighbor
                lNodesTemp[nodeA].LNodesNeighbors.Remove(nodeB);
                lNodesTemp[nodeB].LNodesNeighbors.Remove(nodeA);
                //remove edge to the node
                lNodesTemp[nodeA].LEdges.Remove(nodeA + "-" + nodeB);
                lNodesTemp[nodeB].LNodesNeighbors.Remove(nodeA + "-" + nodeB);
                //edge remove
                lEdgesTemp.Remove(nodeA + "-" + nodeB);
            }
        }
    }
}
