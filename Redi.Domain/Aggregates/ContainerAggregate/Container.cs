using Redi.Domain.Aggregates.ContainerAggregate.Entities;
using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.ContainerAggregate;

public class Container : AggregateRoot<ContainerId>
{
    private readonly List<StakerMembership> _stakerMemberships = new();
    private readonly List<Container> _childContainers = new();

    public string Name { get; private set; } = default!;
    public Container? Parent { get; private set; }
    public Stake Stake { get; private set; } = 0;
    public Stake LocalStake { get; private set; } = 0;
    public Weight Weight { get; private set; } = 1;

    public Container() { }

    private Container(ContainerId id, string name, Container? parent) : base(id)
    {
        Name = name;
        Parent = parent;
    }

    private Container(ContainerId id, string name, Container? parent, 
        Stake stake, Stake localStake, Weight weight) : this(id, name, parent) 
    {
        Stake = stake;
        LocalStake = localStake;
        Weight = weight;
    }

    public IReadOnlyList<StakerMembership> Memberships => _stakerMemberships.AsReadOnly();

    public IReadOnlyList<Container> ChildContainers => _childContainers.AsReadOnly();

    public static Container Create(ContainerId id, string name)
        => new(id, name, null);

    public static Container CreateRoot(ContainerId id, string name)
        => new(id, name, null, 1, 1, 1);

    public void AddChildContainer(Container child)
    {
        /* Checks to make sure that the item is
         * not already a child, is not the current container
         * and does put the heirarchy is an invalid state by
         * by creating a cycle or adding root as a child
         */
        if (_childContainers.Contains(child))
            throw new Exception($"{child.Name} is already a child of {this.Name}");
        if (this.Equals(child))
            throw new Exception("Container cannot contain itself");
        if (child.IsRoot())
            throw new Exception("The root container cannot become a child");
        if(this.HasDirectAncestorInDescendantsOf(child))
            throw new Exception("Child cannot have a descendant that is a direct ancestor of the parent container.");

        child.Parent?.RemoveChildContainer(child);
        child.Parent = this;
        _childContainers.Add(child);

        UpdateDescendantStakes();
    }

    public void AddChildStaker(Staker child)
    {
        if (_stakerMemberships.Where(sm => sm.StakerId == child.Id).Any())
            throw new Exception($"{child.FirstName} {child.LastName} is already a member of {this.Name}");

        var membership = new StakerMembership(Guid.NewGuid(), this.Id, child.Id);
        _stakerMemberships.Add(membership);

        UpdateDescendantStakes();
    }

    public void Edit(string name)
    {
        Name = name;
    }

    public bool IsRoot()
    {
        return this.Stake == 1m && this.LocalStake == 1m;
    }

    public void RemoveChildContainer(Container child)
    {

        // Ensure container is acutally a child
        if (!_childContainers.Contains(child))
            throw new Exception("The provided container is not listed as a child of the current container.");
        if ( child.Parent is null || child.Parent != this)
            throw new Exception("The provided container does not list this container as its parent.");

        /* Reset child values to default so that there 
         * remains only 1 active contianer heirarchy
         */
        child.Parent = null;
        child.Stake = 0;
        child.LocalStake = 0;

        _childContainers.Remove(child);

        UpdateDescendantStakes();
        child.UpdateDescendantStakes();
    }

    public void RemoveChildStaker(Staker staker)
    {
        var membership = _stakerMemberships.Where(sm => sm.StakerId ==  staker.Id).FirstOrDefault()
            ?? throw new Exception("The provided staker does not have a membership in this container");

        _stakerMemberships.Remove(membership);

        UpdateDescendantStakes();
    }

    public void UpdateChildContainerWeight(Container child, Weight weight)
    {
        if (child.Parent is null || !child.Parent.Equals(this))
            throw new Exception($"{child.Name} is not a valid child of {this.Name}");

        var cc = _childContainers.Find(c => c == child)
            ?? throw new Exception("Child container not found");

        cc.Weight = weight;

        UpdateDescendantStakes();
    }

