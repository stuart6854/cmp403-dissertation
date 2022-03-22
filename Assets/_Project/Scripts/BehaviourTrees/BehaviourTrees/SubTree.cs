using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public abstract class SubTree
    {
        private RootNode rootNode;
        public NodeState TreeState { get; private set; }

        private readonly Blackboard blackboard;

        protected SubTree()
        {
            blackboard = new Blackboard();
        }

        public void InitTree()
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