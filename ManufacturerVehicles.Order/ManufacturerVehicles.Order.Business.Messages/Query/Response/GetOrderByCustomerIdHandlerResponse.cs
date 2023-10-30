using ManufacturerVehicles.Order.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Query.Response
{
    public class GetOrderByCustomerIdHandlerResponse : BaseResponse
    {
        public List<Orders> Orders { get; set; }
    }
}
