using System.Reflection;
using FantasticStay.API.Infrastructure.Filters;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Aggregates.Property;
using FantasticStay.Infrastructure;
using FantasticStay.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FantasticStay.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRepositories();
			services.AddCustomDbContext(Configuration);
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Test task - Fantastic Stay HTTP API",
					Version = "v1",
					Description = "The Fantastic Stay Service HTTP API"
				});
			});

			services.AddControllers(options =>
				{
					options.Filters.Add(typeof(HttpGlobalExceptionFilter));
				})
				.AddNewtonsoftJson();

			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			var pathBase = Configuration["PATH_BASE"];
			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"/swagger/v1/swagger.json", "FantasticStay.API V1");
				});


			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

	public static class StartupExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<ICalendarRepository, CalendarRepository>();
			services.AddScoped<IPropertyRepository, PropertyRepository>();

			return services;
		}

		public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<FantasticStayContext>(options =>
					{
						options.UseSqlite(configuration.GetConnectionString("FantasticStayContext"),
							sqlOptions =>
							{
								sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
							});
					}
				);

			return services;
		}
	}
}
