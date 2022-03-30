﻿using UnityEngine;

namespace stuartmillman.dissertation
{
    public class WorldObject : MonoBehaviour
    {
        public bool IsInUse;

        public bool IsInteracting { get; protected set; }

        public bool IsInteractionComplete { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True when interaction complete</returns>
        public virtual bool Interact()
        {
            return false;
        }

        public void ResetInteraction()
        {
            IsInteracting = false;
            IsInteractionComplete = false;
            IsInUse = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void CancelInteract()
        {
        }
    }
}