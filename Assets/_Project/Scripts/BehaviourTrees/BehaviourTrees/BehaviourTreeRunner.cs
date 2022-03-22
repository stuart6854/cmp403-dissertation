using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        private BehaviourTree tree;
        private Blackboard blackboard;

        private BTAgent agent;

        private void Start()
        {
            tree = GetComponent<BehaviourTree>();
            if (tree == null)
            {
                Debug.LogError("No BehaviourTree component to run!", this);
                this.enabled = false;
                return;
            }

            blackboard = new Blackboard();
            tree.InitTree(blackboard);

            agent = GetComponent<BTAgent>();
        }

        private void Update()
        {
            tree.UpdateTree(agent, blackboard);
        }
    }
}