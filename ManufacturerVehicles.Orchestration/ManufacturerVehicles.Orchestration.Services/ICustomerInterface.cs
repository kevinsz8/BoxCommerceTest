using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Services
{
    public interface ICustomerInterface
    {
        Task<GetCustomerResponse> GetCustomers(GetCustomerRequest request);
    }
}
