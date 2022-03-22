using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public abstract class ConditionalNode<T> : ActionNode
    {
        protected readonly string variableName;
        protected T requiredValue;

        protected ConditionalNode(string variableName, T requiredValue)
        {
            this.variableName = variableName;
            this.requiredValue = requiredValue;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            return ConditionMet(blackboard) ? NodeState.Success : NodeState.Failure;
        }

        protected abstract bool ConditionMet(Blackboard blackboard);
    }
}