using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Get From Storage")]
    public class GActionGetFromStorage : GAction
    {
        [SerializeField] private string _itemName = "";
        [SerializeField] private int _itemAmount = 0;

        private Storage _storage;

        public override void PrepareForPlanning()
        {
            _storage = null;

            AddEffect(_itemName, _itemAmount);

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
                    _storage.ResetInteraction();

                    // TODO: Make sure can remove from inventory
                    _storage.RemoveFromStorage(_itemName, _itemAmount);
                    agent.Inventory.Add(_itemName, _itemAmount);
                    return true;
                }
            }

            return false;
        }

        public override bool CheckProceduralRequirements(GAgent agent)
        {
            var storages = FindObjectsOfType<Storage>();
            if (storages == null || storages.Length == 0)
                return false;

            // Find storage with required item (and amount)
            foreach (var storage in storages)
            {
                if (storage.HasInStorage(_itemName))
                    if (storage.QueryAmount(_itemName) >= _itemAmount)
                    {
                        _storage = storage;
                        break;
                    }
            }

            // No storage with item found, return false
            if (_storage == null)
                return false;

            SetTargetLocation(_storage.transform.position);

            return true;
        }
    }
}