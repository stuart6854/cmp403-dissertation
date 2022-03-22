namespace stuartmillman.dissertation.bt
{
    /*
     * A Decorator node is a node that can only have one child.
     * Their function is to:
     * - Transform their child's result
     * - Terminate their child's execution
     * - Repeat processing of the child
     */
    public abstract class DecoratorNode : Node
    {
        protected Node Child { get; private set; }

        protected DecoratorNode(Node child)
        {
            Child = child;
        }
    }
}