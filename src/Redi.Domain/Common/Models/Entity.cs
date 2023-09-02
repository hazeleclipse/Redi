namespace Redi.Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull, IEquatable<ValueObject>
    {
        public TId Id { get; protected set; } = default!;

        public Entity() { }

        protected Entity(TId id)
         => Id = id;

        public override bool Equals(object? obj)
            => obj is Entity<TId> entity && Equals(entity);

        public bool Equals(Entity<TId>? other)
            => other is not null && this.Id.Equals(other.Id);      

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
            => Equals(left, right);

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
            => ! Equals(left, right);

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
