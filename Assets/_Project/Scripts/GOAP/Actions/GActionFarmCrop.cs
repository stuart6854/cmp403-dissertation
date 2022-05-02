using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Farm Crop")]
    public class GActionFarmCrop : GAction
    {
        [SerializeField] private string _cropName;
        [SerializeField] private int _cropAmount;

        private FarmLand _farmLand;

        public override void PrepareForPlanning()
        {
            _farmLand = null;

            AddEffect(_cropName, _cropAmount);

            SetCost(10.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_farmLand.IsInteracting && !_farmLand.IsInteractionComplete)
            {
                _farmLand.Interact(agent.gameObject);
                agent.TriggerFarmAnim();
            }
            else
            {
                if (_farmLand.IsInteractionComplete && _farmLand.User == agent.gameObject)
                {
                    _farmLand.ResetInteraction();

                    agent.Inventory.Add(_cropName, _cropAmount);
                    return true;
                }
            }

            return false;
        }

        public override bool CheckProceduralRequirements(GAgent agent)
        {
            var farmLands = FindObjectsOfType<FarmLand>();
            if (farmLands == null || farmLands.Length == 0)
                return false;

            foreach (var farmLand in farmLands)
            {
                if (farmLand.CropName == _cropName)
                    _farmLand = farmLand;
            }

            if (_farmLand == null)
                return false;

            SetCost(_farmLand.TimeToGrow);

            SetTargetLocation(_farmLand.transform.position);

            return true;
        }
    }
}