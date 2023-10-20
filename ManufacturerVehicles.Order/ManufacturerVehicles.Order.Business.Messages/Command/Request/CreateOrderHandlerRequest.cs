using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Command.Request
{
    public class CreateOrderHandlerRequest : IRequest<CreateOrderHandlerResponse>
	{
		public Guid CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public OrderStatus Status { get; set; }
	}
}
