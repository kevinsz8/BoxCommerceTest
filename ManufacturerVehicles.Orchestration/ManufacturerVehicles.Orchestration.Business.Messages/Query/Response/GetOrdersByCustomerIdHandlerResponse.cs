using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Query.Response
{
    public class GetOrdersByCustomerIdHandlerResponse : BaseResponse
    {
        public List<Orders> Orders { get; set; }
    }
}
