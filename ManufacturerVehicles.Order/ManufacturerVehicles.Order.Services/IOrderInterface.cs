using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Services
{
	public interface IOrderInterface
	{
		Task<List<GetOrderResponse>> GetOrders(GetOrderRequest request);
	}
}