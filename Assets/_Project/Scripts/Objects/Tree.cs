using System;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Tree : WorldObject
    {
        [SerializeField] private float timeToChop;
        [SerializeField] private float _interactTime = 0.0f;

        public float TimeToChop => timeToChop;

        private void Update()
        {
            if (IsInteracting)
            {
                _interactTime -= Time.deltaTime;
                if (_interactTime <= 0.0f)
                {
                    IsInteractionComplete = true;
                    _interactTime = 0.0f;

                    Destroy(this.gameObject);
                }
            }
        }

        public override bool Interact()
        {
            if (IsInteracting)
                return false;

            IsInteracting = true;
            _interactTime = timeToChop;

            return true;
        }

        public override void CancelInteract()
        {
            IsInteracting = false;
        }
    }
}