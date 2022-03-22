using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class BaseBT : BehaviourTree
    {
        [SerializeField]
        private Job agentJob;

        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 3.0f;

        [SerializeField]
        private float stoppingDist = 0.8f;

        protected override void PreRegisterVariables(Blackboard blackboard)
        {
            blackboard.Set<Job>("agentJob", agentJob);
        }

        protected override Node BuildTree()
        {
            var repeatNode = new RepeatNode(
                new SelectorNode()
                    .AddChild(new SequencerNode()
                        .AddChild(new ConditionalJobNode("agentJob", Job.Logger))
                        .AddChild(new SubTreeNode(new LoggerSubTree(moveSpeed, stoppingDist))))
                    .AddChild(new SequencerNode()
                        .AddChild(new ConditionalJobNode("agentJob", Job.Miner))
                        .AddChild(new SubTreeNode(new MinerSubTree(moveSpeed, stoppingDist)))
                    )
                    .AddChild(new SequencerNode()
                        .AddChild(new ConditionalJobNode("agentJob", Job.Crafter))
                        .AddChild(new SubTreeNode(new CrafterSubTree(moveSpeed, stoppingDist)))
                    )
                    .AddChild(new SequencerNode()
                        .AddChild(new ConditionalJobNode("agentJob", Job.Farmer))
                        .AddChild(new SubTreeNode(new FarmerSubTree(moveSpeed, stoppingDist)))
                    )
            );


            return repeatNode;
        }
    }
}