    public void UpdateChildStakerWeight(Staker staker, Weight weight)
    {
        var sm = _stakerMemberships.Find(sm => sm.StakerId == staker.Id)
            ?? throw new Exception("Member staker not found");

        sm.Weight = weight;

        UpdateDescendantStakes();
    }

    private bool HasDirectAncestorInDescendantsOf(Container container)
    {

        List<Container> directAncestors = new();
        var currentAncestor = this;

        do
        {
            directAncestors.Add(currentAncestor);
            currentAncestor = currentAncestor.Parent;
        } while (currentAncestor is not null);

        Queue<Container> currentDescendantQueue = new();
        currentDescendantQueue.Enqueue(container);

        while (currentDescendantQueue.Count > 0)
        {
            var currentDescendant = currentDescendantQueue.Dequeue();
            foreach (var decendant in currentDescendant.ChildContainers)
            {
                if (directAncestors.Any(a => a.Equals(decendant)))
                    return true;

                currentDescendantQueue.Enqueue(decendant);
            }
        }
        return false;
    }

    private void UpdateDescendantStakes()
    {
        uint totalWeight;
        Queue<Container> currentDescendantQueue = new();
        currentDescendantQueue.Enqueue(this);

        // Update stakes through out descendant tree
        while (currentDescendantQueue.Count > 0)
        {
            var currentDescendant = currentDescendantQueue.Dequeue();

            // Calculate total weight for current container
            totalWeight = 0;
            foreach (var childContainer in currentDescendant.ChildContainers)
                totalWeight += childContainer.Weight;
            foreach (var childStaker in currentDescendant.Memberships)
                totalWeight += childStaker.Weight;

            // Update stakes for current containers children
            foreach (var descendant in currentDescendant.ChildContainers)
            {
                currentDescendantQueue.Enqueue(descendant);
                descendant.LocalStake = (decimal)descendant.Weight / totalWeight;

                if (descendant.Parent is null)
                    throw new Exception($"Parent for container {descendant.Name} was not properly set or unset");

                descendant.Stake = descendant.LocalStake * descendant.Parent.Stake;
            }
            foreach (var member in currentDescendant.Memberships)
            {
                member.LocalStake = (decimal)member.Weight / totalWeight;
                member.Stake = member.LocalStake * currentDescendant.Stake;
            }
        }
    }

    private void UpdateDescendantStakesRecursively()
    {
        // Calculate total weight contained by parent
        uint totalWeight = 0;
        foreach (var childContainer in ChildContainers)
            totalWeight += childContainer.Weight;
        foreach (var childStaker in Memberships)
            totalWeight += childStaker.Weight;

        // Update stakes through out descendant tree
        foreach (var descendant in ChildContainers)
        {
            descendant.LocalStake = (decimal)descendant.Weight / totalWeight;
            descendant.Stake = descendant.LocalStake * Stake;
            descendant.UpdateDescendantStakesRecursively();
        }
        foreach (var member in Memberships)
        {
            member.LocalStake = (decimal)member.Weight / totalWeight;
            member.Stake = member.LocalStake * Stake;
        }
    }

    public Dictionary<StakerId, Stake> GetStakerTotalStakes()
    {
        if (this.Stake != 1 && this.LocalStake != 1)
            throw new Exception("This is not the root container");

        Dictionary<StakerId, Stake> stakerTotalStakes = new();

        Queue<Container> containerQueue = new();
        containerQueue.Enqueue(this);

        Container currentContainer;

        while (containerQueue.Count > 0)
        {
            currentContainer = containerQueue.Dequeue();

            foreach (var membership in currentContainer.Memberships)
            {
                if (!stakerTotalStakes.ContainsKey(membership.StakerId))
                    stakerTotalStakes.Add(membership.StakerId, membership.Stake);
                else
                {
                    stakerTotalStakes[membership.StakerId] += membership.Stake;
                }
            }

            foreach (var container in currentContainer.ChildContainers)
                containerQueue.Enqueue(container);
        }

        return stakerTotalStakes;
    }
}