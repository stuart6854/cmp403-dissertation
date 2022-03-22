using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class CrafterSubTree : SubTree
    {
        private readonly float moveSpeed;
        private readonly float stoppingDist;

        public CrafterSubTree(float moveSpeed, float stoppingDist)
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
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_EmptyInventoryIntoStorage("targetObject"))
                )
                // Craft item
                .AddChild(new SequencerNode()
                    // Get crafting requirements
                    .AddChild(new Action_FindStorageWithItem("targetObject", "wood_logs", 5))
                    // .AddChild(new Action_FindGameObjectsWithComponent<CraftingBench>("foundObjects"))
                    // .AddChild(new InverterNode(new ListIsEmptyNode("foundObjects")))
                    // .AddChild(new SortGameObjectListByDistNode("foundObjects", "foundObjects"))
                    // .AddChild(new PickListElementNode("foundObjects", 0, "targetObject"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_StorageTakeItem("targetObject", "wood_logs", 5))
                    // .AddChild(new Action_InventoryHasItem("wood_logs", 5))
                    // Craft
                    .AddChild(new Action_FindGameObjectsWithComponent<CraftingBench>("foundObjects"))
                    .AddChild(new InverterNode(new ListIsEmptyNode("foundObjects")))
                    .AddChild(new SortGameObjectListByDistNode("foundObjects", "foundObjects"))
                    .AddChild(new PickListElementNode("foundObjects", 0, "targetObject"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_CraftItem("targetObject", "wood_chair", 1, new[] {"wood_logs"}, new[] {5}))
                )
            );
            return repeat;
        }
    }
}