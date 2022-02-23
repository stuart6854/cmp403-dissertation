using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Move To (Vector)")]
    public class GActionMoveToVector : GAction
    {
        public GActionMoveToVector()
        {
            AddPrecondition("hasMoveTarget_Vector", true);
            AddEffect("hasMoveTarget_Vector", false);
        }

        public override void Run(GAgent agent)
        {
            if (agent.State.Get("moveTarget_Vector", out var value))
            {
                var moveTarget = (Vector3) value;
                // TODO: Move agent - Direct, Pathfind, etc.
            }
        }
    }
}