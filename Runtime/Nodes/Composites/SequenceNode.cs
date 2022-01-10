﻿namespace BananaParty.BehaviorTree
{
    public class SequenceNode : SequentialCompositeNode
    {
        public SequenceNode(IBehaviorNode[] childNodes, bool alwaysReevaluate = false) : base(childNodes, alwaysReevaluate) { }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            int runningChildIndex = RunningChildIndex;

            for (int childIterator = AlwaysReevaluate || runningChildIndex == -1 ? 0 : runningChildIndex;
                childIterator < ChildNodes.Length; childIterator += 1)
            {
                BehaviorNodeStatus childNodeStatus = ChildNodes[childIterator].Execute(time);

                if (childNodeStatus != BehaviorNodeStatus.Success)
                {
                    // Interrupt nodes in front on failed reevaluation.
                    for (int childToResetIterator = childIterator + 1;
                        childToResetIterator <= runningChildIndex; childToResetIterator += 1)
                        ChildNodes[childToResetIterator].Reset();

                    return childNodeStatus;
                }
            }

            return BehaviorNodeStatus.Success;
        }
    }
}
