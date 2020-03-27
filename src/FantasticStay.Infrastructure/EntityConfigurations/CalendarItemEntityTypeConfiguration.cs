using FantasticStay.Domain.Aggregates.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasticStay.Infrastructure.EntityConfigurations
{
	class CalendarItemEntityTypeConfiguration : IEntityTypeConfiguration<CalendarItem>
	{
		public void Configure(EntityTypeBuilder<CalendarItem> entityType)
		{
			entityType.HasKey(o => o.Id);

			entityType.Property(o => o.From).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
			entityType.Property(o => o.To).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
			entityType.Property(o => o.StateId).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
			entityType.Property(o => o.StatePayload).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
			entityType.Property(o => o.CreatedOn).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
		}
	}
}
