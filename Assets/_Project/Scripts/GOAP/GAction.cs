using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    /// <summary>
    /// GOAP action base class.
    /// </summary>
    public abstract class GAction : ScriptableObject
    {
        private int _cost = 0;
        private readonly GState _preconditions = new GState();
        private readonly GState _effects = new GState();

        /// <summary>
        /// Get the action cost.
        /// </summary>
        /// <returns></returns>
        public int GetCost() => _cost;

        /// <summary>
        /// Set the action cost.
        /// </summary>
        /// <param name="cost"></param>
        public void SetCost(int cost) => _cost = cost;

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

        /// <summary>
        /// Called by the AI agent to carry out this action.
        /// </summary>
        public abstract void Run(GAgent agent);

        /// <summary>
        /// Runs procedural checks to determine if this action can run.
        /// </summary>
        /// <returns>True if action can run</returns>
        public virtual bool CheckProceduralRequirements()
        {
            return true;
        }
    }
}