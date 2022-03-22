using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class SortGameObjectListByDistNode : ActionNode
    {
        private readonly string listName;
        private readonly string variableToSaveTo;

        public SortGameObjectListByDistNode(string listName, string variableToSaveTo)
        {
            this.listName = listName;
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
            var originalList = blackboard.Get<List<GameObject>>(listName);
            var sortedList = new List<GameObject>(originalList);
            sortedList.Sort((GameObject a, GameObject b) =>
            {
                var position = agent.transform.position;
                float distA = (a.transform.position - position).sqrMagnitude;
                float distB = (b.transform.position - position).sqrMagnitude;
                return distA.CompareTo(distB);
            });

            blackboard.Set(variableToSaveTo, sortedList);

            return NodeState.Success;
        }
    }
}