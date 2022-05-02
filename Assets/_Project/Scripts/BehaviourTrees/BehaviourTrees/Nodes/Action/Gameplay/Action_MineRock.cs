using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class Action_MineRock : ActionNode
    {
        private readonly string variableName;

        private Rock _rock;

        public Action_MineRock(string variableName)
        {
            this.variableName = variableName;
        }

        protected override void OnStart(BTAgent agent, Blackboard blackboard)
        {
            var rockObject = blackboard.Get<GameObject>(variableName);
            _rock = rockObject.GetComponent<Rock>();
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate(BTAgent agent, Blackboard blackboard)
        {
            if (!_rock.IsInteracting)
            {
                _rock.Interact(agent.gameObject);
                agent.TriggerChopAnim();
            }
            else
            {
                if (_rock.IsInteractionComplete && _rock.User == agent.gameObject)
                {
                    agent.Inventory.Add("stones", 5);
                    return NodeState.Success;
                }
            }

            return NodeState.Running;
        }
    }
}