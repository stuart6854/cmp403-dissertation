using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class RootNode : Node
    {
        private readonly Node child;

        public RootNode(Node childNode)
        {
            child = childNode;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            return child.Update(agent, blackboard);
        }
    }
}