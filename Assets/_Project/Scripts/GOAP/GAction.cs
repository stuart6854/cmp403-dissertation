using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    /// <summary>
    /// GOAP action base class.
    /// </summary>
    public abstract class GAction : ScriptableObject
    {
        private float _cost = 0.0f;
        private readonly GState _preconditions = new GState();
        private readonly GState _effects = new GState();

        private Vector3 _targetLocation;
        private bool _requireAtTargetLocation;

        private bool _firstRun = true;

        /// <summary>
        /// Get the action cost.
        /// </summary>
        /// <returns></returns>
        public float GetCost() => _cost;

        /// <summary>
        /// Set the action cost.
        /// </summary>
        /// <param name="cost"></param>
        public void SetCost(float cost) => _cost = cost;

        /// <summary>
        /// Get the actions preconditions.
        /// </summary>
        /// <returns>GState with precondition states</returns>
        public GState GetPreconditions() => _preconditions;

        /// <summary>
        /// Get the actions effects.
        /// </summary>
        /// <returns>GState with effect states</returns>
        public GState GetEffects() => _effects;

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            _firstRun = true;
            _requireAtTargetLocation = false;

            _preconditions.Clear();
            _effects.Clear();
        }

        /// <summary>
        /// Adds a precondition.
        /// </summary>
        /// <param name="stateName">Name of the state</param>
        /// <param name="value">State value to be stored</param>
        public void AddPrecondition(string stateName, object value) => _preconditions.Set(stateName, value);

        /// <summary>
        /// Adds an effect.
        /// </summary>
        /// <param name="stateName">Name of the state</param>
        /// <param name="value">State value to be stored</param>
        public void AddEffect(string stateName, object value) => _effects.Set(stateName, value);

        public void SetTargetLocation(Vector3 location)
        {
            _requireAtTargetLocation = true;
            _targetLocation = location;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void PrepareForPlanning()
        {
            Reset();
        }

        /// <summary>
        /// Called by the AI agent to carry out this action.
        /// </summary>
        public bool Run(GAgent agent)
        {
            if (_firstRun)
            {
                if (_requireAtTargetLocation)
                {
                    agent.MoveTo(_targetLocation);
                }

                _firstRun = false;
                return false;
            }

            if (_requireAtTargetLocation && !agent.AtDestination())
            {
                agent.MoveTo(_targetLocation);
                return false;
            }

            return Run_Internal(agent);
        }

        protected abstract bool Run_Internal(GAgent agent);

        /// <summary>
        /// Runs procedural checks to determine if this action can run.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns>True if action can run</returns>
        public virtual bool CheckProceduralRequirements(GAgent agent)
        {
            return true;
        }
    }
}