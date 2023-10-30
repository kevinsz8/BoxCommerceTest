using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Command.Request;
using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetOrderHandlerRequest,GetOrderRequest>();
			CreateMap<GetOrderResponse, Orders>();

			CreateMap<CreateOrderHandlerRequest, CreateOrderRequest>();

			CreateMap<AddItemOrderHandlerRequest, AddItemOrderRequest>();

			CreateMap<UpdateStatusOrderHandlerRequest, UpdateOrderStatusRequest>();

			CreateMap<DeleteItemOrderHandlerRequest, DeleteItemOrderRequest>();

            CreateMap<GetOrderItemByOrderIdHandlerRequest, GetOrderItemByOrderIdRequest>();
            CreateMap<GetOrderItemByOrderIdResponse, GetOrderItemByOrderIdHandlerResponse>();

            CreateMap<ConfirmOrderHandlerRequest, ConfirmOrderRequest>();

            CreateMap<AddOrderItemsPendingHandlerRequest, AddOrderItemsPendingRequest>();

            CreateMap<DeleteOrderItemsPendingHandlerRequest, DeleteOrderItemsPendingRequest>();

            CreateMap<CancelOrderHandlerRequest, CancelOrderRequest>();
            CreateMap<CancelOrderResponse, CancelOrderHandlerResponse>();

            CreateMap<GetOrderItemsPendingByOrderIdHandlerRequest, GetOrderItemsPendingByOrderIdRequest>();
            CreateMap<GetOrderItemsPendingByOrderIdResponse, GetOrderItemsPendingByOrderIdHandlerResponse>();

            CreateMap<GetOrderByCustomerIdHandlerRequest, GetOrdersByCustomerIdRequest>();
            CreateMap<GetOrdersByCustomerIdResponse, GetOrderByCustomerIdHandlerResponse>();


        }
    }
}
