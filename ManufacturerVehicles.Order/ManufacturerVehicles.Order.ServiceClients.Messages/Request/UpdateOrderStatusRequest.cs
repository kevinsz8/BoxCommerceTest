using ManufacturerVehicles.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Request
{
	public class UpdateOrderStatusRequest
	{
		public Guid OrderId { get; set; }
		public OrderStatus Status { get; set; }
	}
}
