using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class WaitNode : ActionNode
    {
        private readonly float seconds;
        private float startTime;

        public WaitNode(float seconds)
        {
            this.seconds = seconds;
        }

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (Time.time - startTime >= seconds)
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}