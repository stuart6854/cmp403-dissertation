using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class MoveTowardsNode : ActionNode
    {
        private readonly string targetVariableName;
        private readonly float moveSpeed;
        private readonly float stoppingDistance;

        public MoveTowardsNode(string targetVariableName, float moveSpeed, float stoppingDistance)
        {
            this.targetVariableName = targetVariableName;
            this.moveSpeed = moveSpeed;
            this.stoppingDistance = stoppingDistance;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var targetGameObject = blackboard.Get<GameObject>(targetVariableName);
            if (Vector3.Distance(targetGameObject.transform.position, agent.transform.position) <= stoppingDistance)
                return NodeState.Success;

            var targetPosition = targetGameObject.transform.position;
            var agentPosition = agent.transform.position;

            // Keep moving
            agentPosition = Vector3.MoveTowards(agentPosition, targetPosition, moveSpeed * Time.deltaTime);
            agent.transform.position = agentPosition;

            // Look Towards
            Vector3 lookTowardsTarget = targetPosition;
            lookTowardsTarget.y = agentPosition.y;

            agent.transform.LookAt(lookTowardsTarget);

            return NodeState.Running;
        }
    }
}