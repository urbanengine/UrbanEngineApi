using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
	internal class RoomConfiguration : EntityBaseConfiguration<RoomEntity>
	{
		public override string TableName => "Room";

		public override void Configure(EntityTypeBuilder<RoomEntity> builder)
		{
			base.Configure(builder);

			builder.Property(e => e.Name)
				.IsRequired(true)
				.HasMaxLength(100);

			builder.Property(e => e.Description)
				.IsRequired(false)
				.HasMaxLength(500);

			builder.Property(e => e.Resources)
				.IsRequired(false)
				.HasMaxLength(500);

			builder.Property(e => e.VenueId).IsRequired(false);
			builder.Property(e => e.HasTVOrProjector).IsRequired(false);
			builder.Property(e => e.Capacity).IsRequired(false);

			builder.HasOne(e => e.Venue)
				.WithMany(e => e.Rooms)
				.HasForeignKey(e => e.VenueId);
		}
	}
}
