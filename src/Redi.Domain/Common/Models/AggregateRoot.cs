namespace Redi.Domain.Common.Models
{
    public class AggregateRoot<TId> : Entity<TId>
        where TId : notnull, IEquatable<ValueObject>
    {
        public AggregateRoot() { }
        protected AggregateRoot(TId id) : base(id) { }      
    }
}
