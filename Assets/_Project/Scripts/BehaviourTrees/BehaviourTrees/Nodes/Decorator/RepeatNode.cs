using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class RepeatNode : DecoratorNode
    {
        public RepeatNode(Node child) : base(child)
        {
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            Child.Update(agent, blackboard);
            return NodeState.Running;
        }
    }
}