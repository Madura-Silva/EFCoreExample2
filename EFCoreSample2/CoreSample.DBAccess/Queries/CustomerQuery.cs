using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public class CustomerQuery : ICustomerQuery
    {
        private ICoreSampleDBContext _dbContext;

        public CustomerQuery(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = _dbContext.DbSet<Customer>().Where(x => x.Id == id).SingleOrDefault();
                _dbContext.DbSet<Customer>().Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }       
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _dbContext.DbSet<Customer>()
                .Select(x => new CustomerDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    Orders = x.Orders.Select(or => new OrderDTO
                    {
                        Id = or.Id,
                        BillingAddress = or.BillingAddress,
                        OrderAddress = or.OrderAddress,
                        OrderedDate = or.OrderedDate,
                        OrderNumber = or.OrderNumber
                    }).ToList()
                }).ToListAsync();


        }

        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            //Best way to convert to DTO and send

            return await _dbContext.DbSet<Customer>().Where(x => x.Id == id)
                .Select(x => new CustomerDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    Orders = x.Orders.Select(or => new OrderDTO
                    {
                        Id = or.Id,
                        BillingAddress = or.BillingAddress,
                        OrderAddress = or.OrderAddress,
                        OrderedDate = or.OrderedDate,
                        OrderNumber = or.OrderNumber
                    }).ToList()
                }).SingleOrDefaultAsync();



            //Another way of converting to DTO and send
            //var customer = _dbContext.DbSet<Customer>().Include(o => o.Orders).Where(x => x.Id == id).SingleOrDefault();

            //var customerDTO = new CustomerDTO();
            //customerDTO.Id = customer.Id;
            //customerDTO.FirstName = customer.FirstName;
            //customerDTO.LastName = customer.LastName;
            //customerDTO.Orders = new List<OrderDTO>();

            //foreach (var order in customer.Orders)
            //{
            //    var orderDTO = new OrderDTO();
            //    orderDTO.Id = order.Id;
            //    orderDTO.OrderAddress = order.OrderAddress;

            //    customerDTO.Orders.Add(orderDTO);
            //}
            //return customerDTO;

            //Another way of doing
            //return await _dbContext.DbSet<Customer>().Include(o => o.Orders).Where(x => x.Id == id).Select(x => new CustomerDTO {
            //    Id = x.Id,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    Orders = x.Orders.Select(or => new OrderDTO { 
            //        Id = or.Id,
            //        BillingAddress = or.BillingAddress,
            //        OrderAddress = or.OrderAddress,
            //        OrderedDate = or.OrderedDate,
            //        OrderNumber = or.OrderNumber,
            //        }).ToList()

            //}).SingleOrDefaultAsync();
        }


        public async Task<CustomerDTO> InsertCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                
            };
            _dbContext.DbSet<Customer>().Add(customer);
            await _dbContext.SaveChangesAsync();

            customerDTO.Id = customer.Id;
            return customerDTO;

        }

        public async Task<CustomerDTO> UpdateCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Id = customerDTO.Id,
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,

            };
            _dbContext.DbSet<Customer>().Update(customer);
            await _dbContext.SaveChangesAsync();
            
            return customerDTO;
        }
    }
}
