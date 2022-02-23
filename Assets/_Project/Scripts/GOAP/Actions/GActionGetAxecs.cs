using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Get Axe")]
    public class GActionGetAxe : GAction
    {
        public GActionGetAxe()
        {
            AddPrecondition("axeAvailable", true);
            AddPrecondition("axe", 0);
            AddEffect("axe", 1);
        }

        public override void Run(GAgent agent)
        {
        }
    }
}