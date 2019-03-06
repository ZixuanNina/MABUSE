using System.Collections.Generic;
using CuttingEdge.Conditions;

namespace mabuse.datamode
{
    /// <summary>
    ///Graph collect the Node list and Edge list to generat the useful result for the graph based on the time interval.
    /// </summary>
    ///<author>
    /// Zixuan (Nina) Hao
    /// </author>
    public class Graph
    {
        public int CountGainNode { get; set; }
        public int CountLostNode { get; set; }
        public int CountGainEdge { get; set; }
        public int CountLostEdge { get; set; }
        public double GraphStartTime { get; set; }
        public double GraphEndTime { get; set; }
        public Dictionary<string, Node> NodeIdToNodeObjectDict = new Dictionary<string, Node>();
        public Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();

        /// <summary>
        /// Gets the maximum degree.
        /// </summary>
        /// <returns>The max deg.</returns>/
        public int GetMaxDegreeOfGraph()
        {
            int degree = 0;
            int maxDegree = int.MinValue;
            foreach (Node node in NodeIdToNodeObjectDict.Values)
            {
                degree = node.EdgeIdToEdgeObjectDict.Count;
                Condition.Ensures(degree, "degree")
                    .IsNotLessThan(0);
                if(degree > maxDegree)
                {
                    maxDegree = degree;
                }
            }
            return maxDegree;
        }
        /// <summary>
        /// get the number of neighbors of two nodes in a graph.
        /// </summary>
        /// <returns>The tri number.</returns>
        /// <param name="nodeA">Node a.</param>
        /// <param name="nodeB">Node b.</param>
        public int CountNumberOfPatnerwiseNeighbors(Node nodeA, Node nodeB)
        {
            //input parameter condition check
            Condition.Requires(nodeA, "nodeA")
                .IsOfType(nodeB.GetType())
                .IsNotNull();
            Condition.Requires(nodeB, "nodeB")
                .IsOfType(nodeA.GetType())
                .IsNotNull();

            int count = 0;
            foreach (Node node in NodeIdToNodeObjectDict[nodeA.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Values)
            {
                if (!nodeB.NodeId.Equals(node.NodeId) 
                    && NodeIdToNodeObjectDict[nodeB.NodeId].NodeIdOfNeighborsOfNodeObjectDict.ContainsKey(node.NodeId))
                {
                    count++;
                }
            }

            Condition.Ensures(count, "count of parterwise neighbors of the edge")
                .IsGreaterOrEqual(0)
                .IsInRange(0, NodeIdToNodeObjectDict[nodeA.NodeId].NodeIdOfNeighborsOfNodeObjectDict.Count);

            return count;
        }
        /// <summary>
        /// Gets the graph max number of the partenerwise 
        /// </summary>
        /// <returns>The graph max tri.</returns>
        public int GetGraphMaxNumberOfPartnerwiseNeighbors()
        {
            int count = 0;
            int maxCount = int.MinValue;
            foreach (Edge edge in EdgeIdToEdgeObjectDict.Values)
            {
                count = CountNumberOfPatnerwiseNeighbors(edge.NodeA, edge.NodeB);

                Condition.Ensures(count, "count of parterwise neighbors of the edge")
                    .IsGreaterOrEqual(0)
                    .IsInRange(0, edge.NodeA.NodeIdOfNeighborsOfNodeObjectDict.Count);

                if (count > maxCount)
                {
                    maxCount = count;
                }
            }

            return maxCount;
        }
    }

}