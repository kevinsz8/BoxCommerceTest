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
    public class CustomerInterface : ICustomerInterface
    {
        private readonly IMapper _mapper;
        private readonly HttpService _httpService;
        private readonly ServiceConfigOptions _options;


        public CustomerInterface(IMapper mapper, HttpService httpService, IOptions<ServiceConfigOptions> options)
        {
            _mapper = mapper;
            _options = options.Value;
            _httpService = httpService;
        }

        public async Task<GetCustomerResponse> GetCustomers(GetCustomerRequest request)
        {
            var url = _options.GetCustomersEndpoint;
            var response = await _httpService.GetAsync<GetCustomerResponse>(url);
            return response;
        }

    }
}