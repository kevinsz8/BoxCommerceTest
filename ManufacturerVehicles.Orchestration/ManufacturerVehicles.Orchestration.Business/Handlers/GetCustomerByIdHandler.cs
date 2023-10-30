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
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdHandlerRequest, GetCustomerByIdHandlerResponse>
    {
        private readonly ICustomerInterface _CustomerInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCustomerByIdHandler(ICustomerInterface CustomerInterface, IMapper mapper, ILogger<GetCustomerHandler> logger)
        {
            _CustomerInterface = CustomerInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetCustomerByIdHandlerResponse> Handle(GetCustomerByIdHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<GetCustomersByIdRequest>(request);
                var res = await _CustomerInterface.GetCustomersById(requestI);

                var response = _mapper.Map<GetCustomerByIdHandlerResponse>(res);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetCustomerByIdHandlerResponse
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
