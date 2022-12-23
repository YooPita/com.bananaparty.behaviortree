﻿using NUnit.Framework;
using System.Linq;

namespace BananaParty.BehaviorTree.Tests
{
    public class SelectorNodeTests
    {
        [Test]
        public void ShouldExecuteAllChildrenOnce()
        {
            InvocationTestNode[] testNodes = new[]
            {
                new InvocationTestNode(BehaviorNodeStatus.Failure),
                new InvocationTestNode(BehaviorNodeStatus.Failure),
                new InvocationTestNode(BehaviorNodeStatus.Failure)
            };

            var selector = new SelectorNode(testNodes);
            selector.Execute();

            Assert.IsTrue(testNodes.All(testNode => testNode.ExecutionCount == 1));
        }
    }
}
