using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Customer.Business
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