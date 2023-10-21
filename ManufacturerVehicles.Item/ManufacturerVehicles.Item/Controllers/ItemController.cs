using Azure.Core;
using ManufacturerVehicles.Item.Business.Messages.Query.Request;
using ManufacturerVehicles.Item.Business.Messages.Query.Response;
using ManufacturerVehicles.Item.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Item.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ItemController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet(Name = "GetItem")]
		[ProducesResponseType(typeof(GetItemHandlerResponse), 200)]
		public async Task<GetItemHandlerResponse> GetItem()
		{
			var request = new GetItemHandlerRequest();
			var response = await _mediator.Send(request);

			return response;
		}
	}
}
