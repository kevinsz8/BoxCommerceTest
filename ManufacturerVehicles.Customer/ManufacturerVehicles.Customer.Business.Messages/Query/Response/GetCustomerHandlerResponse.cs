using ManufacturerVehicles.Customer.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.Business.Messages.Query.Response
{
    public class GetCustomerHandlerResponse : BaseResponse
    {
        public List<Customers> Customers { get; set; }
    }
}
