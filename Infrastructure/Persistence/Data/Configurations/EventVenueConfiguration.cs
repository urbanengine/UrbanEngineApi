using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data.Configurations
{
    internal class EventVenueConfiguration : IEntityTypeConfiguration<EventVenue>
    {
        public void Configure(EntityTypeBuilder<EventVenue> builder)
        {
            builder.ToTable("Venue");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Address)
                .HasMaxLength(100);

            builder.Property(e => e.Address2)
                .HasMaxLength(100);

            builder.Property(e => e.City)
                .HasMaxLength(75);

            builder.Property(e => e.State)
                .HasMaxLength(75);

            builder.Property(e => e.PostalCode)
                .HasMaxLength(30);

            builder.Property(e => e.Country)
                .HasMaxLength(75);
             
            builder.Property(p => p.Region)
                .HasConversion(
                    p => p.Value,
                    p => RegionType.FromValue(p));
        }
    }
}
