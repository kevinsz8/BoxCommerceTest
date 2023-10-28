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
    public class CommunicationInterface : ICommunicationInterface
    {
        private readonly IMapper _mapper;
        private readonly HttpService _httpService;
        private readonly ServiceConfigOptions _options;


        public CommunicationInterface(IMapper mapper, HttpService httpService, IOptions<ServiceConfigOptions> options)
        {
            _mapper = mapper;
            _options = options.Value;
            _httpService = httpService;
        }

        public async Task<SendCustomerNotificationResponse> SendCustomerNotification(SendCustomerNotificationRequest request)
        {
            var url = _options.SendCustomerNotificationEndpoint;
            return await _httpService.PostAsync<SendCustomerNotificationRequest, SendCustomerNotificationResponse>(url, request);
        }

    }
}