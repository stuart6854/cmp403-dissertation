using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_MoveTo : ActionNode
    {
        private readonly string targetVariableName;
        private readonly float moveSpeed;
        private readonly float stoppingDistance;

        public Action_MoveTo(string targetVariableName, float moveSpeed, float stoppingDistance)
        {
            this.targetVariableName = targetVariableName;
            this.moveSpeed = moveSpeed;
            this.stoppingDistance = stoppingDistance;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var targetGameObject = blackboard.Get<GameObject>(targetVariableName);
            agent.MoveTo(targetGameObject.transform.position);
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (agent.AtDestination())
                return NodeState.Success;

            return NodeState.Running;
        }
    }
}