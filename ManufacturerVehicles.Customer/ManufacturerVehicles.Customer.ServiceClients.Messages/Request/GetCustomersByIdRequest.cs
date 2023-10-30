using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.ServiceClients.Messages.Request
{
    public class GetCustomersByIdRequest
    {
        public Guid CustomerId { get; set; }
    }
}
