using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
    internal class CheckInConfiguration : EntityBaseConfiguration<CheckInEntity>
    {
        public override string TableName => "CheckIn";

        public override void Configure( EntityTypeBuilder<CheckInEntity> builder )
        {
            base.Configure( builder );

            builder.Property( o => o.UserId ).IsRequired( true );
            builder.Property( o => o.EventId ).IsRequired( false );
            builder.Property( o => o.CheckedInAt ).IsRequired( true );

            builder.HasOne( o => o.Event )
                .WithMany( o => o.CheckIns )
                .HasForeignKey( o => o.EventId );
        }
    }
}
