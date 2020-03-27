using FantasticStay.Domain.Aggregates.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasticStay.Infrastructure.EntityConfigurations
{
	class CalendarEntityTypeConfiguration : IEntityTypeConfiguration<Calendar>
	{
		public void Configure(EntityTypeBuilder<Calendar> entityType)
		{
			entityType.HasKey(o => o.Id);

			entityType.Property(x => x.PropertyId).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction).IsRequired();
			entityType.HasIndex(o => o.PropertyId).IsUnique();

			entityType.Metadata.FindNavigation(nameof(Calendar.Items)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
