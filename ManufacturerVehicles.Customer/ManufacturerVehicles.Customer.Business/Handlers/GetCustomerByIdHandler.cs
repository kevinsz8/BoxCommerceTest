using AutoMapper;
using ManufacturerVehicles.Customer.Business.Messages.Common;
using ManufacturerVehicles.Customer.Business.Messages.Query.Request;
using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using ManufacturerVehicles.Customer.ServiceClients.Messages.Request;
using ManufacturerVehicles.Customer.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.Business.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdHandlerRequest, GetCustomerByIdHandlerResponse>
    {
        private readonly ICustomerInterface _customerInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCustomerByIdHandler(ICustomerInterface customerInterface, IMapper mapper, ILogger<GetCustomerByIdHandler> logger)
        {
            _customerInterface = customerInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCustomerByIdHandlerResponse> Handle(GetCustomerByIdHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<GetCustomersByIdRequest>(request);
                var customers = await _customerInterface.GetCustomersById(requestI);

                var response = _mapper.Map<GetCustomerByIdHandlerResponse>(customers);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetCustomerByIdHandlerResponse
                {
                    StatusMessage = "Error",
                    ErrorMessage = "An error occurred while processing your request. Please try again later.",
                    Success = false
                };

                return errorResponse;
            }
        }
    }
}