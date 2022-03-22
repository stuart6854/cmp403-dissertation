using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    /*
     * A Composite node is a node which can have one or more children.
     * They are used to handle control flow (for loops, switch statements, etc.).
     */
    public abstract class CompositeNode : Node
    {
        protected List<Node> Children { get; private set; } = new List<Node>();

        public CompositeNode AddChild(Node node)
        {
            Children.Add(node);
            return this;
        }
    }
}