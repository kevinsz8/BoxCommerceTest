using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManufacturerVehicles.Orchestration.Business.Handlers
{
	public class GetItemHandler : IRequestHandler<GetItemHandlerRequest, GetItemHandlerResponse>
	{
		private readonly IItemInterface _ItemInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public GetItemHandler(IItemInterface ItemInterface, IMapper mapper, ILogger<GetItemHandler> logger)
		{
			_ItemInterface = ItemInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<GetItemHandlerResponse> Handle(GetItemHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = new GetItemRequest();
				var res = await _ItemInterface.GetItems(requestI);

				var response = _mapper.Map<GetItemHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new GetItemHandlerResponse
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
