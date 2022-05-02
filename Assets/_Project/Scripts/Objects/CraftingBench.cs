using UnityEngine;

namespace stuartmillman.dissertation
{
    public class CraftingBench : WorldObject
    {
        [SerializeField] private float timeToCraft;
        private float _interactTime = 0.0f;

        public float TimeToCraft => timeToCraft;

        private void Update()
        {
            if (IsInteracting)
            {
                _interactTime -= Time.deltaTime;
                if (_interactTime <= 0.0f)
                {
                    IsInteractionComplete = true;
                    _interactTime = 0.0f;
                }
            }
        }

        public override bool Interact(GameObject user)
        {
            if (IsInteracting)
                return false;

            User = user;
            IsInteractionComplete = false;
            IsInteracting = true;
            _interactTime = timeToCraft;

            return true;
        }

        public override void CancelInteract()
        {
            IsInteracting = false;
        }
    }
}