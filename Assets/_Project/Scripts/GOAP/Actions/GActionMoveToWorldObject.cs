using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Move To (Transform)")]
    public class GActionMoveToWorldObject : GAction
    {
        private WorldObject _object;

        public GActionMoveToWorldObject()
        {
        }

        public override void Run(GAgent agent)
        {
            // TODO: Move agent - Direct, Pathfind, etc.
            agent.MoveTo(_object.transform.position);
        }

        public void SetObject(WorldObject obj)
        {
            _object = obj;
            AddEffect("at_object", _object);
        }
    }
}