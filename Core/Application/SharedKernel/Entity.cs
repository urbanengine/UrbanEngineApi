namespace UrbanEngine.Core.Application.SharedKernel
{
    public abstract class Entity<TIdentity>
    {

        public TIdentity Id { get; protected set; }

        protected Entity() { }
    }
}
