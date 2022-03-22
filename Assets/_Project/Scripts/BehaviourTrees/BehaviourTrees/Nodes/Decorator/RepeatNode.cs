using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class RepeatNode : DecoratorNode
    {
        public RepeatNode(Node child) : base(child)
        {
        }

        protected override void OnStart()
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