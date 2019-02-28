using System;
using System.Collections.Generic;
using System.IO;
using mabuse.datamode;


namespace mabuse
{
    /// <summary>
    /// This class manage the file reading and parsing the information store in thr Dictionary list
    /// </summary>
    ///<author>
    /// Zixuan (Nina) Hao
    /// </author>
    public class Parser
    {
        private Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();
        private Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();
        private Dictionary<double, Graph> GraphTimeToGraphObjectDict = new Dictionary<double, Graph>();
        private int countGainNode;
        private int countLostNode;
        private int countGainEdge;
        private int countLostEdge;
        private double GraphTime;
        private Dictionary<string, Node> CurrentSetOfNodesInSimulation = new Dictionary<string, Node>();
        private Dictionary<string, Edge> CurrentSetOfEdgesInSimulation = new Dictionary<string, Edge>();

        /// <summary>
        /// Get a dictionary map from the double time the graph represents to the graph object.
        /// </summary>
        public Dictionary<double, Graph> GetGraphTimeToGraphDictionary()
        {
            return GraphTimeToGraphObjectDict;
        }

        /// <summary>
        /// Get a dictionary map from the string node id to the node object.
        /// </summary>
        public Dictionary<string, Node> GetNodeIdToNodeDictionary()
        {
            return NodeIdToNodeObjectDict;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathOfFile">Path to the trace file to be parsed.</param>
        public Parser(string pathOfFile)
        {
            FileParsing(pathOfFile);
        }

        /// <summary>
        /// File parsing function
        /// </summary>
        /// <param name="filePath"> Path to the trace file.</param>
        private void FileParsing(string filePath)
        {
            int CountLine = 0;
            double timeInterVal = 365;
            double TimeAtLine = 0;
            GraphTime = 0;
            string[] lines = File.ReadAllLines(filePath);
            Graph CurrentGraph = new Graph { GraphStartTime = 0, GraphEndTime = 0 };
            GraphTimeToGraphObjectDict.Add(0, CurrentGraph);
            int CurrentYear = 0;
            foreach (string line in lines)
            {
                CountLine++;
                string[] LineTokens = line.Split(' ');
                //This part could be future redefined since this time did not consider the layer issue.
                //only generate the simple nodes and edges graph
                //need to check and save the nodes with its id.
                //process data with the respond command And check missing information
                if (LineTokens.Length >= 3)
                {
                    bool SuccessfullyConvertTimeToDouble = Double.TryParse(LineTokens[0].Replace(":", ""), out TimeAtLine);
                    if (!SuccessfullyConvertTimeToDouble) throw new Exception($"Failed to parse time as double on line {CountLine}");

                    CurrentYear = (int)(TimeAtLine / timeInterVal)+ 1;
                    string command = LineTokens[1] + " " + LineTokens[2];
                    if (!(TimeAtLine % timeInterVal).Equals(0) && !(GraphTimeToGraphObjectDict.ContainsKey(CurrentYear * 365)))
                    {
                        double timeTmp = CurrentYear * 365;
                        Graph NextGraph = new Graph { GraphStartTime = timeTmp - 365, GraphEndTime = timeTmp };
                        GraphTimeToGraphObjectDict.Add(timeTmp, NextGraph);
                        StoreNodesAndEdgesFromPreviousInterval(CurrentGraph);
                        countGainNode = countLostNode = countGainEdge = countLostEdge = 0;
                        GraphTime = timeTmp;
                        CurrentGraph = NextGraph;
                    }
                    switch (command)
                    {
                        case "add node":
                            AddNode(LineTokens[3], TimeAtLine);
                            break;
                        case "remove node":
                            RemoveNode(LineTokens[3], TimeAtLine);
                            break;
                        case "add edge":
                            AddEdge(LineTokens[4], LineTokens[5], TimeAtLine);
                            break;
                        case "remove edge":
                            RemoveEdge(LineTokens[4], LineTokens[5], TimeAtLine);
                            break;
                        default:
                            Console.WriteLine("new command detected: " + command);
                            break;

                    }
                }
                else
                {
                    if (lines.Length != CountLine && line.Length.Equals(0))
                    {
                        throw new Exception($"Non terminating empty line detected in file at : {CountLine}");
                    }
                    if (!line.Length.Equals(0))
                    {
                        throw new Exception("Missing node information at line: " + CountLine);
                    }
                    else
                    {
                        Console.WriteLine("Parse file complete.");
                    }
                }
            }
            //storing the last information for the last interval/day of report
            StoreNodesAndEdgesFromPreviousInterval(CurrentGraph);
            //add edges to edge list
            AddEdgeToGivenEdgeDic();
            //add nodes to node list
            AddNodeTONodeDic();
            //add edges to the nodes
            AddEdgeAndNeighborToTheNide();
        }

        /// <summary>
        /// Create and store a new graph object.
        /// </summary>
        /// <param name="ThisGraph"></param>
        private void StoreNodesAndEdgesFromPreviousInterval(Graph ThisGraph)
        {
            ThisGraph.CountGainEdge = countGainEdge;
            ThisGraph.CountLostEdge = countLostEdge;
            ThisGraph.CountGainNode = countGainNode;
            ThisGraph.CountLostNode = countLostNode;
            //generate the next graph and transit the exist nodes and edges to the current new graph
            foreach (Node node in CurrentSetOfNodesInSimulation.Values)
            {
                ThisGraph.NodeIdToNodeObjectDict.Add(node.NodeId, new Node
                {
                    NodeId = node.NodeId,
                    NodeStartTime = node.NodeStartTime,
                    NodeEndTime = node.NodeEndTime
                });
                AddRelatedEdgesToGivenNode(node);
            }
            foreach (Edge edge in CurrentSetOfEdgesInSimulation.Values)
            {
                ThisGraph.EdgeIdToEdgeObjectDict.Add(edge.EdgeId, edge);
            }
        }
        /// <summary>
        /// Adds the edge to graph node.
        /// </summary>
        /// <param name="node">Node.</param>
        public void AddRelatedEdgesToGivenNode(Node node)
        {
            foreach (Edge edge in CurrentSetOfNodesInSimulation[node.NodeId].EdgeIdToEdgeObjectDict.Values)
            {
                if (!GraphTimeToGraphObjectDict[GraphTime].NodeIdToNodeObjectDict[node.NodeId].EdgeIdToEdgeObjectDict.ContainsKey(edge.EdgeId))
                {
                    GraphTimeToGraphObjectDict[GraphTime].NodeIdToNodeObjectDict[node.NodeId].EdgeIdToEdgeObjectDict.Add(edge.EdgeId, new Edge
                    {
                        EdgeId = edge.EdgeId,
                        EdgeStartTime = edge.EdgeStartTime,
                        EdgeEndTime = edge.EdgeEndTime,
                        NodeA = edge.NodeA,
                        NodeB = edge.NodeB
                    });
                    if (edge.NodeA.NodeId.Equals(node.NodeId))
                    {
                        GraphTimeToGraphObjectDict[GraphTime].NodeIdToNodeObjectDict[node.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Add(edge.NodeB.NodeId, new Node
                        {
                            NodeId = edge.NodeB.NodeId,
                            NodeStartTime = edge.EdgeStartTime,
                            NodeEndTime = edge.EdgeEndTime
                        });
                    }
                    else
                    {
                        GraphTimeToGraphObjectDict[GraphTime].NodeIdToNodeObjectDict[node.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Add(edge.NodeA.NodeId, new Node
                        {
                            NodeId = edge.NodeA.NodeId,
                            NodeStartTime = edge.EdgeStartTime,
                            NodeEndTime = edge.EdgeEndTime
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="time">Time.</param>
        private void AddNode(string node, double time)
        {
            //increment the gained node
            countGainNode++;
            //add node set the start time
            CurrentSetOfNodesInSimulation.Add(node, new Node 
            { 
                NodeId = node, 
                NodeStartTime = time 
            });
        }

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="time">Time.</param>/
        private void RemoveNode(string node, double time)
        {
            //increment lose node
            countLostNode++;
            //set the condition of the node
            NodeIdToNodeObjectDict.Add(node, new Node 
            { 
                NodeId = node, 
                NodeStartTime = CurrentSetOfNodesInSimulation[node].NodeStartTime, 
                NodeEndTime = time 
            });
            //remove all the connected edge, set the end time the same as the node
            foreach (Edge edge in CurrentSetOfNodesInSimulation[node].EdgeIdToEdgeObjectDict.Values)
            {
                //update edge to edge list
                if (!EdgeIdToEdgeObjectDict.ContainsKey(edge.EdgeId))
                {
                    EdgeIdToEdgeObjectDict.Add(edge.EdgeId, new Edge
                    {
                        EdgeId = edge.EdgeId,
                        NodeA = edge.NodeA,
                        NodeB = edge.NodeB,
                        EdgeStartTime = CurrentSetOfEdgesInSimulation[edge.EdgeId].EdgeStartTime,
                        EdgeEndTime = time
                    });
                }
                //updata the temporary edge information of the list
                if (CurrentSetOfEdgesInSimulation.ContainsKey(edge.EdgeId))
                {
                    CurrentSetOfEdgesInSimulation.Remove(edge.EdgeId);
                }
                if (edge.NodeA.NodeId.Equals(node))
                {
                    CurrentSetOfNodesInSimulation[edge.NodeB.NodeId].EdgeIdToEdgeObjectDict.Remove(edge.EdgeId);
                }
                else
                {
                    CurrentSetOfNodesInSimulation[edge.NodeA.NodeId].EdgeIdToEdgeObjectDict.Remove(edge.EdgeId);
                }
                countLostEdge++;
            }
            //remove all neighbor status of neighbor nodes

            foreach (Node nodeTemp in CurrentSetOfNodesInSimulation[node].NodeIdOfNeighborsOfNodeObjectDict.Values)
            {
                //remove the neighbor node' s neighbor node
                CurrentSetOfNodesInSimulation[nodeTemp.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Remove(node);
            }
            CurrentSetOfNodesInSimulation[node].EdgeIdToEdgeObjectDict.Clear();
            CurrentSetOfNodesInSimulation[node].NodeIdOfNeighborsOfNodeObjectDict.Clear();
            CurrentSetOfNodesInSimulation.Remove(node);
        }

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="nodeA">Node a.</param>
        /// <param name="nodeB">Node b.</param>
        /// <param name="time">Time.</param>
        private void AddEdge(string nodeA, string nodeB, double time)
        {
            if (!(CurrentSetOfEdgesInSimulation.ContainsKey(nodeA + "-" + nodeB)) && !(CurrentSetOfEdgesInSimulation.ContainsKey(nodeB + "-" + nodeA)))
            {
                //increment gain edge
                countGainEdge++;
                //add the neighbor to each node
                try
                {
                    CurrentSetOfNodesInSimulation[nodeA].NodeIdOfNeighborsOfNodeObjectDict.Add(nodeB, new Node
                    {
                        NodeId = nodeB,
                        NodeStartTime = time
                    });
                    CurrentSetOfNodesInSimulation[nodeB].NodeIdOfNeighborsOfNodeObjectDict.Add(nodeA, new Node
                    {
                        NodeId = nodeA,
                        NodeStartTime = time
                    });
                    //add the edge to the node
                    CurrentSetOfNodesInSimulation[nodeA].EdgeIdToEdgeObjectDict.Add(nodeA + "-" + nodeB, new Edge
                    {
                        EdgeId = nodeA + "-" + nodeB,
                        NodeA = CurrentSetOfNodesInSimulation[nodeA],
                        NodeB = CurrentSetOfNodesInSimulation[nodeB],
                        EdgeStartTime = time
                    });
                    CurrentSetOfNodesInSimulation[nodeB].EdgeIdToEdgeObjectDict.Add(nodeA + "-" + nodeB, new Edge
                    {
                        EdgeId = nodeA + "-" + nodeB,
                        NodeA = CurrentSetOfNodesInSimulation[nodeA],
                        NodeB = CurrentSetOfNodesInSimulation[nodeB],
                        EdgeStartTime = time
                    });

                    //set the start time of edge
                    CurrentSetOfEdgesInSimulation.Add(nodeA + "-" + nodeB, new Edge
                    {
                        EdgeId = nodeA + "-" + nodeB,
                        NodeA = CurrentSetOfNodesInSimulation[nodeA],
                        NodeB = CurrentSetOfNodesInSimulation[nodeB],
                        EdgeStartTime = time
                    });
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine("Exception caught: {0}", e);
                    throw;
                }

            }
        }

        /// <summary>
        /// Removes the edge.
        /// </summary>
        /// <param name="nodeA">Node a.</param>
        /// <param name="nodeB">Node b.</param>
        /// <param name="time">Time.</param>
        private void RemoveEdge(string nodeA, string nodeB, double time)
        {
            if (CurrentSetOfEdgesInSimulation.ContainsKey(nodeA + "-" + nodeB) && !EdgeIdToEdgeObjectDict.ContainsKey(nodeA + "-" + nodeB))
            {
                //increment lost edge
                countLostEdge++;
                //set the condition of the edge
                EdgeIdToEdgeObjectDict.Add(nodeA + "-" + nodeB, new Edge 
                { 
                    EdgeId = nodeA + "-" + nodeB, 
                    NodeA = CurrentSetOfNodesInSimulation[nodeA], 
                    NodeB = CurrentSetOfNodesInSimulation[nodeB], 
                    EdgeStartTime = CurrentSetOfEdgesInSimulation[nodeA + "-" + nodeB].EdgeStartTime, 
                    EdgeEndTime = time 
                });
                //remove neighbor


                CurrentSetOfNodesInSimulation[nodeA].NodeIdOfNeighborsOfNodeObjectDict.Remove(nodeB);
                CurrentSetOfNodesInSimulation[nodeB].NodeIdOfNeighborsOfNodeObjectDict.Remove(nodeA);
                //remove edge to the node
                CurrentSetOfNodesInSimulation[nodeA].EdgeIdToEdgeObjectDict.Remove(nodeA + "-" + nodeB);
                CurrentSetOfNodesInSimulation[nodeB].EdgeIdToEdgeObjectDict.Remove(nodeA + "-" + nodeB);
                //edge remove
                CurrentSetOfEdgesInSimulation.Remove(nodeA + "-" + nodeB);
            }
        }

        /// <summary>
        /// Adds the edge to Edge dictionary with the edge Id.
        /// </summary>
        private void AddEdgeToGivenEdgeDic()
        {
            foreach (Edge edge in CurrentSetOfEdgesInSimulation.Values)
            {
                //update edges to edge list
                EdgeIdToEdgeObjectDict.Add(edge.EdgeId, new Edge
                {
                    EdgeId = edge.EdgeId,
                    NodeA = edge.NodeA,
                    NodeB = edge.NodeB,
                    EdgeStartTime = edge.EdgeStartTime,
                    EdgeEndTime = int.MaxValue
                });
            }
        }

        /// <summary>
        /// Adds the node to Node dictionary with the node Id.
        /// </summary>
         private void AddNodeTONodeDic()
        {
            foreach (Node node in CurrentSetOfNodesInSimulation.Values)
            {

                NodeIdToNodeObjectDict.Add(node.NodeId, new Node
                {
                    NodeId = node.NodeId,
                    NodeStartTime = node.NodeStartTime,
                    NodeEndTime = int.MaxValue
                });
            }
        }

        /// <summary>
        /// Adds the related edge and neighbor to the node.
        /// </summary>
         private void AddEdgeAndNeighborToTheNide()
        {

            foreach (Node node in NodeIdToNodeObjectDict.Values)
            {
                foreach (Edge edge in EdgeIdToEdgeObjectDict.Values)
                {
                    if (!NodeIdToNodeObjectDict[node.NodeId].EdgeIdToEdgeObjectDict.ContainsKey(edge.EdgeId))
                    {
                        if (edge.NodeA.NodeId.Equals(node.NodeId))
                        {
                            NodeIdToNodeObjectDict[node.NodeId].EdgeIdToEdgeObjectDict.Add(edge.EdgeId, edge);
                            NodeIdToNodeObjectDict[node.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Add(edge.NodeB.NodeId, new Node
                            {
                                NodeId = edge.NodeB.NodeId,
                                NodeStartTime = edge.EdgeStartTime,
                                NodeEndTime = edge.EdgeEndTime
                            });
                        }
                        else if (edge.NodeB.NodeId.Equals(node.NodeId))
                        {
                            NodeIdToNodeObjectDict[node.NodeId].EdgeIdToEdgeObjectDict.Add(edge.EdgeId, edge);
                            NodeIdToNodeObjectDict[node.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Add(edge.NodeA.NodeId, new Node
                            {
                                NodeId = edge.NodeA.NodeId,
                                NodeStartTime = edge.EdgeStartTime,
                                NodeEndTime = edge.EdgeEndTime
                            });
                        }
                    }
                }
            }
        }

    }
}
