using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_CraftItem : ActionNode
    {
        private readonly string _variableName;
        private readonly string _itemName;
        private readonly int _itemAmount;

        private readonly string[] _requiredItemNames;
        private readonly int[] _requiredItemAmounts;

        private CraftingBench _craftingBench;

        public Action_CraftItem(string variableName, string itemName, int itemAmount, string[] requiredItemNames,
            int[] requiredItemAmounts)
        {
            this._variableName = variableName;
            this._itemName = itemName;
            this._itemAmount = itemAmount;
            this._requiredItemNames = requiredItemNames;
            this._requiredItemAmounts = requiredItemAmounts;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var treeObject = blackboard.Get<GameObject>(_variableName);
            _craftingBench = treeObject.GetComponent<CraftingBench>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (!_craftingBench.IsInteracting)
            {
                _craftingBench.Interact();
            }
            else
            {
                if (_craftingBench.IsInteractionComplete)
                {
                    for (int i = 0; i < _requiredItemNames.Length; i++)
                    {
                        agent.Inventory.Remove(_requiredItemNames[i], _requiredItemAmounts[i]);
                    }

                    agent.Inventory.Add(_itemName, _itemAmount);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}