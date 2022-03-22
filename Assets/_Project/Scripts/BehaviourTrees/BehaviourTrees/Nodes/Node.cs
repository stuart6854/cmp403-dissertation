using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public abstract class Node
    {
        public NodeState State { get; private set; }
        public bool hasStarted { get; private set; } = false;

        public NodeState Update(BTAgent agent, Blackboard blackboard)
        {
            if (!hasStarted)
            {
                OnStart(agent, blackboard);
                hasStarted = true;
            }

            State = OnUpdate(agent, blackboard);

            if (State == NodeState.Failure || State == NodeState.Success)
            {
                OnStop();
                hasStarted = false;
            }

            return State;
        }

        protected abstract void OnStart(BTAgent agent, Blackboard blackboard);
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate(BTAgent agent, Blackboard blackboard);
    }
}