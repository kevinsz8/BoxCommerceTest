﻿using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
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
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderHandlerRequest, ConfirmOrderHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ConfirmOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<ConfirmOrderHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ConfirmOrderHandlerResponse> Handle(ConfirmOrderHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<ConfirmOrderRequest>(request);
                var res = await _OrderInterface.ConfirmOrder(requestI);

                var response = _mapper.Map<ConfirmOrderHandlerResponse>(res);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new ConfirmOrderHandlerResponse
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