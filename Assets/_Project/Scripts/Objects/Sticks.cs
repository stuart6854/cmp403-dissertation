using System;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Sticks : WorldObject
    {
        [SerializeField] private float timeToCollect;
        private float _interactTime = 0.0f;

        public float TimeToCollect => timeToCollect;

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

        public override bool Interact(GameObject user)
        {
            if (IsInteracting)
                return false;

            IsInteracting = true;
            _interactTime = timeToCollect;
            User = user;

            return true;
        }

        public override void CancelInteract()
        {
            IsInteracting = false;
        }
    }
}