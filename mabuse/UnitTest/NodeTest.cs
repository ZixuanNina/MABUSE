﻿using NUnit.Framework;
using System.Collections.Generic;
using mabuse.datamode;
using System;

namespace mabuse.UnitTest
{
    [TestFixture]
    public class NodeTest
    {
        /// <summary>
        /// Tests the count degree expect.
        /// </summary>
        [Test]
        public void Test_CountDegreeExpect()
        {
            Dictionary<string, Node> TestNodeDic = new Dictionary<string, Node>();
            TestNodeDic.Add("a", new Node
            {
                NodeId = "a"
            });
            for(int i = 0; i < 10; i++)
            {
                TestNodeDic["a"].EdgeIdToEdgeObjectDict.Add(i.ToString(), new Edge
                {
                    EdgeId = i.ToString(),
                    EdgeStartTime = i,
                    EdgeEndTime = 2 * i
                });
            }
            int count = TestNodeDic["a"].CountDegree(0);
            int expect = 1;
            Assert.AreEqual(count, expect);
            count = TestNodeDic["a"].CountDegree(5);
            expect = 3;
            Assert.AreEqual(count, expect);
        }

        /// <summary>
        /// Tests the count degree negtive.
        /// </summary>
        [Test]
        public void Test_CountDegreeNegtive()
        {
            Dictionary<string, Node> TestNodeDic = new Dictionary<string, Node>
            {
                {
                    "a",
                    new Node
                    {
                        NodeId = "a"
                    }
                }
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => TestNodeDic["a"].CountDegree(-1));
        }
    }
}
