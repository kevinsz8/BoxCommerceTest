using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Services
{
	public interface IOrderInterface
	{
		Task<List<GetOrderResponse>> GetOrders(GetOrderRequest request);
		Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
		Task<AddItemOrderResponse> AddItemsOrder(AddItemOrderRequest request);
		Task<DeleteItemOrderResponse> DeleteItemOrder(DeleteItemOrderRequest request);
		Task<bool> UpdateOrderStatus(UpdateOrderStatusRequest request);

        Task<GetOrderItemByOrderIdResponse> GetOrderItemsByOrderId(GetOrderItemByOrderIdRequest request);
        Task<ConfirmOrderResponse> ConfirmOrder(ConfirmOrderRequest request );
        Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request);
        Task<AddOrderItemsPendingResponse> AddOrderItemsPending(AddOrderItemsPendingRequest request);
        Task<DeleteOrderItemsPendingResponse> DeleteOrderItemsPending(DeleteOrderItemsPendingRequest request);
    }
}