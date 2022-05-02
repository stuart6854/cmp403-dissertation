using stuartmillman.dissertation.goap;
using UnityEngine;

namespace stuartmillman.dissertation
{
    [RequireComponent(typeof(GAgent))]
    public class AgentLogger : MonoBehaviour
    {
        private GAgent _gAgent;

        private void Awake()
        {
            _gAgent = GetComponent<GAgent>();
        }

        private void Update()
        {
            if (!_gAgent.HasPlan)
            {
                PrepareToPlan();
                _gAgent.Plan();
            }
        }

        private void PrepareToPlan()
        {
            _gAgent.ClearInitialState();
            _gAgent.ClearGoals();
            // _gAgent.ClearActions();

            _gAgent.AddInitialState("has_axe", true);

            if (_gAgent.Inventory.GetTotalAmount() == 0)
            {
                _gAgent.AddGoal("has_wood", true);
            }
            else
            {
                _gAgent.AddGoal("inventory_empty", true);
            }
        }
    }
}