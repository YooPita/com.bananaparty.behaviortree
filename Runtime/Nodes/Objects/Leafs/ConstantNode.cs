﻿namespace Retrover.BehaviorTree
{
    public class ConstantNode : BehaviorNode
    {
        protected override string Name => $"Constant Node {_statusToReturn}";

        protected readonly BehaviorNodeStatus _statusToReturn;

        /// <summary>
        /// Always returns the specified <paramref name="statusToReturn"/>.
        /// </summary>
        public ConstantNode(BehaviorNodeStatus statusToReturn)
        {
            _statusToReturn = statusToReturn;
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            return _statusToReturn > 0 ? _statusToReturn : BehaviorNodeStatus.Success;
        }
    }
}
