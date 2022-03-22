using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_InventoryHasItems : ActionNode
    {
        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            return agent.Inventory.GetTotalAmount() > 0 ? NodeState.Success : NodeState.Failure;
        }
    }
}