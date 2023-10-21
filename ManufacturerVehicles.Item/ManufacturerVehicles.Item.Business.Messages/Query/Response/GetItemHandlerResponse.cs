using ManufacturerVehicles.Item.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.Business.Messages.Query.Response
{
    public class GetItemHandlerResponse : BaseResponse
    {
        public List<Items> Items { get; set; }
    }
}
