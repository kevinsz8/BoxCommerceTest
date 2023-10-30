using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Services
{
	public interface IOrderInterface
	{
		Task<GetOrderResponse> GetOrders(GetOrderRequest request);
		Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
		Task<AddItemOrderResponse> AddItemsOrder(AddItemOrderRequest request);
		Task<DeleteItemOrderResponse> DeleteItemOrder(DeleteItemOrderRequest request);
		Task<UpdateOrderStatusResponse> UpdateOrderStatus(UpdateOrderStatusRequest request);
        Task<GetOrderItemByOrderIdResponse> GetOrderItemsByOrderId(GetOrderItemByOrderIdRequest request);
        Task<ConfirmOrderResponse> ConfirmOrder(ConfirmOrderRequest request);
        Task<AddOrderItemsPendingResponse> AddOrderItemsPending(AddOrderItemsPendingRequest request);
        Task<DeleteOrderItemsPendingResponse> DeleteOrderItemsPending(DeleteOrderItemsPendingRequest request);
		Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request);
        Task<GetOrderItemsPendingByOrderIdResponse> GetOrderItemsPendingByOrderId(GetOrderItemsPendingByOrderIdRequest request);
        Task<GetOrdersByCustomerIdResponse> GetOrdersByCustomerId(GetOrdersByCustomerIdRequest request);
    }
}
