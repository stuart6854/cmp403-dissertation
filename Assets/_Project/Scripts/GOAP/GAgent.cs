using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace stuartmillman.dissertation.goap
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Inventory))]
    [DisallowMultipleComponent]
    public class GAgent : MonoBehaviour
    {
        [SerializeField] private GActionList actionList;

        private NavMeshAgent _navAgent;
        private Inventory _inventory;

        private readonly GState _initialState = new GState();
        private readonly GState _goalState = new GState();

        private GActionList _actionList;

        private Queue<GAction> _actionPlan;
        private GState _currentState;

        public Inventory Inventory => _inventory;

        public GState State => _currentState;
        public bool HasPlan => _actionPlan != null && _actionPlan.Count > 0;

        private string _agentName;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _inventory = GetComponent<Inventory>();

            _actionList = actionList.Clone();

            _agentName = this.gameObject.name;
        }

        /// <summary>
        /// Clears all the agents current state.
        /// </summary>
        public void ClearInitialState()
        {
            _initialState.Clear();

            _initialState.Set("inventory_empty", _inventory.GetTotalAmount() == 0);
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

        public void ClearActions()
        {
            _actionList.Actions.Clear();
        }

        public void AddAction(GAction action)
        {
            _actionList.Actions.Add(action);
        }

        /// <summary>
        /// Request the agent to create a new action plan.
        /// </summary>
        public void Plan()
        {
            var newPlan = GPlanner.Plan(_actionList, _initialState, _goalState);
            if (newPlan == null || newPlan.Count == 0)
            {
                // Failed to create plane
                Debug.LogWarning("[GAgent] Failed to create plan.", this);
                return;
            }

            _actionPlan = newPlan;
            _currentState = new GState(_initialState);
        }

        private void Update()
        {
            // Run plan
            if (_actionPlan != null && _actionPlan.Count > 0)
            {
                var currentAction = _actionPlan.Peek();

                this.gameObject.name = _agentName + " [" + currentAction.name + "]";
                if (currentAction.Run(this))
                {
                    _actionPlan.Dequeue();
                }

                if (_actionPlan.Count == 0)
                {
                    Debug.Log("[GAgent] Finished plan.");
                    this.gameObject.name = _agentName + " []";
                    _actionPlan = null;
                }
            }
        }

        public void MoveTo(Vector3 position)
        {
            _navAgent.destination = position;
            _navAgent.isStopped = false;
        }

        public bool AtDestination()
        {
            if (_navAgent.pathPending) return false;
            if (_navAgent.remainingDistance >= _navAgent.stoppingDistance) return false;
            if (_navAgent.pathStatus == NavMeshPathStatus.PathComplete)
                return true;

            return false;
        }
    }
}