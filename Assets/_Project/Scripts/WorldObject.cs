using UnityEngine;

namespace stuartmillman.dissertation
{
    public class WorldObject : MonoBehaviour
    {
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

        /// <summary>
        /// 
        /// </summary>
        public virtual void CancelInteract()
        {
        }
    }
}