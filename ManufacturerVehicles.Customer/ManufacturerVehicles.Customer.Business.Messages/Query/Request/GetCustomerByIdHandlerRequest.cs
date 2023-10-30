using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Customer.Business.Messages.Query.Request
{
    public class GetCustomerByIdHandlerRequest : IRequest<GetCustomerByIdHandlerResponse>
    {
        public Guid CustomerId { get; set; }
    }
}
