using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Command.Response
{
	public class UpdateStatusOrderHandlerResponse : BaseResponse
	{
		public Guid OrderId { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
