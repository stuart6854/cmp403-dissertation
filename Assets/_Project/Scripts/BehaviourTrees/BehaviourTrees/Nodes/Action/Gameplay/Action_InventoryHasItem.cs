using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_InventoryHasItem : ActionNode
    {
        private readonly string _itemName;
        private readonly int _itemAmount;

        public Action_InventoryHasItem(string itemName, int itemAmount)
        {
            this._itemName = itemName;
            this._itemAmount = itemAmount;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (!agent.Inventory.Has(_itemName))
                return NodeState.Failure;

            if (agent.Inventory.GetAmount(_itemName) < _itemAmount)
                return NodeState.Failure;

            return NodeState.Success;
        }
    }
}