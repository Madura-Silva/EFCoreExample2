using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public interface ICustomerQuery
    {
        Task<CustomerDTO> GetCustomerAsync(int id);
        Task<CustomerDTO> InsertCustomerAsync(CustomerDTO customerDTO);

        Task<CustomerDTO> UpdateCustomerAsync(CustomerDTO customerDTO);

        Task<bool> DeleteCustomerAsync(int id);

        Task<List<CustomerDTO>> GetAllCustomersAsync();
    }
}
