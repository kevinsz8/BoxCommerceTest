using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Command.Request
{
	public class CreateOrderHandlerRequest : IRequest<CreateOrderHandlerResponse>
	{
		public Guid CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
	}
}
