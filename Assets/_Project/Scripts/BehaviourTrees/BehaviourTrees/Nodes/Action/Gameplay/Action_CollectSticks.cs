using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_CollectSticks : ActionNode
    {
        private readonly string variableName;

        private Sticks _sticks;

        public Action_CollectSticks(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var sticksObj = blackboard.Get<GameObject>(variableName);
            _sticks = sticksObj.GetComponent<Sticks>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            agent.name = agent.OriginalAgentName + " [Action_CollectSticks]";

            if (!_sticks.IsInteracting)
            {
                _sticks.Interact(agent.gameObject);
                agent.TriggerFarmAnim();
            }
            else
            {
                if (_sticks.IsInteractionComplete && _sticks.User == agent.gameObject)
                {
                    agent.Inventory.Add("wood_logs", 1);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}