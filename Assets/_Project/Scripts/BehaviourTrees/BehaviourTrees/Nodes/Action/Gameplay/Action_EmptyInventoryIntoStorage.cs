using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_EmptyInventoryIntoStorage : ActionNode
    {
        private readonly string variableName;

        private Storage _storage;

        public Action_EmptyInventoryIntoStorage(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var storageGameObject = blackboard.Get<GameObject>(variableName);
            _storage = storageGameObject.GetComponent<Storage>();
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
                    _storage.ResetInteraction();
                    
                    foreach (var item in agent.Inventory.GetItems())
                    {
                        _storage.AddToStorage(item, agent.Inventory.GetAmount(item));
                    }

                    agent.Inventory.Clear();

                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}