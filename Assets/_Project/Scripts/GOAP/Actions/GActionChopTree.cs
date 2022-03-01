using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Chop Tree")]
    public class GActionChopTree : GAction
    {
        private Tree _tree;

        public GActionChopTree()
        {
            AddPrecondition("has_axe", true);
            AddEffect("wood_logs", 5);
        }

        public override void Run(GAgent agent)
        {
        }

        public void SetTree(Tree tree)
        {
            _tree = tree;
            AddPrecondition("at_object", _tree);
        }
    }
}