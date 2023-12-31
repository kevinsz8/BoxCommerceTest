﻿using ManufacturerVehicles.Order.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Command.Response
{
	public class DeleteItemOrderHandlerResponse : BaseResponse
	{
		public Guid OrderId { get; set; }
		public int QuantityRemaining { get; set; }
	}
}
