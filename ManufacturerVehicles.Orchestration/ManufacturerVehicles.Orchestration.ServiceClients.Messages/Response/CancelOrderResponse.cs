﻿using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response
{
    public class CancelOrderResponse : BaseResponse
    {
        public Guid OrderId { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
