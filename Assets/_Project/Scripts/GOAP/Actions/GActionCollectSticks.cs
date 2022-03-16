using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Collect Sticks")]
    public class GActionCollectSticks : GAction
    {
        private Sticks _sticks;

        public override void PrepareForPlanning()
        {
            _sticks = null;

            AddEffect("has_wood", true);

            SetCost(2.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_sticks.IsInteracting)
            {
                _sticks.Interact();
            }
            else
            {
                if (_sticks.IsInteractionComplete)
                {
                    agent.Inventory.Add("wood_logs", 1);
                    return true;
                }
            }

            return false;
        }

        public override bool CheckProceduralRequirements()
        {
            var sticks = FindObjectsOfType<Sticks>();
            if (sticks == null || sticks.Length == 0)
                return false;

            _sticks = sticks[0];
            SetCost(_sticks.TimeToCollect);

            SetTargetLocation(_sticks.transform.position);
            return true;
        }
    }
}