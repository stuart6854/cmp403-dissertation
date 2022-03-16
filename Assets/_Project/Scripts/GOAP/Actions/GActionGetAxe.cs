using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Get Axe")]
    public class GActionGetAxe : GAction
    {
        public override void PrepareForPlanning()
        {
            AddPrecondition("axeAvailable", true);
            AddPrecondition("axe", 0);
            AddEffect("axe", 1);
            
            SetCost(1.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            return false;
        }
    }
}