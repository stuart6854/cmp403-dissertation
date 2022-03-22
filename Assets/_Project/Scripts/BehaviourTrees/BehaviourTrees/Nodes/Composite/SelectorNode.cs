using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    /*
     * A selector node will return success if any of its children succeed.
     * It will keep processing its children until either one succeeds or
     * they all fail, at which point the selector will return failure.
     */
    public class SelectorNode : CompositeNode
    {
        private int childIndex;

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            childIndex = 0;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var currentChild = Children[childIndex];
            switch (currentChild.Update(agent, blackboard))
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Failure:
                    childIndex++;
                    break;
                case NodeState.Success:
                    return NodeState.Success;
            }

            return childIndex == Children.Count ? NodeState.Failure : NodeState.Running;
        }
    }
}