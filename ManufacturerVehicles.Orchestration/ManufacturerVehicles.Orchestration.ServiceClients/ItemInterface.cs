using AutoMapper;
using ManufacturerVehicles.Orchestration.Models;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;
using ManufacturerVehicles.Orchestration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ManufacturerVehicles.Orchestration.ServiceClients
{
	public class ItemInterface : IItemInterface
	{
		private readonly IMapper _mapper;
		private readonly HttpService _httpService;
		private readonly ServiceConfigOptions _options;


		public ItemInterface(IMapper mapper, HttpService httpService, IOptions<ServiceConfigOptions> options)
		{
			_mapper = mapper;
			_options = options.Value;
			_httpService = httpService;
		}

		public async Task<GetItemResponse> GetItems(GetItemRequest request)
		{
			var url = _options.GetItemsEndpoint;
			var response = await _httpService.GetAsync<GetItemResponse>(url);
			return response;
		}

        public async Task<ModifyStockItemResponse> ModifyStockItem(ModifyStockItemRequest request)
        {
            var url = _options.ModifyStockItemsEndpoint;
            return await _httpService.PostAsync<ModifyStockItemRequest, ModifyStockItemResponse>(url, request);
        }
    }
}