using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_InventoryHasItems : ActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            return agent != null ? NodeState.Success : NodeState.Failure;
        }
    }
}