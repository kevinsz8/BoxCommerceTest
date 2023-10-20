using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.ServiceClients.Messages.Response
{
	public class GetCustomerResponse
	{
		public Guid CustomerID { get; set; }

		public string? Name { get; set; }

		public string? Email { get; set; }

		public string? Phone { get; set; }
	}
}
