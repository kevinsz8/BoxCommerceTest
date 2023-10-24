using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.Business.Messages.Common
{
    public class BaseResponse
    {
        public string ErrorMessage { get; set; }
        public string StatusMessage { get; set; }
        public bool Success { get; set; }
    }
}
