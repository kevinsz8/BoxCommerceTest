using Azure.Core;
using ManufacturerVehicles.Item.Business.Messages.Command.Request;
using ManufacturerVehicles.Item.Business.Messages.Command.Response;
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

		[HttpGet]
		[Route("getItems")]
		[ProducesResponseType(typeof(GetItemHandlerResponse), 200)]
		public async Task<GetItemHandlerResponse> GetItem()
		{
			var request = new GetItemHandlerRequest();
			return await _mediator.Send(request);
		}

        [HttpPost]
        [Route("modifyStockItem")]
        [ProducesResponseType(typeof(ModifyStockItemHandlerResponse), 200)]
        public async Task<ModifyStockItemHandlerResponse> ModifyStockItem([FromBody] ModifyStockItemHandlerRequest request )
        {
			return await _mediator.Send(request);
        }
    }
}
