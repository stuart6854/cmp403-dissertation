using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Move To (Transform)")]
    public class GActionMoveToWorldObject : GAction
    {
        private WorldObject _object;

        public override bool Run(GAgent agent)
        {
            agent.MoveTo(_object.transform.position);
            return agent.AtDestination();
        }

        public void SetObject(WorldObject obj)
        {
            _object = obj;
            AddEffect("at_object", _object);
        }
    }
}