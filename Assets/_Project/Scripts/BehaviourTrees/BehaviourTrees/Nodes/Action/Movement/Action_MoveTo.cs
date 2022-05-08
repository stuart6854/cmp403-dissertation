using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_MoveTo : ActionNode
    {
        private readonly string targetVariableName;

        public Action_MoveTo(string targetVariableName)
        {
            this.targetVariableName = targetVariableName;
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
            agent.name = agent.OriginalAgentName + " [Action_MoveTo]";
            
            if (agent.AtDestination())
                return NodeState.Success;

            return NodeState.Running;
        }
    }
}