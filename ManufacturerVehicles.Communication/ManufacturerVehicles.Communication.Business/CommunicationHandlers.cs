using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Communication.Business
{
	public static class CommunicationHandlers
	{
		public static IServiceCollection AddCommunicationHandlersModule(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CommunicationHandlers).Assembly));
			return serviceCollection;
		}
	}
}