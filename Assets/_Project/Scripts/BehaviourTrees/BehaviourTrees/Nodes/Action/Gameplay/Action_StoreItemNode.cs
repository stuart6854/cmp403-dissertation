using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_StoreItemNode : ActionNode
    {
        private readonly string variableName;

        public Action_StoreItemNode(string variableName)
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
            // var gameObject = blackboard.Get<GameObject>(variableName);
            //
            // var itemProvider = gameObject.GetComponent<ItemProvider>();
            // if (itemProvider == null)
            //     return NodeState.Failure;
            //
            // agent.StoreItem(itemProvider.item);

            return NodeState.Success;
        }
    }
}