﻿using NUnit.Framework;
using System.Linq;

namespace BananaParty.BehaviorTree.Tests
{
    public class RollbackSelectorNodeTests
    {
        [Test]
        public void ShouldExecuteAllChildrenOnce()
        {
            InvocationRollbackTestNode[] testNodes = new[]
            {
                new InvocationRollbackTestNode(BehaviorNodeStatus.Failure),
                new InvocationRollbackTestNode(BehaviorNodeStatus.Failure),
                new InvocationRollbackTestNode(BehaviorNodeStatus.Failure)
            };

            var resultStatus = new RollbackSelectorNode(testNodes).Execute();

            Assert.IsTrue(resultStatus == BehaviorNodeStatus.Failure);
            Assert.IsTrue(testNodes.All(testNode => testNode.ExecutionCount == 1));
        }

        [Test]
        public void ShouldStopAfterSuccess()
        {
            InvocationRollbackTestNode[] testNodes = new[]
            {
                new InvocationRollbackTestNode(BehaviorNodeStatus.Success),
                new InvocationRollbackTestNode(BehaviorNodeStatus.Failure)
            };

            var resultStatus = new RollbackSelectorNode(testNodes).Execute();

            Assert.IsTrue(resultStatus == BehaviorNodeStatus.Success);
            Assert.IsTrue(testNodes[0].ExecutionCount == 1 && testNodes[1].ExecutionCount == 0);
        }

        [Test]
        public void ShouldStopAfterRunning()
        {
            InvocationRollbackTestNode[] testNodes = new[]
            {
                new InvocationRollbackTestNode(BehaviorNodeStatus.Running),
                new InvocationRollbackTestNode(BehaviorNodeStatus.Success)
            };

            var resultStatus = new RollbackSelectorNode(testNodes).Execute();

            Assert.IsTrue(resultStatus == BehaviorNodeStatus.Running);
            Assert.IsTrue(testNodes[0].ExecutionCount == 1 && testNodes[1].ExecutionCount == 0);
        }

        [Test]
        public void ShouldRollback()
        {
            InvocationRollbackTestNode[] testNodes = new[]
            {
                new InvocationRollbackTestNode(BehaviorNodeStatus.Success)
            };
            var node = new RollbackSelectorNode(testNodes);
            var clone = node.Clone();
            testNodes[0].ResultStatus = BehaviorNodeStatus.Running;
            var resultStatus = node.Execute();
            var cloneResult = clone.Execute();
            Assert.IsTrue(resultStatus == BehaviorNodeStatus.Running);
            Assert.IsTrue(cloneResult == BehaviorNodeStatus.Success);
            Assert.IsTrue(testNodes[0].ExecutionCount == 1);
        }
    }
}