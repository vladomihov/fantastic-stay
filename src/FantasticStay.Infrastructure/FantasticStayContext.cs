using System.Threading;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Aggregates.Property;
using FantasticStay.Domain.SeedWork;
using FantasticStay.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FantasticStay.Infrastructure
{
	public class FantasticStayContext : DbContext, IUnitOfWork
	{
		public DbSet<Property> Properties { get; set; }
		public DbSet<Calendar> Calendars { get; set; }
		public DbSet<CalendarItem> CalendarItems { get; set; }

		public FantasticStayContext(DbContextOptions<FantasticStayContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CalendarEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CalendarItemEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PropertyEntityTypeConfiguration());
		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var result = await base.SaveChangesAsync(cancellationToken);

			return true;
		}

	}
}
