using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderById(int id);
        Task<OrderDTO> InsertOrder(OrderDTO orderDTO);

        Task<OrderDTO> UpdateOrder(OrderDTO orderDTO);

        Task<bool> DeleteOrder(int id);
    }
}
