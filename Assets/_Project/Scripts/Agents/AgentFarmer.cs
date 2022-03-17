using stuartmillman.dissertation.goap;
using UnityEngine;

namespace stuartmillman.dissertation
{
    [RequireComponent(typeof(GAgent))]
    public class AgentFarmer : MonoBehaviour
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

            if (_gAgent.Inventory.GetTotalAmount() == 0)
            {
                _gAgent.AddGoal("food", 1);
            }
            else
            {
                _gAgent.AddGoal("inventory_empty", true);
            }
        }
    }
}