using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Orchestration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("getCustomers")]
        [ProducesResponseType(typeof(GetCustomerHandlerResponse), 200)]
        public async Task<GetCustomerHandlerResponse> GetCustomer()
        {
            var request = new GetCustomerHandlerRequest();
            var response = await _mediator.Send(request);

            return response;
        }
    }
}
