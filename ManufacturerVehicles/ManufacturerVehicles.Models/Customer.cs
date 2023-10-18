using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Models
{
	public class Customer
	{
		[Key]
		public int CustomerID { get; set; }

		[MaxLength(255)]
		public string Name { get; set; }

		[MaxLength(255)]
		public string Email { get; set; }

		[MaxLength(20)]
		public string Phone { get; set; }
	}
}
