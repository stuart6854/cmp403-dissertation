using stuartmillman.dissertation.goap;
using UnityEngine;

namespace stuartmillman.dissertation
{
    [RequireComponent(typeof(GAgent))]
    public class AgentMiner : MonoBehaviour
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

            _gAgent.AddInitialState("has_pickaxe", true);

            _gAgent.AddGoal("stones", 5);

            var rocks = FindObjectsOfType<Rock>();
            foreach (var rock in rocks)
            {
                var moveAction = ScriptableObject.CreateInstance<GActionMoveToWorldObject>();
                moveAction.SetObject(rock);
                moveAction.SetCost(Vector3.Distance(transform.position, rock.transform.position));
                _gAgent.AddAction(moveAction);

                var mineAction = ScriptableObject.CreateInstance<GActionMineRock>();
                mineAction.SetRock(rock);
                _gAgent.AddAction(mineAction);
            }
        }
    }
}