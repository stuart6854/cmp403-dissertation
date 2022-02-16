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
        /// 
        /// </summary>
        /// <returns></returns>
        public GState GetPreconditions() => _preconditions;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GState GetEffects() => _effects;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="value"></param>
        public void AddPrecondition(string stateName, object value) => _preconditions.Set(stateName, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="value"></param>
        public void AddEffect(string stateName, object value) => _effects.Set(stateName, value);

        /// <summary>
        /// Called by the AI agent to carry out this action.
        /// </summary>
        public abstract void Run(GAgent agent);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckProceduralRequirements()
        {
            return true;
        }
    }
}