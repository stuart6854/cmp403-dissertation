using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_EmptyInventoryIntoStorage : ActionNode
    {
        private readonly string variableName;

        public Action_EmptyInventoryIntoStorage(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var storageGameObject = blackboard.Get<GameObject>(variableName);

            var storage = storageGameObject.GetComponent<Storage>();
            if (storage == null)
                return NodeState.Failure;

            foreach (var item in agent.Inventory.GetItems())
            {
                storage.AddToStorage(item, agent.Inventory.GetAmount(item));
            }

            agent.Inventory.Clear();

            return NodeState.Success;
        }
    }
}