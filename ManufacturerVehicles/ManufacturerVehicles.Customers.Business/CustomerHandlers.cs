using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Customers.Business
{
	public static class CustomerHandlers
	{
		public static IServiceCollection AddCustomerHandlersModule(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CustomerHandlers).Assembly));
			return serviceCollection;
		}
	}
}