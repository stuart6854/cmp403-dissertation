using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class FarmerSubTree : SubTree
    {
        private readonly float moveSpeed;
        private readonly float stoppingDist;

        public FarmerSubTree(float moveSpeed, float stoppingDist)
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
                // Farming
                .AddChild(new SequencerNode()
                    // Wheat
                    .AddChild(new Action_FindFarmLandWithCrop("targetObject", "wheat"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_Farm("targetObject", "wheat", 1))
                    // Carrots
                    .AddChild(new Action_FindFarmLandWithCrop("targetObject", "carrot"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_Farm("targetObject", "carrot", 1))
                    // Craft Food
                    .AddChild(new Action_FindGameObjectsWithComponent<CraftingBench>("foundObjects"))
                    .AddChild(new InverterNode(new ListIsEmptyNode("foundObjects")))
                    // TODO: Replace next 2 lines with Action_PickNearest
                    .AddChild(new SortGameObjectListByDistNode("foundObjects", "foundObjects"))
                    .AddChild(new PickListElementNode("foundObjects", 0, "targetObject"))
                    .AddChild(new SequencerNode()
                        .AddChild(new InverterNode(new CheckNullNode("targetObject")))
                        .AddChild(new Action_MoveTo("targetObject"))
                    )
                    .AddChild(new Action_CraftItem("targetObject", "food", 1, new[] {"wheat", "carrot"}, new[] {1, 1}))
                )
            );
            return repeat;
        }
    }
}