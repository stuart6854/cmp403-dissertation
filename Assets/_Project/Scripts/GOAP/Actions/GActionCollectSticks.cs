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
                _sticks.Interact(agent.gameObject);
                agent.TriggerFarmAnim();
            }
            else
            {
                if (_sticks.IsInteractionComplete && _sticks.User == agent.gameObject)
                {
                    agent.Inventory.Add("wood_logs", 1);
                    return true;
                }
            }

            return false;
        }

        public override void OnActionChosen()
        {
            _sticks.IsInUse = true;
        }

        public override bool CheckProceduralRequirements(GAgent agent)
        {
            var sticks = FindObjectsOfType<Sticks>();
            if (sticks == null || sticks.Length == 0)
            {
                ScenarioManager.Instance.SetNoSticks();
                return false;
            }

            Sticks nearestStick = null;
            var dist = float.MaxValue;
            foreach (var stick in sticks)
            {
                if (stick.IsInUse)
                    continue;

                var tempDist = Vector3.SqrMagnitude(stick.transform.position - agent.transform.position);
                if (tempDist < dist)
                {
                    nearestStick = stick;
                    dist = tempDist;
                }
            }

            if (nearestStick == null)
                return false;

            _sticks = nearestStick;
            SetCost(_sticks.TimeToCollect);

            SetTargetLocation(_sticks.transform.position);
            return true;
        }
    }
}