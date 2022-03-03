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
            _gAgent.ClearActions();

            _gAgent.AddInitialState("has_axe", true);

            _gAgent.AddGoal("wood_logs", 5);

            var trees = FindObjectsOfType<Tree>();
            foreach (var tree in trees)
            {
                var chopAction = ScriptableObject.CreateInstance<GActionChopTree>();
                chopAction.SetTree(tree);
                _gAgent.AddAction(chopAction);
            }
        }
    }
}