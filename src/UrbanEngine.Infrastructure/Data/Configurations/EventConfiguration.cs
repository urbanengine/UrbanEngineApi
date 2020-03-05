using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
    internal class EventConfiguration : EntityBaseConfiguration<EventEntity>
    {
        public override string TableName => "Event";

        public override void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            base.Configure(builder); 

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(e => e.EndDate).IsRequired(false);
            builder.Property(e => e.StartDate).IsRequired(false);
            builder.Property(e => e.OrganizerId).IsRequired(false);
			builder.Property(e => e.RoomId).IsRequired(false);

            builder.Ignore(e => e.Duration);

            builder.Property(p => p.EventType)
                .HasConversion(
                    p => p.Value,
                    p => EventType.FromValue(p))
                .IsRequired();
            
			builder.HasOne(e => e.Room)
				.WithMany(e => e.Events)
				.HasForeignKey(e => e.RoomId);
        }
    }
}
