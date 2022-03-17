using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Craft Item")]
    public class GActionCraftItem : GAction
    {
        [SerializeField] private string _itemName;
        [SerializeField] private int _itemAmount;

        [SerializeField] private string[] _requiredItemNames;
        [SerializeField] private int[] _requiredItemAmounts;

        private CraftingBench _craftingBench;

        public override void PrepareForPlanning()
        {
            _craftingBench = null;

            for (int i = 0; i < _requiredItemNames.Length; i++)
            {
                AddPrecondition(_requiredItemNames[i], _requiredItemAmounts[i]);
            }

            AddEffect(_itemName, _itemAmount);

            SetCost(5.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_craftingBench.IsInteracting)
            {
                _craftingBench.Interact();
            }
            else
            {
                if (_craftingBench.IsInteractionComplete)
                {
                    _craftingBench.ResetInteraction();

                    agent.Inventory.Add(_itemName, _itemAmount);
                    return true;
                }
            }

            return false;
        }

        public override bool CheckProceduralRequirements()
        {
            var craftingBenches = FindObjectsOfType<CraftingBench>();
            if (craftingBenches == null || craftingBenches.Length == 0)
                return false;

            _craftingBench = craftingBenches[0];
            SetCost(_craftingBench.TimeToCraft);

            SetTargetLocation(_craftingBench.transform.position);
            return true;
        }
    }
}