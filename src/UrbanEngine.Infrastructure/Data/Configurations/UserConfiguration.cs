using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Data.Configurations
{
	internal class UserConfiguration : EntityBaseConfiguration<UserEntity>
	{
		public override string TableName => "User";

		public override void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			base.Configure(builder);

			builder.Property(e => e.AuthZeroId)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(e => e.FirstName )
				.IsRequired(false)
				.HasMaxLength(50);

			builder.Property(e => e.LastName )
				.IsRequired(false)
				.HasMaxLength( 50 );

			builder.Property( e => e.Email )
				.IsRequired( false )
				.HasMaxLength( 100 );

			builder.Property(e => e.CountryOrRegion)
				.HasMaxLength(100)
				.IsRequired(false);

			builder.Property(e => e.PostalCode)
				.HasMaxLength(25)
				.IsRequired(false);
		}
	}
}
