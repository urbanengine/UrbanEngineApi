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

			builder.Property(e => e.Bio)
				.IsRequired(false)
				.HasMaxLength(2000);

			builder.Property(e => e.CompanyId)
				.IsRequired(false);

			builder.Property(e => e.CountryOrRegion)
				.HasMaxLength(100)
				.IsRequired(false);

			builder.Property(e => e.PostalCode)
				.HasMaxLength(25)
				.IsRequired(false);
		}
	}
}
