using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Chop Tree")]
    public class GActionChopTree : GAction
    {
        public GActionChopTree()
        {
            AddPrecondition("axe", 1);
            AddEffect("logs", 5);
        }

        public override void Run(GAgent agent)
        {
        }
    }
}