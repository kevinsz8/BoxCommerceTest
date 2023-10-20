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
		Placed = 1,
		InProduction = 2,
		OnHold = 3,
		Completed = 4,
		Canceled = 5

	}
}
