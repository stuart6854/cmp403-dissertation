using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_FindStorageWithItem : ActionNode
    {
        private readonly string _variableToSaveTo;
        private readonly string _itemName;
        private readonly int _itemAmount;

        public Action_FindStorageWithItem(string variableToSaveTo, string itemName, int itemAmount)
        {
            this._variableToSaveTo = variableToSaveTo;
            this._itemName = itemName;
            this._itemAmount = itemAmount;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            var storages = Object.FindObjectsOfType<Storage>();

            if (storages != null)
            {
                foreach (var storage in storages)
                {
                    if (storage.HasInStorage(_itemName))
                    {
                        if (storage.QueryAmount(_itemName) >= _itemAmount)
                        {
                            blackboard.Set(_variableToSaveTo, storage.gameObject);
                            return NodeState.Success;
                        }
                    }
                }
            }

            return NodeState.Failure;
        }
    }
}