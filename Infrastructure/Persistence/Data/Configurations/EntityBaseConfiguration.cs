using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Infrastructure.Persistence.Data.Configurations
{
    internal abstract class EntityBaseConfiguration<TEntity, TIdentity> : 
        IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity<TIdentity>
    {
        public abstract string TableName { get; }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.DateCreated)
                .IsRequired();
        }
    }
}
