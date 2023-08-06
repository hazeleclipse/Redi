using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate
{
    public class CoreNode : Node
    {
        private readonly HashSet<Node> _children = new();

        public CoreNode() { }

        private CoreNode(NodeId id, string name, CoreNode? parent) : base(id, name, parent) { }

        private CoreNode(NodeId id, string name, CoreNode? parent, Weight weight, Stake localStake, Stake stake) 
            : base(id, name, parent, weight, localStake, stake) { }

        public IReadOnlyCollection<Node> Children => _children;

        public static CoreNode Create(NodeId id, string name, CoreNode? parent)
            => new(id, name, parent);

        public static CoreNode CreateRoot(NodeId id, string name)
            => new(id, name, null, 1, 1, 1);

        public void AddChild(Node child)
        {
            /* Checks to make sure that the node is
             * not already a child, is not the current node
             * and does put the heirarchy is an invalid state by
             * by creating a cycle or adding root as a child
             */
            if (_children.Contains(child))
                throw new Exception($"{child.Id} is already a child of {this.Id}");

            if (this.Equals(child))
                throw new Exception("Node cannot contain itself");

            if (child is CoreNode coreNodeChild)
            {
                if (coreNodeChild.IsRoot())
                    throw new Exception("The root node cannot become a child");

                if (this.HasDirectAncestorInDescendantsOf(coreNodeChild))
                    throw new Exception("Child cannot have a descendant that is a direct ancestor of the parent node.");
            }


            child.Parent?.RemoveChild(child);
            child.Parent = this;
            _children.Add(child);

            UpdateDescendantStakes();
        }

        public bool IsRoot()
            => this.Stake == 1m && this.LocalStake == 1m;        

        public void RemoveChild(Node child)
        {
            // Ensure container is acutally a child
            if (!_children.Contains(child))
                throw new Exception("The provided node is not listed as a child of the current node.");

            if (child.Parent is null || child.Parent != this)
                throw new Exception("The provided node does not list this node as its parent.");

            /* Reset child values to default so that there 
             * remains only 1 active contianer heirarchy
             */
            child.Parent = null;
            child.Stake = 0;
            child.LocalStake = 0;

            _children.Remove(child);

            UpdateDescendantStakes();
            if (child is CoreNode coreNode)
                coreNode.UpdateDescendantStakes();
        }

        public void UpdateChildWeight(Node child, Weight weight)
        {
            if (child.Parent is null || !child.Parent.Equals(this) || !_children.Contains(child))
                throw new Exception($"{child.Name} is not a valid child of {this.Name}");

            child.Weight = weight;

            UpdateDescendantStakes();
        }

        private bool HasDirectAncestorInDescendantsOf(CoreNode node)
        {
            // Gather all direct ancestors
            HashSet<Node> directAncestors = new HashSet<Node>();
            var currentAncestor = this;

            do
            {
                directAncestors.Add(currentAncestor);
                currentAncestor = currentAncestor.Parent;
            } while (currentAncestor is not null);

            // Check against all descendants skipping non core nodes
            Queue<CoreNode> currentDescendantQueue = new();
            currentDescendantQueue.Enqueue(node);

            while (currentDescendantQueue.Count > 0)
            {
                var currentDescendant = currentDescendantQueue.Dequeue();
                foreach (var descendant in currentDescendant.Children)
                {                       
                    if (descendant is CoreNode coreNodeDescendant)
                    {
                        if (directAncestors.Contains(coreNodeDescendant))
                            return true;

                        currentDescendantQueue.Enqueue(coreNodeDescendant);
                    }
                }
            }
            return false;
        }

        private void UpdateDescendantStakes()
        {
            uint totalWeight;
            Queue<CoreNode> currentCoreNodeDescendantQueue = new();
            currentCoreNodeDescendantQueue.Enqueue(this);

            // Update stakes through out descendant tree
            while (currentCoreNodeDescendantQueue.Count > 0)
            {
                var currentDescendant = currentCoreNodeDescendantQueue.Dequeue();

                // Calculate total weight for current container
                totalWeight = 0;
                foreach (var child in currentDescendant.Children)
                    totalWeight += child.Weight;

                // Update stakes for current containers children
                foreach (var descendant in currentDescendant.Children)
                {
                    descendant.LocalStake = (decimal)descendant.Weight / totalWeight;

                    if (descendant.Parent is null)
                        descendant.Stake = 0;

                    // Parent should not be null unless a bug caused it not to be properly set
                    descendant.Stake = descendant.Parent is null 
                        ? 0 
                        : descendant.LocalStake * descendant.Parent.Stake;

                    if (descendant is CoreNode coreNodeDescendant)
                        currentCoreNodeDescendantQueue.Enqueue(coreNodeDescendant);
                }
            }
        }
    }
}
