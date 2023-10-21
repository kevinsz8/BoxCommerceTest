using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Orchestration.Business
{
	public static class OrchestrationHandlers
	{
		public static IServiceCollection AddOrchestrationHandlersModule(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OrchestrationHandlers).Assembly));
			return serviceCollection;
		}
	}
}