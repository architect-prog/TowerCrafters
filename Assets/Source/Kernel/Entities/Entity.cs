using System;
using Source.Kernel.Interfaces;

namespace Source.Kernel.Entities
{
    public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
    {
        public abstract TId Id { get; }

        public static bool operator ==(Entity<TId> first, Entity<TId> second)
        {
            if (first is null && second is null)
                return true;

            if (first is null || second is null)
                return false;

            var result = first.Equals(second);
            return result;
        }

        public static bool operator !=(Entity<TId> first, Entity<TId> second)
        {
            var result = !(first == second);
            return result;
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            var result = Id?.Equals(other.Id) ?? false;
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;
            var result = Equals(other);

            return result;
        }

        public override int GetHashCode()
        {
            var result = HashCode.Combine(Id);
            return result;
        }
    }
}