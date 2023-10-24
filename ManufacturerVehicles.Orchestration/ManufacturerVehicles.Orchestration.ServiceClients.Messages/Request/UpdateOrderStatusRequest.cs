using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request
{
	public class UpdateOrderStatusRequest
	{
		public Guid OrderId { get; set; }
		public OrderStatus Status { get; set; }
	}
}
