using System;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Rock : WorldObject
    {
        [SerializeField] private float timeToMine;
        private float _interactTime = 0.0f;

        public float TimeToMine => timeToMine;

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

        private void LateUpdate()
        {
            if (IsInteracting && _interactTime <= 0.0f)
            {
                Destroy(this.gameObject, 0.1f);
            }
        }

        public override bool Interact(GameObject user)
        {
            if (IsInteracting)
                return false;

            IsInteracting = true;
            _interactTime = timeToMine;
            User = user;

            return true;
        }

        public override void CancelInteract()
        {
            IsInteracting = false;
        }
    }
}