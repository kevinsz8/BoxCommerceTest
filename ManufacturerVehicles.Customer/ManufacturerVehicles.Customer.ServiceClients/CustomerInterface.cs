﻿using AutoMapper;
using ManufacturerVehicles.Customer.Services;
using ManufacturerVehicles.Customer.DataAccess;
using ManufacturerVehicles.Customer.ServiceClients.Messages.Request;
using ManufacturerVehicles.Customer.ServiceClients.Messages.Response;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Customer.ServiceClients
{
	public class CustomerInterface : ICustomerInterface
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CustomerInterface(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<GetCustomerResponse>> GetCustomers(GetCustomerRequest request)
		{
			var customerData = await (from data in _context.Customers 
									  select new GetCustomerResponse
									  {
										  CustomerID = data.CustomerID,
										  Email = data.Email,
										  Name = data.Name,
										  Phone = data.Phone
									  }).ToListAsync();

			return customerData;
		}

        public async Task<GetCustomersByIdResponse> GetCustomersById(GetCustomersByIdRequest request)
        {
            var customerData = await (from data in _context.Customers
									  where data.CustomerID == request.CustomerId
                                      select new GetCustomersByIdResponse
                                      {
                                          CustomerID = data.CustomerID,
                                          Email = data.Email,
                                          Name = data.Name,
                                          Phone = data.Phone
                                      }).FirstOrDefaultAsync();

			if(customerData != null)
			{
				customerData.Success = true;
			}
			else
			{
				customerData.Success = false;
			}

            return customerData;
        }
    }
}