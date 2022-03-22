using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        private RootNode rootNode;
        public NodeState TreeState { get; private set; }

        public void InitTree(Blackboard blackboard)
        {
            PreRegisterVariables(blackboard);
            rootNode = new RootNode(BuildTree());
        }

        public NodeState UpdateTree(BTAgent agent, Blackboard blackboard)
        {
            if (rootNode.State == NodeState.Running)
                TreeState = rootNode.Update(agent, blackboard);

            return TreeState;
        }

        protected abstract void PreRegisterVariables(Blackboard blackboard);

        protected abstract Node BuildTree();
    }
}