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
	public class GetCustomerHandler : IRequestHandler<GetCustomerHandlerRequest, GetCustomerHandlerResponse>
	{
		private readonly ICustomerInterface _customerInterface;
		private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCustomerHandler(ICustomerInterface customerInterface, IMapper mapper, ILogger<GetCustomerHandler> logger)
		{
			_customerInterface = customerInterface;
			_mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCustomerHandlerResponse> Handle(GetCustomerHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = new GetCustomerRequest();
                var customers = await _customerInterface.GetCustomers(requestI);

                var response = new GetCustomerHandlerResponse()
                {
                    StatusMessage = "Success",
                    Customers = _mapper.Map<List<Customers>>(customers),
                    Success = true
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetCustomerHandlerResponse
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
