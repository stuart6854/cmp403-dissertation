using System.Collections;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class PickRandomListElementNode : ActionNode
    {
        private readonly string listName;
        private readonly string variableToSaveTo;

        public PickRandomListElementNode(string listName, string variableToSaveTo)
        {
            this.listName = listName;
            this.variableToSaveTo = variableToSaveTo;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var list = blackboard.Get<IList>(listName);

            var randomElement = list[Random.Range(0, list.Count)];
            blackboard.Set(variableToSaveTo, randomElement);

            return NodeState.Success;
        }
    }
}