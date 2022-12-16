﻿using System.Security.Cryptography.X509Certificates;

namespace BananaParty.BehaviorTree
{
    public class SucceederNode : DecoratorNode
    {
        public SucceederNode(IBehaviorNode childNode) : base(childNode) { }

        protected override BehaviorNodeStatus OnFailure()
        {
            return base.OnSuccess();
        }
    }
}
