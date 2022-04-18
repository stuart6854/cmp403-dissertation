using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Mine Rock")]
    public class GActionMineRock : GAction
    {
        private Rock _rock;

        public override void PrepareForPlanning()
        {
            _rock = null;

            AddPrecondition("has_pickaxe", true);
            AddEffect("stones", 5);

            SetCost(5.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_rock.IsInteracting)
            {
                _rock.Interact();
                agent.TriggerChopAnim();
            }
            else
            {
                if (_rock.IsInteractionComplete)
                {
                    agent.Inventory.Add("stones", 5);
                    return true;
                }
            }

            return false;
        }

        public override void OnActionChosen()
        {
            _rock.IsInUse = true;
        }

        public override bool CheckProceduralRequirements(GAgent agent)
        {
            var rocks = FindObjectsOfType<Rock>();
            if (rocks == null || rocks.Length == 0)
            {
                ScenarioManager.Instance.SetNoRocks();
                return false;
            }

            Rock nearestRock = null;
            var dist = float.MaxValue;
            foreach (var rock in rocks)
            {
                if (rock.IsInUse)
                    continue;

                var tempDist = Vector3.SqrMagnitude(rock.transform.position - agent.transform.position);
                if (tempDist < dist)
                {
                    nearestRock = rock;
                    dist = tempDist;
                }
            }

            if (nearestRock == null)
                return false;

            _rock = nearestRock;
            SetCost(_rock.TimeToMine);

            SetTargetLocation(_rock.transform.position);

            return true;
        }
    }
}