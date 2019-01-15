/*
Author: Zixuan(Nina) Hao
Purpose:
    Edge format the edge object to store the information
 */

using System;
using System.Collections.Generic;

namespace mabuse.datamode
{
    public class Edge
    {
        public string EdgeId { get; set; }
        public string NodeA { get; set; }
        public string NodeB { get; set; }
        public double EdgeStartT { get; set; }
        public double EdgeEndT { get; set; }
    }
}
