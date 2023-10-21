using AutoMapper;
using ManufacturerVehicles.Item.Business.Messages.Common;
using ManufacturerVehicles.Item.Business.Messages.Query.Request;
using ManufacturerVehicles.Item.Business.Messages.Query.Response;
using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManufacturerVehicles.Item.Business.Handlers
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
				var itemsResponse = await _ItemInterface.GetItems(requestI);

				var response = new GetItemHandlerResponse()
				{
					StatusMessage = "Success",
					Items = _mapper.Map<List<Items>>(itemsResponse),
					Success = true
				};

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new GetItemHandlerResponse
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
