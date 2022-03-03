using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Move To (Transform)")]
    public class GActionMoveToTransform : GAction
    {
        public GActionMoveToTransform()
        {
            AddPrecondition("hasMoveTarget_Transform", true);
            AddEffect("hasMoveTarget_Transform", false);
        }

        public override bool Run(GAgent agent)
        {
            if (agent.State.Get("moveTarget_Transform", out var value))
            {
                var moveTarget = (Transform) value;
                // TODO: Move agent - Direct, Pathfind, etc.
            }

            return false;
        }
    }
}