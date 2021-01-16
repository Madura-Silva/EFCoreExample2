using CoreSample.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetCustomerById(int id);
        Task<CustomerDTO> InsertCustomer(CustomerDTO customerDTO);

        Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO);

        Task<bool> DeleteCustomer(int id);

        Task<List<CustomerDTO>> GetAllCustomers();
    }
}