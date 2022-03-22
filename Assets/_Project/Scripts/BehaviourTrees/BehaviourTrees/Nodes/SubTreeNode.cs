using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class SubTreeNode : Node
    {
        private readonly SubTree tree;

        public SubTreeNode(SubTree tree)
        {
            this.tree = tree;
            this.tree.InitTree();
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            return tree.UpdateTree(agent, blackboard);
        }
    }
}