using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;

namespace UrbanEngine.Infrastructure.Persistence.Data.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired(false);
            builder.Property(e => e.EndDate).IsRequired(false);
            builder.Property(e => e.StartDate).IsRequired(false);
            builder.Property(e => e.OrganizerId).IsRequired(false);
            builder.Property(p => p.EventType)
                .HasConversion(
                    p => p.Value,
                    p => EventType.FromValue(p))
                .IsRequired();
        }
    }
}
