namespace mabuse.datamode
{
    /// <summary>
    ///    Edge format the edge object to store the information
    /// </summary>
    ///<author>
    /// Zixuan (Nina) Hao
    /// </author>
    public class Edge
    {
        public string EdgeId { get; set; }
        public Node NodeA { get; set; }
        public Node NodeB { get; set; }
        public double EdgeStartTime { get; set; }
        public double EdgeEndTime { get; set; }
    }
}
