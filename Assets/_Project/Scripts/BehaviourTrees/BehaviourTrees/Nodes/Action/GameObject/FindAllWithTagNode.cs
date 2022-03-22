using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class FindAllWithTagNode : ActionNode
    {
        private readonly string tag;
        private readonly string variableToSaveTo;

        public FindAllWithTagNode(string tag, string variableToSaveTo)
        {
            this.tag = tag;
            this.variableToSaveTo = variableToSaveTo;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var gameObjects = GameObject.FindGameObjectsWithTag(tag);

            var gameObjectList = new List<GameObject>();
            if (gameObjects != null)
                gameObjectList.AddRange(gameObjects);

            blackboard.Set(variableToSaveTo, gameObjectList);

            return NodeState.Success;
        }
    }
}