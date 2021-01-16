using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public interface IOrderQuery
    {
        Task<OrderDTO> GetOrderAsync(int id);
        Task<OrderDTO> InsertOrderAsync(OrderDTO orderDTO);

        Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO);

        Task<bool> DeleteOrderAsync(int id);

        Task<bool> DeleteAllOrdersForCustomerAsync(int id);
    }
}
