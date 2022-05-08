using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_Farm : ActionNode
    {
        private readonly string _variableName;
        private readonly string _cropName;
        private readonly int _cropAmount;

        private FarmLand _farmLand;

        public Action_Farm(string variableName, string cropName, int cropAmount)
        {
            this._variableName = variableName;
            this._cropName = cropName;
            this._cropAmount = cropAmount;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var farmLandObj = blackboard.Get<GameObject>(_variableName);
            _farmLand = farmLandObj.GetComponent<FarmLand>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            agent.name = agent.OriginalAgentName + " [Action_Farm]";

            if (!_farmLand.IsInteracting && !_farmLand.IsInteractionComplete)
            {
                _farmLand.Interact(agent.gameObject);
                agent.TriggerFarmAnim();
            }
            else
            {
                if (_farmLand.IsInteractionComplete && _farmLand.User == agent.gameObject)
                {
                    _farmLand.ResetInteraction();

                    agent.Inventory.Add(_cropName, _cropAmount);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}