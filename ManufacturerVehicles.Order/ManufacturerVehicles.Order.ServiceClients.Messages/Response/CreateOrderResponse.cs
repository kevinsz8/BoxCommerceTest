﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Response
{
	public class CreateOrderResponse
	{
		public Guid OrderId { get; set; }
		public Guid CustomerId { get; set; }
	}
}
