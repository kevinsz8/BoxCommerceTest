using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ManufacturerVehicles.Orchestration.Controllers
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

		//[HttpGet(Name = "GetCustomers")]
		//[ProducesResponseType(typeof(GetItemHandlerResponse), 200)]
		//public async Task<IActionResult> GetItemCustomer()
		//{
		//	using (var httpClient = new HttpClient())
		//	{
		//		var orderServiceUrl = "https://localhost:7070/Item/getItems";
		//		var response = await httpClient.GetAsync($"{orderServiceUrl}");

		//		if (response.IsSuccessStatusCode)
		//		{
		//			var json = await response.Content.ReadAsStringAsync();
		//			var customerResponse = JsonConvert.DeserializeObject<GetItemHandlerResponse>(json);

		//			return Ok(customerResponse);
		//		}
		//		else
		//		{
		//			return StatusCode((int)response.StatusCode, "items failed failed");
		//		}
		//	}

		//}

		[HttpGet]
		[Route("getItems")]
		[ProducesResponseType(typeof(GetItemHandlerResponse), 200)]
		public async Task<GetItemHandlerResponse> GetItem()
		{
			var request = new GetItemHandlerRequest();
			var response = await _mediator.Send(request);

			return response;
		}
	}
}
