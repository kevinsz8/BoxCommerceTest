using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Handlers
{
    public class GetOrderByCustomerIdHandler : IRequestHandler<GetOrderByCustomerIdHandlerRequest, GetOrderByCustomerIdHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetOrderByCustomerIdHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderByCustomerIdHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrderByCustomerIdHandlerResponse> Handle(GetOrderByCustomerIdHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var requestI = _mapper.Map<GetOrdersByCustomerIdRequest>(request);
                var ordersResponse = await _OrderInterface.GetOrdersByCustomerId(requestI);

                var response = _mapper.Map<GetOrderByCustomerIdHandlerResponse>(ordersResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetOrderByCustomerIdHandlerResponse
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
