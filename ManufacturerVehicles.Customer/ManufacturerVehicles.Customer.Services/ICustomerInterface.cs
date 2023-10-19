using ManufacturerVehicles.Customer.ServiceClients.Messages.Request;
using ManufacturerVehicles.Customer.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Customer.Services
{
	public interface ICustomerInterface
	{
		Task<GetCustomerResponse> GetCustomers(GetCustomerRequest request);
	}
}