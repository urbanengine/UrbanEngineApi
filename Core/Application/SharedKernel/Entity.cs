using System;

namespace UrbanEngine.Core.Application.SharedKernel
{
    public interface IEntity
    {
    }

    public interface IEntity<TIdentity> : IEntity
    {
        TIdentity Id { get; }

        bool IsDeleted { get; }

        DateTime? DateCreated { get; }
    }

    /// <summary>
    /// base class for entities
    /// </summary>
    /// <typeparam name="TIdentity"></typeparam>
    public abstract class Entity<TIdentity> : IEntity<TIdentity>
    {
        /// <summary>
        /// uniquely identifies the entity
        /// </summary>
        public TIdentity Id { get; protected set; }

        /// <summary>
        /// used for soft deletes to indicate if item is deleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// date item was created in the database
        /// </summary>
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        protected Entity() { }
    }
}
