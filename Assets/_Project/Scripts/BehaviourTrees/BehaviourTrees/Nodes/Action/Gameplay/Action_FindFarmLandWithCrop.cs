using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_FindFarmLandWithCrop : ActionNode
    {
        private readonly string _variableToSaveTo;
        private readonly string _cropName;

        public Action_FindFarmLandWithCrop(string variableToSaveTo, string cropName)
        {
            this._variableToSaveTo = variableToSaveTo;
            this._cropName = cropName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            agent.name = agent.OriginalAgentName + " [Action_FindFarmLandWithCrop]";

            var farmlands = Object.FindObjectsOfType<FarmLand>();

            if (farmlands != null)
            {
                foreach (var farmLand in farmlands)
                {
                    if (farmLand.IsInUse)
                        continue;

                    if (farmLand.CropName == _cropName)
                    {
                        blackboard.Set(_variableToSaveTo, farmLand.gameObject);
                        farmLand.IsInUse = true;
                        return NodeState.Success;
                    }
                }
            }

            return NodeState.Failure;
        }
    }
}