using mabuse.datamode;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mabuse.UnitTest
{
    [TestFixture()]
    public class ParserClassTest
    {
        /// <summary>
        /// Tests the get graph time to graph dictionary.
        /// </summary>
        // Test Empty graph
        [Test()]
        public void Test_GetGraphTimeToGraphDictionaryEmpty()
        {
            Dictionary<double, Graph> graph = new Dictionary<double, Graph>();
            Parser parser = new Parser("");
            Assert.AreEqual(parser.GetGraphTimeToGraphDictionary(), graph);
        }

        /// <summary>
        /// Tests the get node identifier to node dictionary empty.
        /// </summary>
        [Test()]
        public void Test_GetNodeIdToNodeDictionaryEmpty()
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            Parser parser = new Parser("");
            Assert.AreEqual(parser.GetNodeIdToNodeDictionary(), nodes);
        }

        /// <summary>
        /// Tests the file parsing function.
        /// </summary>
        [Test]
        public void Test_FileParsing()
        {

        }

        /// <summary>
        /// Tests the store nodes and edges from previous interval.
        /// </summary>
        [Test]
        public void Test_StoreNodesAndEdgesFromPreviousInterval()
        {

        }

        /// <summary>
        /// Tests the add related edges to given node.
        /// </summary>
        [Test]
        public void Test_AddRelatedEdgesToGivenNode()
        {

        }

        /// <summary>
        /// Tests the add node.
        /// </summary>
        [Test]
        public void Test_AddNode()
        {

        }

        /// <summary>
        /// Tests the remove node.
        /// </summary>
        [Test]
        public void Test_RemoveNode()
        {

        }

        /// <summary>
        /// Tests the add edge.
        /// </summary>
        [Test]
        public void Test_AddEdge()
        {

        }

        /// <summary>
        /// Tests the remove edge.
        /// </summary>
        [Test]
        public void Test_RemoveEdge()
        {

        }

        /// <summary>
        /// Tests the add edge to given edge dictionary.
        /// </summary>
        [Test]
        public void Test_AddEdgeToGivenEdgeDic()
        {

        }

        /// <summary>
        /// Tests the add node TO Node dic.
        /// </summary>
        [Test]
        public void Test_AddNodeToNodeDic()
        {

        }

        /// <summary>
        /// Tests the add edge and neighbor to the node.
        /// </summary>
        [Test]
        public void Test_AddEdgeAndNeighborToTheNode()
        {

        }
    }
}
