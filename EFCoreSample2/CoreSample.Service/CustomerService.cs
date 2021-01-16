using CoreSample.DBAccess.Cust.Queries;
using CoreSample.DBAccess.Queries;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerQuery _customerQuery;
        

        public CustomerService(ICustomerQuery customerQuery)
        {
            _customerQuery = customerQuery;          
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            return await _customerQuery.DeleteCustomerAsync(id);
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            return await _customerQuery.GetAllCustomersAsync();
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return await _customerQuery.GetCustomerAsync(id);
        }

        public async Task<CustomerDTO> InsertCustomer(CustomerDTO customerDTO)
        {
            return await _customerQuery.InsertCustomerAsync(customerDTO);
        }

        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO)
        {
            return await _customerQuery.UpdateCustomerAsync(customerDTO);
        }
    }
}
