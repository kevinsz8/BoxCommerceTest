using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Command.Request;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
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
		}
	}
}
