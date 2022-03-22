using System.Collections;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class ListIsEmptyNode : ActionNode
    {
        private readonly string listName;

        public ListIsEmptyNode(string listName)
        {
            this.listName = listName;
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
            return list.Count == 0 ? NodeState.Success : NodeState.Failure;
        }
    }
}