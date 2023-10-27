using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Communication.Business.Messages.Common
{
    public class Customers
    {
        public Guid CustomerID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
