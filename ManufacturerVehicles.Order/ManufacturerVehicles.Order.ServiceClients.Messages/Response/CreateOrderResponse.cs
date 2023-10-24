﻿using ManufacturerVehicles.Order.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Response
{
	public class CreateOrderResponse : BaseResponse
	{
		public Guid OrderId { get; set; }
		public Guid CustomerId { get; set; }
	}
}
