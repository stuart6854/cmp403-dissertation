using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class PrintVarNode : ActionNode
    {
        private readonly string variableName;

        public PrintVarNode(string variableName)
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
            Debug.Log(blackboard.Get<object>(variableName).ToString());
            return NodeState.Success;
        }
    }
}