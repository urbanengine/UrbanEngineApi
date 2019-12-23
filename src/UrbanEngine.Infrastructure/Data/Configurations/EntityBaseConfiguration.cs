using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
    internal abstract class EntityBaseConfiguration<TEntity> : 
        IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
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
