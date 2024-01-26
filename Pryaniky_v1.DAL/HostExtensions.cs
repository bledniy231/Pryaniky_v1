using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pryaniky_v1.DAL
{
	public static class HostExtensions
	{
		public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration config)
			=> services.AddDbContext<PryanikyDbContext>(opt =>
			{
				string connectionString = config.GetConnectionString("SqlServerConnectionString") 
					?? throw new ArgumentNullException("Cannot find connection string in configuration");

				opt.UseSqlServer(connectionString);
			});

		public static IServiceProvider MigrateDatabase(this IServiceProvider services)
		{
			using var scope = services.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<PryanikyDbContext>();
			dbContext.Database.Migrate();
			return services;
		}
	}
}
