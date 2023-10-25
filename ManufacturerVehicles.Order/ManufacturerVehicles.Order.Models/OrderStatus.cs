using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Models
{
	public enum OrderStatus
	{
		New = 0,
		Confirmed = 1,
		InProduction = 2,
		Completed = 3,
        Delivering = 4,
        Canceled = 5

	}
}
