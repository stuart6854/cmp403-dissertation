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

        protected override void OnStart()
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