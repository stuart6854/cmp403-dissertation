using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_StorageTakeItem : ActionNode
    {
        private readonly string _variableName;
        private readonly string _itemName;
        private readonly int _itemAmount;

        private Storage _storage;

        public Action_StorageTakeItem(string variableName, string itemName, int itemAmount)
        {
            this._variableName = variableName;
            this._itemName = itemName;
            this._itemAmount = itemAmount;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var treeObject = blackboard.Get<GameObject>(_variableName);
            _storage = treeObject.GetComponent<Storage>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (!_storage.IsInteracting)
            {
                _storage.Interact(agent.gameObject);
            }
            else
            {
                if (_storage.IsInteractionComplete && _storage.User == agent.gameObject)
                {
                    _storage.RemoveFromStorage(_itemName, _itemAmount);
                    agent.Inventory.Add(_itemName, _itemAmount);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}