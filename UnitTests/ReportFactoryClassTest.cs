using mabuse.datamode;
using System.Collections.Generic;
using System;
using Xunit;

namespace mabuse.UnitTest
{
    /// <summary>
    /// Report factory class test.
    /// </summary>
    public class ReportFactoryClassTest
    {
        //Test empty graph
        //Empty graph
        [Fact]
        public void Test_EmptyGraph()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>();
            ReportFactory reportFactory;
            Assert.Throws<ArgumentException>(() => reportFactory = new ReportFactory(graph));
        }

        /// <summary>
        ///Test for GetMaxDegree function
        /// </summary>
        // Test when there is one edge
        [Fact]
        public void Test_GetMaxDegree()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });

            graph[365].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[365].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            ReportFactory reportFactory = new ReportFactory(graph);
            int maxDegree = reportFactory.GetMaxDegree();
            int expect = 1;
            Assert.Equal(maxDegree, expect);
        }

        //Test when there is no node and no edge
        [Fact]
        public void Test_GEtMaxDegZeroNodeAndEdge()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            int maxDeg = reportFactory.GetMaxDegree();
            Assert.Equal(maxDeg, int.MinValue);
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            ReportFactory reportFactory1 = new ReportFactory(graph);
            maxDeg = reportFactory1.GetMaxDegree();
            int expect = 0;
            Assert.Equal(maxDeg, expect);
        }
        //test if function could find the max degree with comparision
        [Fact]
        public void Tesr_GetMaxDegWithComparision()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge { EdgeId = "a-c" });

            graph[365].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[365].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });

            ReportFactory reportFactory = new ReportFactory(graph);
            int maxDeg = reportFactory.GetMaxDegree();
            int expect = 2;
            Assert.Equal(maxDeg, expect);
        }

        /// <summary>
        /// Tests the get interval of the distribution.
        /// </summary>
        //Max = 0
        [Fact]
        public void Test_GetIntervalOfTheDistributionZero()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            int[] range = reportFactory.GetIntervalOfTheDistribution(0);
            int[] expect = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.Equal(range, expect);
        }

        //max is negative
        [Fact]
        public void Test_GetIntervalOfTheDistributionNegative()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            
            Assert.Throws<ArgumentOutOfRangeException>(() => reportFactory.GetIntervalOfTheDistribution(-1));
        }

        //max is in 1-10
        [Fact]
        public void Test_GetIntervalOfTheDistributionRangeOneToTen()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            int[] range = reportFactory.GetIntervalOfTheDistribution(1);
            int[] expect = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.Equal(range, expect);
            range = reportFactory.GetIntervalOfTheDistribution(5);
            Assert.Equal(range, expect);
            range = reportFactory.GetIntervalOfTheDistribution(10);
            Assert.Equal(range, expect);
        }

        //max is greater than 10
        [Fact]
        public void Test_GetIntervalOfTheDistributionRangeGreaterThanTen()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            int[] range = reportFactory.GetIntervalOfTheDistribution(11);
            int[] expect = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };
            Assert.Equal(range, expect);
            range = reportFactory.GetIntervalOfTheDistribution(20);
            expect[0] = 2;
            expect[1] = 4;
            expect[2] = 6;
            expect[3] = 8;
            expect[4] = 10;
            expect[5] = 12;
            expect[6] = 14;
            expect[7] = 16;
            expect[8] = 18;
            expect[9] = 20;
            Assert.Equal(range, expect);
            range = reportFactory.GetIntervalOfTheDistribution(22);
            expect = new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 22 };
            Assert.Equal(range, expect);
        }

        /// <summary>
        /// Tests the count degree with interval.
        /// </summary>
        //Empty graph
        [Fact]
        public void Test_CountDegreeWithIntervalZeroDegree()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            Assert.Throws<ArgumentOutOfRangeException>(() => reportFactory.CountDegreeWithInterval(graph[0]));
            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            int[] count = reportFactory.CountDegreeWithInterval(graph[0]);
            int[] expect = new int[10];
            expect[0] = 1;
            Assert.Equal(count, expect);
        }

        //with degrees
        [Fact]
        public void Test_CountDegreeWithInterval()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };

            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge
            {
                EdgeId = "b-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["b"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge
            {
                EdgeId = "b-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["b"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge
            {
                EdgeId = "a-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge
            {
                EdgeId = "a-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("c-d", new Edge
            {
                EdgeId = "c-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["c"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[0].NodeIdToNodeObjectDict["d"].EdgeIdToEdgeObjectDict.Add("c-d", new Edge
            {
                EdgeId = "c-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["c"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[365].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[365].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });

            ReportFactory reportFactory = new ReportFactory(graph);
            int[] countDeg = reportFactory.CountDegreeWithInterval(graph[0]);
            int[] expect = new int[10];
            expect[0] = 1;
            expect[1] = 2;
            expect[2] = 1;
            Assert.Equal(countDeg, expect);
            countDeg = reportFactory.CountDegreeWithInterval(graph[365]);
            expect[0] = 2;
            expect[1] = 0;
            expect[2] = 0;
            Assert.Equal(countDeg, expect);
        }

        /// <summary>
        /// Tests the get max number of common neighbor.
        /// </summary>

        //Test with information in graph
        [Fact]
        public void Test_GetMaxNumberOfCommonNeighbor()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };

            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["d"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("b-c", new Edge
            {
                EdgeId = "b-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["b"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("a-c", new Edge
            {
                EdgeId = "a-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].EdgeIdToEdgeObjectDict.Add("c-d", new Edge
            {
                EdgeId = "c-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["c"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[365].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            ReportFactory reportFactory = new ReportFactory(graph);
            int maxCount = reportFactory.GetMaxNumberOfCommonNeighbor();
            int expect = 1;
            Assert.Equal(maxCount, expect);
        }

        /// <summary>
        /// Tests the count partner function.
        /// </summary>
        //Empty graph
        [Fact]
        public void Test_CountPartnerEmpty()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };
            ReportFactory reportFactory = new ReportFactory(graph);
            Assert.Throws<ArgumentOutOfRangeException>(() => reportFactory.CountDegreeWithInterval(graph[0]));

            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            int[] countDeg = reportFactory.CountPartner(graph[0]);
            int[] expect = new int[10];
            expect[0] = 1;
            Assert.Equal(countDeg, expect);
        }

        //Test the CountPartner Function
        [Fact]
        public void Test_CountPartner()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>
            {
                { 0, new Graph { GraphStartTime = 0, GraphEndTime = 0 } },
                { 365, new Graph {GraphStartTime = 0, GraphEndTime = 365} }
            };

            graph[0].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["b"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["d"].NodeIdOfNeighborsOfNodeObjectDict.Add("c", new Node { NodeId = "c" });
            graph[0].NodeIdToNodeObjectDict["c"].NodeIdOfNeighborsOfNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["d"].NodeIdOfNeighborsOfNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[0].NodeIdToNodeObjectDict["a"].NodeIdOfNeighborsOfNodeObjectDict.Add("d", new Node { NodeId = "d" });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge
            {
                EdgeId = "a-b",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["b"]
            });
            graph[0].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge
            {
                EdgeId = "b-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["b"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("b-c", new Edge
            {
                EdgeId = "b-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["b"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge
            {
                EdgeId = "a-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("a-c", new Edge
            {
                EdgeId = "a-c",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["c"]
            });
            graph[0].NodeIdToNodeObjectDict["c"].EdgeIdToEdgeObjectDict.Add("c-d", new Edge
            {
                EdgeId = "c-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["c"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[0].NodeIdToNodeObjectDict["d"].EdgeIdToEdgeObjectDict.Add("c-d", new Edge
            {
                EdgeId = "c-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["c"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[0].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-d", new Edge
            {
                EdgeId = "a-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[0].NodeIdToNodeObjectDict["d"].EdgeIdToEdgeObjectDict.Add("a-d", new Edge
            {
                EdgeId = "a-d",
                NodeA = graph[0].NodeIdToNodeObjectDict["a"],
                NodeB = graph[0].NodeIdToNodeObjectDict["d"]
            });
            graph[365].NodeIdToNodeObjectDict.Add("a", new Node { NodeId = "a" });
            graph[365].NodeIdToNodeObjectDict.Add("b", new Node { NodeId = "b" });
            graph[365].NodeIdToNodeObjectDict["a"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });
            graph[365].NodeIdToNodeObjectDict["b"].EdgeIdToEdgeObjectDict.Add("a-b", new Edge { EdgeId = "a-b" });

            ReportFactory reportFactory = new ReportFactory(graph);
            int[] countDeg = reportFactory.CountDegreeWithInterval(graph[0]);
            int[] expect = new int[10];
            expect[1] = 2;
            expect[2] = 2;
            Assert.Equal(expect, countDeg);
            countDeg = reportFactory.CountDegreeWithInterval(graph[365]);
            expect[2] = 0;
            expect[1] = 0;
            expect[0] = 2;
            Assert.Equal(expect, countDeg);
        }

    }
}
