using CoreSample.DBAccess.Queries;
using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public class OrderService : IOrderService
    {
        private IOrderQuery _orderQuery;

        public OrderService(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }
        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderQuery.DeleteOrderAsync(id);
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await _orderQuery.GetOrderAsync(id);
        }

        public async Task<OrderDTO> InsertOrder(OrderDTO orderDTO)
        {
            return await _orderQuery.InsertOrderAsync(orderDTO);
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO orderDTO)
        {
            return await _orderQuery.UpdateOrderAsync(orderDTO);
        }
    }
}
