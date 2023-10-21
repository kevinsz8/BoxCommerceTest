using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerVehicles.Orchestration.Models
{
	public class ServiceConfigOptions
	{
		public const string MicroserviceUrls = "MicroserviceUrls";
		public string GetItemsEndpoint { get; set; }
		public string GetCustomersEndpoint { get; set; }
	}
}
