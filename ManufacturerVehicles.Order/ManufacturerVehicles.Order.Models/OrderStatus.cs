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
        ProductionCompleted = 2,
        Delivering = 3,
        ReadyToPickUp = 4,
        Canceled = 5

    }
}
