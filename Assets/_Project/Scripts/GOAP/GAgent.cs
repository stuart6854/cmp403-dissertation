using System;
using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    public class GAgent : MonoBehaviour
    {
        [SerializeField] private GActionList actionList;

        private readonly GState _initialState = new GState();
        private readonly GState _goalState = new GState();

        private GActionList _actionList;

        private GAction[] _actionPlan;

        private void Start()
        {
            _actionList = actionList.Clone();
        }

        /// <summary>
        /// Clears all the agents current state.
        /// </summary>
        public void ClearInitialState()
        {
            _initialState.Clear();
        }

        /// <summary>
        /// Add a state to the agents initial states.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="value"></param>
        public void AddInitialState(string stateName, object value)
        {
            _initialState.Set(stateName, value);
        }

        /// <summary>
        /// Clear all the agents goal states.
        /// </summary>
        public void ClearGoals()
        {
            _goalState.Clear();
        }

        /// <summary>
        /// Add a goal state to the agent.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="value"></param>
        public void AddGoal(string stateName, object value)
        {
            _goalState.Set(stateName, value);
        }

        /// <summary>
        /// Request the agent to create a new action plan.
        /// </summary>
        public void Plan()
        {
            var newPlan = GPlanner.Plan(_actionList, _initialState, _goalState);
            if (newPlan.Length == 0)
            {
                // Failed to create plane
            }

            _actionPlan = newPlan;
        }
    }
}