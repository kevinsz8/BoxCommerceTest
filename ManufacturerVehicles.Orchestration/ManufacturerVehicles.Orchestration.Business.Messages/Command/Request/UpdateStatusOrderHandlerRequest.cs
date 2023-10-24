using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Command.Request
{
	public class UpdateStatusOrderHandlerRequest : IRequest<UpdateStatusOrderHandlerResponse>
	{
		public Guid OrderId { get; set; }
		public OrderStatus Status { get; set; }
	}
}
