using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    /// <summary>
    /// Brains of the GOAP system.
    /// </summary>
    public class GPlanner
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionList"></param>
        /// <param name="initialState">The initial world/agent state</param>
        /// <param name="goalState">The goal state</param>
        /// <returns>Array of GAction for agent to carry out</returns>
        public static Queue<GAction> Plan(GActionList actionList, GState initialState, GState goalState)
        {
            // TODO: Reset actions

            // Check what actions can run using their ProceduralConditions
            var usableActions = new List<GAction>();
            foreach (var action in actionList.Actions)
            {
                if (action.CheckProceduralRequirements())
                    usableActions.Add(action);
            }

            // List to hold nodes that have achieved the goal state
            var endNodes = new List<GNode>();

            // Build list of possible actions
            var start = new GNode(null, 0, initialState, null);
            var foundSolution = BuildGraph(start, endNodes, usableActions, goalState);
            if (!foundSolution)
            {
                Debug.Log("[GPlanner] No plan.");
                return null;
            }

            // Pick cheapest action list
            var cheapest = endNodes.First();
            for (int i = 1; i < endNodes.Count; i++)
            {
                var leave = endNodes[i];
                if (leave.runningCost < cheapest.runningCost)
                {
                    cheapest = leave;
                }
            }

            // Build action list
            var actionPlanList = new List<GAction>();
            var node = cheapest;
            while (node.parent != null)
            {
                actionPlanList.Insert(0, node.action);
                node = node.parent;
            }

            // Return action plan as queue
            var actionPlan = new Queue<GAction>(actionPlanList);
            return actionPlan;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="endNodes"></param>
        /// <param name="usableActions"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        private static bool BuildGraph(GNode parent, List<GNode> endNodes, List<GAction> usableActions, GState goal)
        {
            bool foundSolution = false;

            // Go through each available action at this node and see if we can use it here
            foreach (var action in usableActions)
            {
                // If the parent state has the conditions for this actions preconditions,
                // we can use it here
                if (InState(parent.state, action.GetPreconditions()))
                {
                    // Apply the actions effects to the parent state
                    var currentState = ApplyState(parent.state, action.GetEffects());

                    var node = new GNode(parent, parent.runningCost + action.GetCost(), currentState, action);

                    // If the current state == goal state
                    if (InState(currentState, goal))
                    {
                        endNodes.Add(node);
                        // We found a solution
                        foundSolution = true;
                    }
                    else
                    {
                        // Not a solution yet, so test all the remaining actions (minus this action)
                        // and branch out the tree
                        var actionSubset = CreateActionSubset(usableActions, action);

                        bool found = BuildGraph(node, endNodes, actionSubset, goal);
                        if (found)
                        {
                            foundSolution = true;
                        }
                    }
                }
            }

            return foundSolution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="toTest"></param>
        /// <returns></returns>
        private static bool InState(GState state, GState toTest)
        {
            foreach (var stateToFind in toTest.GetState())
            {
                if (!state.Has(stateToFind.Key))
                {
                    return false;
                }

                // Check that the values are the same
                state.Get(stateToFind.Key, out var value);
                if (!stateToFind.Value.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseState"></param>
        /// <param name="toAddState"></param>
        /// <returns></returns>
        private static GState ApplyState(GState baseState, GState toAddState)
        {
            GState newState = new GState(baseState);

            // Add base state
            foreach (var state in baseState.GetState())
            {
                newState.Set(state.Key, state.Value);
            }

            // Add new state
            foreach (var state in toAddState.GetState())
            {
                newState.Set(state.Key, state.Value);
            }

            return newState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="toRemoveAction"></param>
        /// <returns></returns>
        private static List<GAction> CreateActionSubset(List<GAction> actions, GAction toRemoveAction)
        {
            var subsetList = new List<GAction>(actions);
            subsetList.Remove(toRemoveAction);
            return subsetList;
        }
    }

    class GNode
    {
        public GNode parent;
        public int runningCost;
        public GState state;
        public GAction action;

        public GNode(GNode parent, int runningCost, GState state, GAction action)
        {
            this.parent = parent;
            this.runningCost = runningCost;
            this.state = state;
            this.action = action;
        }
    }
}