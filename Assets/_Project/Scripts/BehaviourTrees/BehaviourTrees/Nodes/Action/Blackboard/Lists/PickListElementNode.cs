using System.Collections;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class PickListElementNode : ActionNode
    {
        private readonly string listName;
        private readonly int index;
        private readonly string variableToSaveTo;

        public PickListElementNode(string listName, int index, string variableToSaveTo)
        {
            this.listName = listName;
            this.index = index;
            this.variableToSaveTo = variableToSaveTo;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var list = blackboard.Get<IList>(listName);

            if (index < 0 || index >= list.Count)
                return NodeState.Failure;

            var pickedElement = list[index];
            blackboard.Set(variableToSaveTo, pickedElement);

            return NodeState.Success;
        }
    }
}