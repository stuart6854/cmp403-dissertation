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

        public override bool Run(GAgent agent)
        {
            if (!_tree.IsInteracting)
            {
                _tree.Interact();
            }
            else
            {
                if (_tree.IsInteractionComplete)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetTree(Tree tree)
        {
            _tree = tree;
            SetCost(_tree.TimeToChop);
            AddPrecondition("at_object", _tree);
        }
    }
}