using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_FindGameObjectsWithComponent<T> : ActionNode where T : MonoBehaviour
    {
        private readonly string variableToSaveTo;

        public Action_FindGameObjectsWithComponent(string variableToSaveTo)
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

            var objectList = new List<GameObject>();
            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    objectList.Add(obj.gameObject);
                }
            }

            blackboard.Set(variableToSaveTo, objectList);

            return NodeState.Success;
        }
    }
}