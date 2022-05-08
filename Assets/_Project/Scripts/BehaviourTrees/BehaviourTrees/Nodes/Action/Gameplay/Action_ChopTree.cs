using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_ChopTree : ActionNode
    {
        private readonly string variableName;

        private Tree _tree;

        public Action_ChopTree(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var treeObject = blackboard.Get<GameObject>(variableName);
            _tree = treeObject.GetComponent<Tree>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            agent.name = agent.OriginalAgentName + " [ChopTree]";

            // if (_tree == null)
            //     return NodeState.Failure;

            if (!_tree.IsInteracting)
            {
                _tree.Interact(agent.gameObject);
                agent.TriggerChopAnim();
            }
            else
            {
                if (_tree.IsInteractionComplete && _tree.User == agent.gameObject)
                {
                    agent.Inventory.Add("wood_logs", 5);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}