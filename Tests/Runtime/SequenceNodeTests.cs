using NUnit.Framework;
using System.Linq;

namespace BananaParty.BehaviorTree.Tests
{
    public class SequenceNodeTests
    {
        [Test]
        public void ShouldExecuteAllChildrenOnce()
        {
            InvocationTestNode[] testNodes = new[]
            {
                new InvocationTestNode(BehaviorNodeStatus.Success),
                new InvocationTestNode(BehaviorNodeStatus.Success),
                new InvocationTestNode(BehaviorNodeStatus.Success)
            };

            var sequence = new SequenceNode(testNodes);
            sequence.Execute();

            Assert.IsTrue(testNodes.All(testNode => testNode.ExecutionCount == 1));
        }
    }
}
