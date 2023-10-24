using ManufacturerVehicles.Order.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Query.Response
{
    public class GetOrderItemByOrderIdHandlerResponse : BaseResponse
    {
        public List<OrderItems> OrderItems { get; set; }
    }
}
