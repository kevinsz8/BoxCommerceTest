using Microsoft.Extensions.DependencyInjection;

namespace ManufacturerVehicles.Item.Business
{
	public static class ItemHandlers
	{
		public static IServiceCollection AddItemHandlersModule(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ItemHandlers).Assembly));
			return serviceCollection;
		}
	}
}