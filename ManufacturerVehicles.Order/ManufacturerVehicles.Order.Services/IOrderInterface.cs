using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Services
{
	public interface IOrderInterface
	{
		Task<List<GetOrderResponse>> GetOrders(GetOrderRequest request);
		//Task<List<GetOrderResponse>> GetCustomerOrders(int customerId);
		Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
		//Task<bool> UpdateOrderStatus(int orderId, string newStatus);
		//Task<bool> CancelOrder(int orderId);
	}
}