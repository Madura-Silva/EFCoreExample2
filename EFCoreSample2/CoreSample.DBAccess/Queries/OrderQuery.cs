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
    public class OrderQuery : IOrderQuery
    {
        private ICoreSampleDBContext _dbContext;

        public OrderQuery(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> DeleteAllOrdersForCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                var order = _dbContext.DbSet<Order>().Where(x => x.Id == id).SingleOrDefault();
                _dbContext.DbSet<Order>().Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            return await _dbContext.DbSet<Order>().Where(x => x.Id == id)
                .Select(x => new OrderDTO
                {
                    BillingAddress = x.BillingAddress,
                    OrderAddress = x.OrderAddress,
                    OrderedDate = x.OrderedDate,
                    OrderNumber = x.OrderNumber,
                    Id = x.Id,
                    Customer = new CustomerDTO { 
                        FirstName = x.Customer.FirstName,
                        Id = x.Customer.Id,
                        LastName = x.Customer.LastName
                    },
                    Products = x.OrderProducts.Select(pr => new ProductDTO { 
                        Id = pr.Product.Id,
                        CurrentPrice = pr.Product.CurrentPrice,
                        Name = pr.Product.Name,
                        SKU = pr.Product.SKU,
                        NoOfItemsOrdered = pr.NumberOfItems,                        
                    }).ToList()
                }).SingleOrDefaultAsync();
        }

        public async Task<OrderDTO> InsertOrderAsync(OrderDTO orderDTO)
        {
            Order order = new Order
            {
                OrderAddress = orderDTO.OrderAddress,
                OrderedDate = DateTime.Now,
                BillingAddress = orderDTO.BillingAddress,
                CustomerId = orderDTO.Customer.Id,
            };
            order.OrderNumber = "ORN-"+ DateTime.Now.ToString("yyyy-mm-dd-HH-mm");        
            order.OrderProducts = new List<OrderProduct>();
            foreach (var product in orderDTO.Products)
            {
                order.OrderProducts.Add(new OrderProduct { 
                    ProductId = product.Id,
                    OrderId = order.Id,
                    NumberOfItems = product.NoOfItemsOrdered,
                });
            }


            _dbContext.DbSet<Order>().Add(order);
            await _dbContext.SaveChangesAsync();

            orderDTO.Id = order.Id;
            return orderDTO;
        }

        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO)
        {
            var order = await _dbContext.DbSet<Order>()
                .Include(x => x.OrderProducts)
                .Where(x => x.Id == orderDTO.Id).SingleOrDefaultAsync();

            if (order != null)
            {
                order.OrderAddress = orderDTO.OrderAddress;
                order.BillingAddress = orderDTO.BillingAddress;
                order.OrderProducts.Clear();
                order.OrderProducts = new List<OrderProduct>();
                foreach (var product in orderDTO.Products)
                {
                    order.OrderProducts.Add(new OrderProduct
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                        NumberOfItems = product.NoOfItemsOrdered,
                    });
                }

            }

            //Order order = new Order
            //{
            //    Id = orderDTO.Id,
            //    OrderedDate = orderDTO.OrderedDate,
            //    OrderNumber= orderDTO.OrderNumber,
            //    OrderAddress = orderDTO.OrderAddress,
            //    BillingAddress = orderDTO.BillingAddress,
            //    CustomerId = orderDTO.Customer.Id,
            //};
            //order.OrderProducts = new List<OrderProduct>();
            //foreach (var product in orderDTO.Products)
            //{
            //    order.OrderProducts.Add(new OrderProduct
            //    {
            //        ProductId = product.Id,
            //        OrderId = order.Id,
            //    });
            //}

            _dbContext.DbSet<Order>().Update(order);
            await _dbContext.SaveChangesAsync();

            return orderDTO;
        }
    }
}
