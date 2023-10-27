using System.ComponentModel.DataAnnotations;

namespace ManufacturerVehicles.Communication.Models
{
	public class Customer
	{
		[Key]
		public Guid CustomerID { get; set; }

		[MaxLength(255)]
		public string Name { get; set; }

		[MaxLength(255)]
		public string Email { get; set; }

		[MaxLength(20)]
		public string Phone { get; set; }
	}
}
