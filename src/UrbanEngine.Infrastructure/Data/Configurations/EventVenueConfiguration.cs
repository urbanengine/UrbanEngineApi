using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
    internal class EventVenueConfiguration : EntityBaseConfiguration<EventVenueEntity>
    {
        public override string TableName => "Venue";

        public override void Configure(EntityTypeBuilder<EventVenueEntity> builder)
        {
            base.Configure(builder);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(e => e.Name)
                .IsUnique();
            
            builder.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.Address2)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(75);

            builder.Property(e => e.State)
                .IsRequired(false)
                .HasMaxLength(75);

            builder.Property(e => e.PostalCode)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.Property(e => e.Country)
                .IsRequired(false)
                .HasMaxLength(75);
             
            builder.Property(p => p.Region)
                .IsRequired(false)
                .HasConversion(
                    p => p.Value,
                    p => RegionType.FromValue(p));
        }
    }
}
