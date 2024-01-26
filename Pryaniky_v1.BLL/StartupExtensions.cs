using Microsoft.Extensions.DependencyInjection;

namespace Pryaniky_v1.BLL
{
	public static class StartupExtensions
	{
		public static IServiceCollection ConfigureBLL(this IServiceCollection services)
			=> services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(StartupExtensions).Assembly));
	}
}
