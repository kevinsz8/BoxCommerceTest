﻿using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Command.Response
{
	public class AddItemOrderHandlerResponse : BaseResponse
	{
		public Guid ItemId { get; set; }
		public string? Message { get; set; }
    }
}
