using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Services
{
	public interface IOrderInterface
	{
		Task<List<GetOrderResponse>> GetOrders(GetOrderRequest request);
		//Task<List<GetOrderResponse>> GetCustomerOrders(int customerId);
		Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
		Task<AddItemOrderResponse> AddItemsOrder(AddItemOrderRequest request);
		Task<bool> DeleteItemOrder(DeleteItemOrderRequest request);
		Task<bool> UpdateOrderStatus(UpdateOrderStatusRequest request);
		//Task<bool> CancelOrder(int orderId);
	}
}