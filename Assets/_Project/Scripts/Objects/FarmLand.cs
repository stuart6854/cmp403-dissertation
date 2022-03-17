using UnityEngine;

namespace stuartmillman.dissertation
{
    public class FarmLand : WorldObject
    {
        [SerializeField] private string cropName;
        [SerializeField] private float timeToGrow;
        private float _interactTime = 0.0f;

        public string CropName => cropName;
        public float TimeToGrow => timeToGrow;

        private void Update()
        {
            if (IsInteracting)
            {
                _interactTime -= Time.deltaTime;
                if (_interactTime <= 0.0f)
                {
                    IsInteractionComplete = true;
                    _interactTime = 0.0f;
                    IsInteracting = false;
                }
            }
        }

        public override bool Interact()
        {
            if (IsInteracting)
                return false;

            IsInteractionComplete = false;
            IsInteracting = true;
            _interactTime = timeToGrow;

            return true;
        }

        public override void CancelInteract()
        {
            IsInteracting = false;
        }
    }
}