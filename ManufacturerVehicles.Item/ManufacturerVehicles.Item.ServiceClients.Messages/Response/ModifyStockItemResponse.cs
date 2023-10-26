using ManufacturerVehicles.Item.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.ServiceClients.Messages.Response
{
    public class ModifyStockItemResponse : BaseResponse
    {
        public Guid ItemId { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
