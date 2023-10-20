using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Order.Business
{
	public static class OrderHandlers
	{
		public static IServiceCollection CreateOrderHandlersModule(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OrderHandlers).Assembly));
			return serviceCollection;
		}
	}
}