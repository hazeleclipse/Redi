using Redi.Domain.Aggregates.NodeAggregate.Entities;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate
{
    public class ByWeightNode : Node
    {
        readonly List<StakerWeightEntry> _weights = new();
        public ByWeightNode() { }

        private ByWeightNode(NodeId id, string name, CoreNode? parent) : base(id, name, parent) { }

        public static ByWeightNode Create(NodeId id, string name, CoreNode? parent)
            => new(id, name, parent);

        public IReadOnlyCollection<StakerWeightEntry> Weights => _weights;

        public void AddEntry(Staker staker)
        {
            if (_weights.Where(e => e.StakerId == staker.Id).Any())
                return;

            var entry = new StakerWeightEntry(Guid.NewGuid(),this.Id ,staker.Id);

            _weights.Add(entry);
            UpdateStakes();
        }

        public void RemoveEntry(Staker staker)
        {
            var entry = _weights.Where(e => e.Id == staker.Id).FirstOrDefault();
            
            if (entry is null) return;
            
            _weights.Remove(entry);
            UpdateStakes();
        }

        public void UpdateEntry(Staker staker, Weight newWeight)
        {
            var entry = _weights.Where(e => e.StakerId == staker.Id).FirstOrDefault();

            if (entry is null) return;

            entry.Weight = newWeight;
            UpdateStakes();
        }

        private void UpdateStakes()
        {
            // Get total
            long total = 0;
            foreach (var entry in _weights)
                total += entry.Weight;

            // Process each entry using total
            foreach (var entry in _weights)
            {
                entry.LocalStake = (decimal)entry.Weight / total;
                entry.Stake = entry.LocalStake * this.Stake;
            }                
        }
    }
}
