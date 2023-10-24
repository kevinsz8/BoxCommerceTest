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
	public class GetOrderHandler : IRequestHandler<GetOrderHandlerRequest, GetOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public GetOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<GetOrderHandlerResponse> Handle(GetOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = new GetOrderRequest();
				var res = await _OrderInterface.GetOrders(requestI);

				var response = _mapper.Map<GetOrderHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new GetOrderHandlerResponse
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
