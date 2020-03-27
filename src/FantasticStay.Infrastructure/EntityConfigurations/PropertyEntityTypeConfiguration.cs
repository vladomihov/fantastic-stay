using FantasticStay.Domain.Aggregates.Property;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasticStay.Infrastructure.EntityConfigurations
{
	class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
	{
		public void Configure(EntityTypeBuilder<Property> entityType)
		{
			entityType.HasKey(o => o.Id);

			entityType
				.OwnsOne(o => o.Address, a =>
				{
					a.WithOwner();
				});
		}
	}
}
