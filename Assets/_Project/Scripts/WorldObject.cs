using UnityEngine;

namespace stuartmillman.dissertation
{
    public class WorldObject : MonoBehaviour
    {
        public bool IsInUse;

        public bool IsInteracting { get; protected set; }

        public bool IsInteractionComplete { get; protected set; }

        public GameObject User { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True when interaction complete</returns>
        public virtual bool Interact(GameObject user)
        {
            User = user;
            return false;
        }

        public void ResetInteraction()
        {
            User = null;
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