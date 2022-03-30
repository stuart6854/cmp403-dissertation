using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_FindNearestWorldObject<T> : ActionNode where T : WorldObject
    {
        private readonly string variableToSaveTo;

        public Action_FindNearestWorldObject(string variableToSaveTo)
        {
            this.variableToSaveTo = variableToSaveTo;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var objects = Object.FindObjectsOfType<T>();

            if (objects != null)
            {
                WorldObject nearestObj = null;
                var dist = float.MaxValue;
                foreach (var worldObject in objects)
                {
                    if (worldObject.IsInUse)
                        continue;

                    var tempDist = Vector3.SqrMagnitude(worldObject.transform.position - agent.transform.position);
                    if (tempDist < dist)
                    {
                        nearestObj = worldObject;
                        dist = tempDist;
                    }
                }

                if (nearestObj == null)
                    return NodeState.Failure;

                blackboard.Set(variableToSaveTo, nearestObj.gameObject);

                // Mark WorldObject as in use
                nearestObj.IsInUse = true;

                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}