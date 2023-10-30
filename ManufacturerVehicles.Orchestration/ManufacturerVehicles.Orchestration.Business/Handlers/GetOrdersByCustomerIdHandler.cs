using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Handlers
{
    public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdHandlerRequest, GetOrdersByCustomerIdHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetOrdersByCustomerIdHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetCustomerHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrdersByCustomerIdHandlerResponse> Handle(GetOrdersByCustomerIdHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<GetOrdersByCustomerIdRequest>(request);
                var res = await _OrderInterface.GetOrdersByCustomerId(requestI);

                var response = _mapper.Map<GetOrdersByCustomerIdHandlerResponse>(res);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetOrdersByCustomerIdHandlerResponse
                {
                    StatusMessage = "Error",
                    ErrorMessage = ex.Message,
                    Success = false
                };

                return errorResponse;
            }
        }
    }
}
