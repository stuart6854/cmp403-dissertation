using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Chop Tree")]
    public class GActionChopTree : GAction
    {
        private Tree _tree;

        public override void PrepareForPlanning()
        {
            _tree = null;

            AddPrecondition("has_axe", true);
            AddEffect("has_wood", true);

            SetCost(5.0f);
        }

        protected override bool Run_Internal(GAgent agent)
        {
            if (!_tree.IsInteracting)
            {
                _tree.Interact();
                agent.TriggerChopAnim();
            }
            else
            {
                if (_tree.IsInteractionComplete)
                {
                    agent.Inventory.Add("wood_logs", 5);
                    return true;
                }
            }

            return false;
        }

        public override void OnActionChosen()
        {
            _tree.IsInUse = true;
        }

        public override bool CheckProceduralRequirements(GAgent agent)
        {
            var trees = FindObjectsOfType<Tree>();
            if (trees == null || trees.Length == 0)
            {
                ScenarioManager.Instance.SetNoTrees();
                return false;
            }

            Tree nearestTree = null;
            var dist = float.MaxValue;
            foreach (var tree in trees)
            {
                if (tree.IsInUse)
                    continue;

                var tempDist = Vector3.SqrMagnitude(tree.transform.position - agent.transform.position);
                if (tempDist < dist)
                {
                    nearestTree = tree;
                    dist = tempDist;
                }
            }

            if (nearestTree == null)
                return false;

            _tree = nearestTree;
            SetCost(_tree.TimeToChop);

            SetTargetLocation(_tree.transform.position);
            return true;
        }
    }
}