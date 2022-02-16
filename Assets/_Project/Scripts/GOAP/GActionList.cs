using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Action List", order = 0)]
    public class GActionList : ScriptableObject
    {
        [SerializeField] private GAction[] actions;

        public GAction[] Actions => actions;

        /// <summary>
        /// Clones this GActionList and all its GActions.
        /// </summary>
        /// <returns></returns>
        public GActionList Clone()
        {
            // Clone GActionList
            var actionList = Instantiate(this);

            // Clone each GAction
            for (int i = 0; i < actionList.Actions.Length; i++)
            {
                actionList.Actions[i] = Instantiate(actionList.Actions[i]);
            }

            return actionList;
        }
    }
}