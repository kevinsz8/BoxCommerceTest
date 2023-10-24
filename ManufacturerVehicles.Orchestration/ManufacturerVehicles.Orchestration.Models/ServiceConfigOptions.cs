using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ManufacturerVehicles.Orchestration.Models
{
	public class ServiceConfigOptions
	{
		public const string MicroserviceUrls = "MicroserviceUrls";
		public string GetItemsEndpoint { get; set; }
		public string GetCustomersEndpoint { get; set; }
		public string GetOrdersEndpoint { get; set; }
		public string CreateOrderEndpoint { get; set; }
		public string AddOrderItemEndpoint { get; set; }
		public string DeleteOrderItemEndpoint { get; set; }
		public string UpdateOrderStatusEndpoint { get; set; }
	}
}
