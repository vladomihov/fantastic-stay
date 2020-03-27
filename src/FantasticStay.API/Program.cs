using System;
using System.IO;
using System.Linq;
using FantasticStay.API.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FantasticStay.API
{
	public class Program
	{
		public static readonly string Namespace = typeof(Program).Namespace;
		public static readonly string AppName = string.Join(".", Namespace.Split(".").TakeLast(2));

		public static int Main(string[] args)
		{
			var configuration = GetConfiguration();

			Log.Logger = CreateSerilogLogger(configuration);

			try
			{
				Log.Information("Configuring web host ({ApplicationContext})...", AppName);
				var host = BuildHost(args, configuration);

				Log.Information("Applying migrations ({ApplicationContext})...", AppName);
				host.PrepareDatabase();

				Log.Information("Starting web host ({ApplicationContext})...", AppName);
				host.Run();

				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static IConfiguration GetConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			return builder.Build();
		}

		private static ILogger CreateSerilogLogger(IConfiguration configuration)
		{
			return new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.Enrich.WithProperty("ApplicationContext", AppName)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		public static IHost BuildHost(string[] args, IConfiguration configuration)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.UseStartup<Startup>()
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseConfiguration(configuration)
						.UseSerilog();
				})
				.Build();
		}
	}
}
