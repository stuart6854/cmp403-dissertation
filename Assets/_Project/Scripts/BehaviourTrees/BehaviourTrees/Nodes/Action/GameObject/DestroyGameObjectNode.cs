using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class DestroyGameObjectNode : ActionNode
    {
        private readonly string variableName;

        public DestroyGameObjectNode(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var gameObject = blackboard.Get<GameObject>(variableName);
            Object.Destroy(gameObject);

            return NodeState.Success;
        }
    }
}