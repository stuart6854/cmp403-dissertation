namespace stuartmillman.dissertation.bt
{
    public class ConditionalJobNode : ConditionalNode<Job>
    {
        public ConditionalJobNode(string variableName, Job requiredValue) : base(variableName, requiredValue)
        {
        }

        protected override bool ConditionMet(Blackboard blackboard)
        {
            var variableValue = blackboard.Get<Job>(variableName);
            return variableValue == requiredValue;
        }
    }
}