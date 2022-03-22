using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    /*
     * Inverter node inverts its child's outputs.
     * Failure -> Success.
     * Success -> Failure.
     */
    public class InverterNode : DecoratorNode
    {
        public InverterNode(Node child) : base(child)
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
            switch (Child.Update(agent, blackboard))
            {
                case NodeState.Running: return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Success;
                case NodeState.Success:
                    return NodeState.Failure;
            }

            Debug.LogError("[InverterNode] We shouldn't have gotten here!");
            return NodeState.Running;
        }
    }
}