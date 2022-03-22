using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    /*
     * A sequencer node will visit each child node in order,
     * as long a each child succeeds. If all the child nodes succeed,
     * the sequence will return a success. If any child fails, the sequence
     * will return a failure.
     */
    public class SequencerNode : CompositeNode
    {
        private int childIndex;

        protected override void OnStart()
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
                    return NodeState.Failure;
                case NodeState.Success:
                    childIndex++;
                    break;
            }

            return childIndex == Children.Count ? NodeState.Success : NodeState.Running;
        }
    }
}