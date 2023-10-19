using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Response
{
	public class GetOrderResponse
	{
		public int OrderID { get; set; }

		public int CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
