﻿namespace Retrover.BehaviorTree
{
    public abstract class BehaviorTree : IBehaviorTree
    {
        private readonly IBehaviorNode _node;
        private IBehaviorTreeVisualizer _visualizer;

        public BehaviorTree(IBehaviorNode node, IBehaviorTreeVisualizer visualizer)
        {
            _node = node;
            _visualizer = visualizer;
        }

        public void Visualize()
        {
            _visualizer?.Visualize(_node.GetVisualizationData());
        }

        public bool Execute()
        {
            return PublishFinishedState(_node.Execute());
        }

        protected abstract bool OnSuccess();
        protected abstract bool OnFailure();
        protected abstract bool OnRunning();

        protected void Reset()
        {
            _node.Restart();
        }

        private bool PublishFinishedState(BehaviorNodeStatus state)
        {
            return state switch
            {
                BehaviorNodeStatus.Success => OnSuccess(),
                BehaviorNodeStatus.Failure => OnFailure(),
                _ => OnRunning(),
            };
        }
    }
}
