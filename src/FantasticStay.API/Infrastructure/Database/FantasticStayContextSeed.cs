using System.Linq;
using FantasticStay.Domain.Aggregates.Property;
using FantasticStay.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FantasticStay.API.Infrastructure.Database
{
	public static class FantasticStayContextExtensions
	{
		public static void PrepareDatabase(this IHost host)
		{
			using var scope = host.Services.CreateScope();

			using var context = scope.ServiceProvider.GetService<FantasticStayContext>();

			context.Database.EnsureCreated();
			context.Database.Migrate();

			if (!context.Properties.Any())
			{
				context.Properties.Add(new Property(new Address("Dragan Tsankov 1", "Sofia", "Sofia", "Bulgaria", "1000")));
				context.Properties.Add(new Property(new Address("Dragan Tsankov 2", "Sofia", "Sofia", "Bulgaria", "1000")));
				context.Properties.Add(new Property(new Address("Dragan Tsankov 3", "Sofia", "Sofia", "Bulgaria", "1000")));
			}

			context.SaveChanges();
		}
	}
}
