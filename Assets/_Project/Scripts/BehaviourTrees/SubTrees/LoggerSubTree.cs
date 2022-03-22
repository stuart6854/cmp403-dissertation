using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class LoggerSubTree : SubTree
    {
        private readonly float moveSpeed;
        private readonly float stoppingDist;

        public LoggerSubTree(float moveSpeed, float stoppingDist)
        {
            this.moveSpeed = moveSpeed;
            this.stoppingDist = stoppingDist;
        }

        protected override void PreRegisterVariables(Blackboard blackboard)
        {
            blackboard.Set<GameObject>("targetObject");
            blackboard.Set<List<GameObject>>("foundObjects");
        }

        protected override Node BuildTree()
        {
            var repeat = new RepeatNode(new SelectorNode()
                // Put stored item into storage
                .AddChild(new SequencerNode()
                    .AddChild(new Action_InventoryHasItems())
                    .AddChild(new Action_FindGameObjectsWithComponent<Storage>("foundObjects"))
                    .AddChild(new InverterNode(new ListIsEmptyNode("foundObjects")))
                    .AddChild(new SortGameObjectListByDistNode("foundObjects", "foundObjects"))
                    .AddChild(new PickListElementNode("foundObjects", 0, "targetObject"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject", moveSpeed, stoppingDist))
                    )
                    .AddChild(new Action_EmptyInventoryIntoStorage("targetObject"))
                )
                // Find and chop a tree
                .AddChild(new SequencerNode()
                    .AddChild(new Action_FindGameObjectsWithComponent<Tree>("foundObjects"))
                    .AddChild(new InverterNode(new ListIsEmptyNode("foundObjects")))
                    .AddChild(new SortGameObjectListByDistNode("foundObjects", "foundObjects"))
                    .AddChild(new PickListElementNode("foundObjects", 0, "targetObject"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject", moveSpeed, stoppingDist))
                    )
                    .AddChild(new Action_ChopTree("targetObject"))
                )
            );
            return repeat;
        }
    }
}