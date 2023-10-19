using AutoMapper;
using ManufacturerVehicles.Customer.Business.Messages.Query.Request;
using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using ManufacturerVehicles.Customer.Services;
using ManufacturerVehicles.ServiceClients.Messages.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customers.Business.Handlers
{
	public class GetCustomerHandler : IRequestHandler<GetCustomerHandlerRequest, GetCustomerHandlerResponse>
	{
		private readonly ICustomerInterface _customerInterface;
		private readonly IMapper _mapper;
		public GetCustomerHandler(ICustomerInterface customerInterface, IMapper mapper)
		{
			_customerInterface = customerInterface;
			_mapper = mapper;
		}

		public async Task<GetCustomerHandlerResponse> Handle(GetCustomerHandlerRequest request, CancellationToken cancellationToken)
		{
			var requestI = new GetCustomerRequest();
			var customers = await _customerInterface.GetCustomers(requestI);


			return _mapper.Map<GetCustomerHandlerResponse>(customers);
		}
	}
}
