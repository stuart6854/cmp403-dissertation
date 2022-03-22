using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class PrintNode : ActionNode
    {
        private string message;

        public PrintNode(string message)
        {
            this.message = message;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}