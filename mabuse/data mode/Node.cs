using System.Collections.Generic;

namespace mabuse.datamode
{
    /// <summary>
    /// Node is a type of the dicttionary list to format the value and store the information related to the node object
    /// </summary>
    ///<author>
    /// Zixuan (Nina) Hao
    /// </author>
    public class Node
    {
        public string NodeId { get; set; }
        public double NodeStartTime { get; set; }
        public double NodeEndTime { get; set; }
        public Dictionary<string, Edge> EdgeIdToEdgeObjectDict = new Dictionary<string, Edge>();
        public Dictionary<string, Node> NodeIdOfNeighborsOfNodeObjectDict = new Dictionary<string, Node>();

    }
}
