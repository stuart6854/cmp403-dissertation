using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class CheckNullNode : ActionNode
    {
        private readonly string variableName;

        public CheckNullNode(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var variableValue = blackboard.Get<Object>(variableName);
            return variableValue == null ? NodeState.Success : NodeState.Failure;
        }
    }
}