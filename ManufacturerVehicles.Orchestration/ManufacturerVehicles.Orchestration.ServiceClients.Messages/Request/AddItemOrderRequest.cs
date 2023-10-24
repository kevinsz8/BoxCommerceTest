﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request
{
	public class AddItemOrderRequest
	{
		public Guid OrderId { get; set; }
		public Guid ItemId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
