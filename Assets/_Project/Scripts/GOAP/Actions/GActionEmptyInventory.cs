using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Empty Inventory")]
    public class GActionEmptyInventory : GAction
    {
        private Storage _storage;

        public override void PrepareForPlanning()
        {
            _storage = null;

            AddPrecondition("inventory_empty", false);
            AddEffect("inventory_empty", true);

            SetCost(1.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_storage.IsInteracting)
            {
                _storage.Interact();
            }
            else
            {
                if (_storage.IsInteractionComplete)
                {
                    foreach (var item in agent.Inventory.GetItems())
                    {
                        _storage.AddToStorage(item, agent.Inventory.GetAmount(item));
                    }

                    agent.Inventory.Clear();

                    return true;
                }
            }

            return false;
        }

        public override bool CheckProceduralRequirements()
        {
            var storages = FindObjectsOfType<Storage>();
            if (storages == null || storages.Length == 0)
                return false;

            // TODO: Find nearest storage
            _storage = storages[0];

            SetTargetLocation(_storage.transform.position);

            return true;
        }
    }
}