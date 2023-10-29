using AutoMapper;
using ManufacturerVehicles.Orchestration.Models;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;
using ManufacturerVehicles.Orchestration.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.ServiceClients
{
	public class OrderInterface : IOrderInterface
	{
		private readonly IMapper _mapper;
		private readonly HttpService _httpService;
		private readonly ServiceConfigOptions _options;


		public OrderInterface(IMapper mapper, HttpService httpService, IOptions<ServiceConfigOptions> options)
		{
			_mapper = mapper;
			_options = options.Value;
			_httpService = httpService;
		}

		public async Task<AddItemOrderResponse> AddItemsOrder(AddItemOrderRequest request)
		{
			var url = _options.AddOrderItemEndpoint;
			return await _httpService.PostAsync<AddItemOrderRequest, AddItemOrderResponse>(url, request);
		}

		public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
		{
			var url = _options.CreateOrderEndpoint;
			return await _httpService.PostAsync<CreateOrderRequest, CreateOrderResponse>(url, request);
		}

		public async Task<DeleteItemOrderResponse> DeleteItemOrder(DeleteItemOrderRequest request)
		{
			var url = _options.DeleteOrderItemEndpoint
			.Replace("{orderId}", request.OrderId.ToString())
			.Replace("{itemId}", request.ItemId.ToString());
			return await _httpService.DeleteAsync<DeleteItemOrderRequest, DeleteItemOrderResponse>(url, request);
		}

        public async Task<GetOrderResponse> GetOrders(GetOrderRequest request)
		{
			var url = _options.GetOrdersEndpoint.Replace("{maxResults}", request.MaxResults.ToString());
			return await _httpService.GetAsync<GetOrderResponse>(url);
		}

		public async Task<UpdateOrderStatusResponse> UpdateOrderStatus(UpdateOrderStatusRequest request)
		{
			var url = _options.UpdateOrderStatusEndpoint;
			return await _httpService.PostAsync<UpdateOrderStatusRequest, UpdateOrderStatusResponse>(url, request);
		}

        public async Task<GetOrderItemByOrderIdResponse> GetOrderItemsByOrderId(GetOrderItemByOrderIdRequest request)
        {
            var url = _options.GetOrderItemByOrderIdEndpoint.Replace("{orderId}", request.OrderId.ToString());
            return await _httpService.GetAsync<GetOrderItemByOrderIdResponse>(url);
        }

        public async Task<ConfirmOrderResponse> ConfirmOrder(ConfirmOrderRequest request)
        {
            var url = _options.ConfirmOrderEndpoint;
            return await _httpService.PostAsync<ConfirmOrderRequest, ConfirmOrderResponse>(url, request);
        }

        public async Task<AddOrderItemsPendingResponse> AddOrderItemsPending(AddOrderItemsPendingRequest request)
        {
            var url = _options.AddOrderItemsPendingEndpoint;
            return await _httpService.PostAsync<AddOrderItemsPendingRequest, AddOrderItemsPendingResponse>(url, request);
        }

        public async Task<DeleteOrderItemsPendingResponse> DeleteOrderItemsPending(DeleteOrderItemsPendingRequest request)
        {
            var url = _options.DeleteOrderItemsPendingEndpoint
            .Replace("{orderId}", request.OrderId.ToString())
            .Replace("{itemId}", request.ItemId.ToString());
            return await _httpService.DeleteAsync<DeleteOrderItemsPendingRequest, DeleteOrderItemsPendingResponse>(url, request);
        }

        public async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request)
        {
            var url = _options.CancelOrderEndpoint;
            return await _httpService.PostAsync<CancelOrderRequest, CancelOrderResponse>(url, request);
        }

        public async Task<GetOrderItemsPendingByOrderIdResponse> GetOrderItemsPendingByOrderId(GetOrderItemsPendingByOrderIdRequest request)
        {
            var url = _options.GetOrderItemsPendingByOrderIdEndpoint.Replace("{orderId}", request.OrderId.ToString());
            return await _httpService.GetAsync<GetOrderItemsPendingByOrderIdResponse>(url);
        }
    }
}