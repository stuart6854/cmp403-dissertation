using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Mine Rock")]
    public class GActionMineRock : GAction
    {
        private Rock _rock;

        public GActionMineRock()
        {
            AddPrecondition("has_pickaxe", true);
            AddEffect("stones", 5);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_rock.IsInteracting)
            {
                _rock.Interact();
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

        public override bool CheckProceduralRequirements()
        {
            var rocks = FindObjectsOfType<Rock>();
            if (rocks == null || rocks.Length == 0)
                return false;

            // TODO: Find the nearest rock
            _rock = rocks[0];
            SetCost(_rock.TimeToMine);

            SetTargetLocation(_rock.transform.position);

            return true;
        }
    }
}