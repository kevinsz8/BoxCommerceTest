﻿using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Orchestration.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetItemResponse, GetItemHandlerResponse>();
			CreateMap<GetOrderResponse, GetOrderHandlerResponse>();

			CreateMap<CreateOrderHandlerRequest, CreateOrderRequest>();
			CreateMap<CreateOrderResponse, CreateOrderHandlerResponse>();

			CreateMap<AddItemOrderHandlerRequest, AddItemOrderRequest>();
			CreateMap<AddItemOrderResponse, AddItemOrderHandlerResponse>();

			CreateMap<UpdateStatusOrderHandlerRequest, UpdateOrderStatusRequest>();
			CreateMap<UpdateOrderStatusResponse, UpdateStatusOrderHandlerResponse>();

			CreateMap<DeleteItemOrderHandlerRequest, DeleteItemOrderRequest>();
			CreateMap<DeleteItemOrderResponse, DeleteItemOrderHandlerResponse>();

            CreateMap<GetCustomerResponse, GetCustomerHandlerResponse>();

            CreateMap<GetOrderItemByOrderIdHandlerRequest, GetOrderItemByOrderIdRequest>();
            CreateMap<GetOrderItemByOrderIdResponse, GetOrderItemByOrderIdHandlerResponse>();

            CreateMap<ConfirmOrderHandlerRequest, ConfirmOrderRequest>();
            CreateMap<ConfirmOrderResponse, ConfirmOrderHandlerResponse>();

            CreateMap<CancelOrderHandlerRequest, CancelOrderRequest>();
            CreateMap<CancelOrderResponse, CancelOrderHandlerResponse>();

            CreateMap<GetCustomerByIdHandlerRequest, GetCustomersByIdRequest>();
            CreateMap<GetCustomersByIdResponse, GetCustomerByIdHandlerResponse>();

            CreateMap<GetOrdersByCustomerIdHandlerRequest, GetOrdersByCustomerIdRequest>();
            CreateMap<GetOrdersByCustomerIdResponse, GetOrdersByCustomerIdHandlerResponse>();
        }
	}
}
