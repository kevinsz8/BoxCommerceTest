using ManufacturerVehicles.ServiceClients.Messages.Request;
using ManufacturerVehicles.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Customer.Services
{
	public interface ICustomerInterface
	{
		Task<GetCustomerResponse> GetCustomers(GetCustomerRequest request);
	}
}