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

            builder.OwnsOne(p => p.Location);
        }
    }
}
